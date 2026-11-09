# Dota 2 Mobile v1.4 offline iOS port

The iOS build uses the v1.4 APK's recovered assets and Unity `6000.6.7f1`, with IL2CPP and Apple Metal.

## Available offline flow

- Main Menu → Offline Game Modes → Hero Selection → Hero Demo or Skill Practice.
- Hero Collection opens the roster directly. It contains 127 recovered hero portraits; 57 heroes have a recovered base 3D model for the local demo. Portrait-only entries are disabled for 3D play.
- In a demo, the virtual joystick and tap-to-move control movement. Attack and Q/W/E/R buttons trigger local preview effects, cooldown feedback, and damage to a practice target.
- All menus, demos, and practice targets run locally. The app has no multiplayer, matchmaking, account login, or server connection.

## Limits of the recovered APK

The APK's IL2CPP gameplay methods are stubs. Its original match simulation, exact hero spell logic, and network play cannot be restored from this export. The demo HUD uses local showcase effects and a training target; it is not a Dota match or an exact implementation of each hero's abilities.

## Build the IPA

GitHub Actions reassembles chunked Xcode files, archives `ExportedProject/build_ios/Unity-iPhone.xcodeproj`, and uploads an unsigned IPA. Sign the IPA before installing it on a standard iPhone or iPad.

To prepare a new Xcode export, split files above the normal GitHub file-size limit with:

```bash
python3 ../tools/chunk_large_files.py ExportedProject/build_ios
```

The workflow rejoins the numbered parts before calling Xcode. The full AssetRipper Unity source tree is not included in this repository; the editor exporter under `ExportedProject/Assets/IOSPort/Editor` needs that complete tree to regenerate the Xcode project.
