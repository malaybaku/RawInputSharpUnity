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

## Install by unitypackage

Install unitypackage in release.

## Install from source

Clone this repository and open in Unity Editor.

Unzip `RawInput.Sharp` nupkg file on [NuGet Package](https://www.nuget.org/packages/RawInput.Sharp/).

Import `/lib/netstandard1.1/RawInput.Sharp.dll` into the project.

That's it!

## License

[zlib license](https://opensource.org/licenses/Zlib).

See [LICENSE.txt](./LICENSE.txt).
