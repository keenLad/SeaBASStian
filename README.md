# SeaBASStian

SeaBASStian is a Unity project (2D/UX-focused) that demonstrates scene loading, UI view patterns, and uses a number of Unity packages and gameplay helpers. This README documents how to open, run, and extend the project for developers and contributors.

## Quick facts

- Unity Editor: 2024.3.2f2 (recorded in `ProjectSettings/ProjectVersion.txt` as `6000.2.7f2`)
- Addressables-based scene loading (see `Assets/Scripts/Scenes/LoadingScene.cs`)
- Asynchronous initialization using UniTask (Cysharp UniTask)
- UI patterns include modular view components (e.g. `TabItemView`, `ListView`, `AnimatedContentView`)
- Render Pipeline: Universal RP (URP)

## Table of contents

- Project overview
- Prerequisites
- Opening the project
- Running / Play mode
- Project layout
- Important scripts and architecture notes
- Packages and dependencies
- Contributing
- Troubleshooting & FAQs

## Project overview

This repository contains a small Unity project set up with Addressables and async initialization. Scenes are loaded additively through a `LoadingScene` which activates the main scene once loaded. UI is built using modular view components and Unity UI (UGUI) with ColorTint transitions.

The project uses UniTask for async/await-style coroutines and Addressables for scene/content loading.


## Running / Play mode

- Open `Assets/Scenes/LoadingScene.unity` (if present) or the scene designated as the loading scene in your project. Press Play in the Unity Editor.
- The `LoadingScene` waits (2 seconds by design) then loads an additive scene named `MainScene` using Addressables. If Addressables can't find the scene, check Addressables groups and keys in the Addressable Assets window.

## Project layout (high level)

- Assets/
	- Scripts/
		- Scenes/ - scene initialization and loading helpers (e.g. `SceneInitialiser`, `LoadingScene`)
		- UI/Views/ - modular UI view components (TabView, TabItemView, ListView, AnimatedContentView, TimeView, RequestView)
		- Helpers/ - small extension methods and utilities
	- Plugins/ - third-party plugins (DOTween, etc.)
- ProjectSettings/ - Unity project settings
- Packages/manifest.json - package dependencies

## Important scripts and architecture notes

- SceneInitialiser (Assets/Scripts/Scenes/SceneInitialiser.cs)
	- Collects and runs initialization on items derived from `InitialisableBase` using UniTask.WhenAll. Good place to register services and prepare scene subsystems.

- LoadingScene (Assets/Scripts/Scenes/LoadingScene.cs)
	- Demonstrates Addressables scene loading: loads `MainScene` additively and then activates it.

- UI Views (Assets/Scripts/UI/Views)
	- `TabItemView` and `TabView` implement a simple tabbing system. `TabItemView` toggles content and manages visual state. Note: transitions are handled via ColorTint and direct color assignment in code for immediate visual state.
	- `AnimatedContentView` and `AnimatedItem` show simple UI animation helpers.

## Packages and dependencies

Important entries from `Packages/manifest.json`:

- com.cysharp.unitask (UniTask) — async utilities used throughout the project
- com.unity.addressables — for scene/content loading
- com.unity.render-pipelines.universal — Universal Render Pipeline (URP)
- com.unity.inputsystem — new Input System
- com.unity.ugui — Unity UI (UGUI)

Full manifest can be found at `Packages/manifest.json`.

## Contributing

If you want to contribute:

1. Fork the repo and create a branch per feature/bugfix
2. Keep changes small and focused
3. Open a PR with a clear description and testing steps

Coding conventions:
- C# files follow Unity C# conventions; place MonoBehaviours under `Assets/Scripts` and keep editor-only scripts in `Editor` folders.

## Troubleshooting & FAQs

- Addressables scene not found: open Window -> Asset Management -> Addressables -> Groups and ensure the scene `MainScene` is added to Addressables and has the correct address/key.
- Missing packages on project open: open Window -> Package Manager and install the packages from `Packages/manifest.json` or let Unity resolve them on project load.
- Editor version mismatch: if Unity complains about editor version, install Unity 2024.3.x using Unity Hub or ignore if the mismatch is minor. Some packages may require a specific patch.

## Small developer notes

- When setting visual state on UI elements during Start, prefer assigning the color directly from ColorBlock (e.g. `toggle.targetGraphic.color = toggle.colors.selectedColor`) instead of lerping with `fadeDuration` — lerp expects a t parameter (0..1) and not a duration.
- Use `SceneInitialiser` to register scene-level services and initialize dependencies using UniTask.

```
# SeaBASStian