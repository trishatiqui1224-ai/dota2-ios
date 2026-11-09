# Dota 2 Mobile v1.4 iOS boot demo

This repository prepares the Unity game distributed as `Dota-2-Mobile-v1.4-APKVISION.apk` for an iOS build. It is a fan-made Unity adaptation, not Valve's PC game.

## Current scope

- Uses the Shadow Fiend assets recovered from the v1.4 APK.
- Boots into a dark, card-based launcher; Play opens a touch-to-move hero showcase on a standalone stage.
- Recovered IL2CPP gameplay methods in this APK are stubs. This build does not provide a complete match, abilities, or server simulation.
- Unity generates the Xcode project; a macOS runner with Xcode is required to archive and package an IPA.
- The generated IPA is unsigned and must be signed before installation on a standard iPhone or iPad.

## Build an unsigned IPA with GitHub Actions

The workflow at [`.github/workflows/build-ios.yml`](.github/workflows/build-ios.yml) runs on `macos-15` and builds the Xcode project in `unity_ios_project/ExportedProject/build_ios`.

- Push to `main` or `master`, or run **Build Dota 2 Mobile iOS IPA** from the repository's Actions tab.
- The workflow checks out Git LFS files, reassembles chunked build data, and runs `xcodebuild archive`.
- It uploads `Dota2Mobile-unsigned.ipa` as an Actions artifact and publishes the `v1.4-demo-ios` release on the default branch.

## Build locally on macOS

```bash
python3 tools/reassemble_chunks.py unity_ios_project/ExportedProject/build_ios
xcodebuild archive \
  -project unity_ios_project/ExportedProject/build_ios/Unity-iPhone.xcodeproj \
  -scheme Unity-iPhone \
  -configuration Release \
  -destination 'generic/platform=iOS' \
  -archivePath build/Unity-iPhone.xcarchive \
  CODE_SIGNING_ALLOWED=NO
```

## Updating from a newer APK

An APK update requires extracting that APK's Unity assets and metadata, checking its serialized scene and IL2CPP method state, then exporting a fresh iOS Xcode project with the matching boot scene and settings. Replace the contents of `unity_ios_project/ExportedProject/build_ios` with that export, preserve any project-side fixes, and push the change to run the IPA workflow. If the newer APK still has stripped gameplay methods, the iOS build can only run the functionality reconstructed in this project.

Before committing a fresh export, split files larger than GitHub's ordinary per-file limit:

```bash
python3 tools/chunk_large_files.py unity_ios_project/ExportedProject/build_ios
```

The GitHub Actions workflow rejoins those `.partNN` files before invoking Xcode.
