# RawInputSharpUnity

Wrapper of [RawInput.Sharp](https://github.com/mfakane/rawinput-sharp) for Unity.

## Feature

This project helps you with:

- Window procedure management.
- Examples to utilize Mouse / Keyboard / Gamepad handling.

## Limitation

Only works in build software, not support `Run in Editor` mode.

This is because RawInput heavily depends on Windows event loop system, and running in editor might lead editor crash.

## Unity Version

Unity 2018.4 or newer. 

The project in this repository is created by 2018.4.13f1.

## Install

Easier way is to download unitypackage in releases.

If you want to use from source code: 

1. Clone this repository and open in Unity Editor.
2. Unzip `RawInput.Sharp` nupkg file on [NuGet Package](https://www.nuget.org/packages/RawInput.Sharp/).
3. Import `/lib/netstandard1.1/RawInput.Sharp.dll` into the project.

## To Use other devices than keyboard / mouse

`HidRawInput` class can handle events from generic device other than keyboard and mouse.

You can use it with almost same manner as `KeyboardRawInput` or `MouseRawInput` component.

## Known issue

Event delay issue ( #2 ). 

## License

[zlib license](https://opensource.org/licenses/Zlib).

See [LICENSE.txt](./LICENSE.txt).
