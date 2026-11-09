#!/usr/bin/env python3
"""Split files too large for ordinary Git hosting into numbered parts."""

import argparse
import re
from pathlib import Path

PART_FILE = re.compile(r"\.part\d+$")


def split_file(source: Path, chunk_size: int) -> int:
    size = source.stat().st_size
    if size <= chunk_size or PART_FILE.search(source.name):
        return 0

    targets = []
    temporary = []
    try:
        with source.open("rb") as input_file:
            index = 0
            while True:
                block = input_file.read(chunk_size)
                if not block:
                    break

                target = source.with_name(f"{source.name}.part{index:02d}")
                temp = target.with_name(f"{target.name}.tmp")
                if target.exists() or temp.exists():
                    raise FileExistsError(f"Refusing to overwrite {target}")

                with temp.open("xb") as output_file:
                    output_file.write(block)
                temporary.append(temp)
                targets.append(target)
                index += 1

        for temp, target in zip(temporary, targets):
            temp.replace(target)
        source.unlink()
    except Exception:
        for path in temporary:
            path.unlink(missing_ok=True)
        for path in targets:
            path.unlink(missing_ok=True)
        raise

    print(f"[Chunk] {source} -> {len(targets)} parts ({size} bytes)")
    return 1


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("directory", type=Path, help="Directory tree to scan")
    parser.add_argument(
        "--chunk-size-mb",
        type=int,
        default=90,
        help="Maximum size per part in MiB (default: 90)",
    )
    args = parser.parse_args()
    if args.chunk_size_mb < 1:
        parser.error("--chunk-size-mb must be at least 1")
    if not args.directory.is_dir():
        parser.error(f"directory does not exist: {args.directory}")

    chunk_size = args.chunk_size_mb * 1024 * 1024
    files = sorted(
        path
        for path in args.directory.rglob("*")
        if path.is_file() and not PART_FILE.search(path.name)
    )
    chunked = sum(split_file(path, chunk_size) for path in files)
    print(f"[Chunk] Finished; split {chunked} files")


if __name__ == "__main__":
    main()
