# Dota 2 Mobile v1.4 iOS boot demo

This iOS export uses Shadow Fiend assets from `Dota-2-Mobile-v1.4-APKVISION.apk` and was generated with Unity `6000.6.7f1` for IL2CPP and Apple Metal.

## What currently runs

- The iOS player opens a dark, card-based Dota 2 Mobile launcher.
- Play opens a standalone Shadow Fiend stage with basic touch-to-move input.
- The APK's recovered gameplay methods are stubs. Full matches, abilities, and server simulation are not implemented in this build.
- The project in `ExportedProject/build_ios` is the native Xcode export used by CI. The full AssetRipper Unity source tree is not included here.

The editor boot scripts are under `ExportedProject/Assets/IOSPort`. With the complete Unity asset export restored, the helper reads the APK's original hero prefab, creates a clean showcase scene, removes gameplay-only components from the display prefab, and generates an iOS Xcode project.

## Build the IPA

GitHub Actions runs `../.github/workflows/build-ios.yml` on macOS, reassembles any chunked Xcode files, archives `Unity-iPhone.xcodeproj`, and uploads an unsigned IPA. An unsigned IPA must be signed before installation on a standard iPhone or iPad.

To prepare a new Xcode export for GitHub, split files above the ordinary Git file-size limit with:

```bash
python3 ../tools/chunk_large_files.py ExportedProject/build_ios
```

The workflow rejoins the numbered parts before calling Xcode.
