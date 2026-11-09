#!/usr/bin/env python3
import argparse
import re
from pathlib import Path

PART_PATTERN = re.compile(r"^(?P<base>.+\.part)(?P<index>\d+)$")


def reassemble(target_dir: Path) -> None:
    print(f"[Reassemble] Scanning for chunked files in {target_dir}...")
    part00_files = sorted(target_dir.rglob("*.part00"))
    if not part00_files:
        print("[Reassemble] No chunked files found.")
        return

    for first_part in part00_files:
        match = PART_PATTERN.match(first_part.name)
        if not match:
            continue

        base_name = match.group("base")[:-5]
        parts = []
        for path in first_part.parent.glob(f"{base_name}.part*"):
            part_match = PART_PATTERN.match(path.name)
            if part_match and part_match.group("base") == match.group("base"):
                parts.append((int(part_match.group("index")), path))

        parts.sort(key=lambda item: item[0])
        indices = [index for index, _ in parts]
        if indices != list(range(len(parts))):
            raise ValueError(f"Chunk sequence is incomplete for {first_part}: {indices}")

        output_path = first_part.with_name(base_name)
        temporary_path = output_path.with_name(f"{output_path.name}.reassembling")
        if temporary_path.exists():
            raise FileExistsError(f"Refusing to overwrite {temporary_path}")

        print(f"[Reassemble] Reassembling {len(parts)} parts into {output_path.name}...")
        try:
            with temporary_path.open("xb") as output_file:
                for _, part_path in parts:
                    with part_path.open("rb") as input_file:
                        while block := input_file.read(8 * 1024 * 1024):
                            output_file.write(block)
            temporary_path.replace(output_path)
        except Exception:
            temporary_path.unlink(missing_ok=True)
            raise

        for _, part_path in parts:
            part_path.unlink()
        print(f"[Reassemble] Finished {output_path.name} ({output_path.stat().st_size / (1024 * 1024):.2f} MiB)")

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Reassemble numbered .partNN files.")
    parser.add_argument("directory", nargs="?", type=Path, default=Path("."))
    args = parser.parse_args()
    if not args.directory.is_dir():
        parser.error(f"directory does not exist: {args.directory}")
    reassemble(args.directory)
