# ULTRAKILL Speedometer
### by Walaryne
___
## To Install:


First, download and install [BepInEx 6.0.0BE](https://builds.bepis.io/projects/bepinex_be) to your ULTRAKILL game directory for your platform (WINE/Proton is still Windows!).

(Note: Downloads are shown when you click on a build artifact, I used `404`.)

Instructions for installation are on the [BepInEx website](https://docs.bepinex.dev/master/articles/user_guide/installation/index.html).

Next, run the game once to generate the BepInEx/plugins folder, close the game, then copy the `UltrakillSpeedometer.dll` you downloaded from Releases into it.

Run the game again, and enjoy!
___
## Notes
### Regarding Proton/WINE on Linux
BepInEx WILL work on Linux for Proton games, however you need to manually override winhttp in the game's compat prefix.

The best method for this is to grab [protontricks](https://github.com/Matoking/protontricks), run the GUI for it, select ULTRAKILL, select default prefix, and run winecfg.

Then from there, go to the libraries tab, and in the text box next to the add button, type `winhttp` and add it, then hit apply and close everything out.

When you rerun the game, BepInEx should be functional.
___
## Building
In order to build this project, you must source the following assemblies yourself:
```
0Harmony.dll         BepInEx.Preloader.Core.dll   MonoMod.RuntimeDetour.dll
Assembly-CSharp.dll  BepInEx.Preloader.Unity.dll  MonoMod.Utils.dll
BepInEx.Core.dll     BepInEx.Unity.dll            UnityEngine.CoreModule.dll
```
Some can be found in the BepInEx/core folder of your installation, some are found in ULTRAKILL/ULTRAKILL_Data/Managed folder.

**THESE FILES CANNOT BE REDISTRIBUTED** unless the licensing permits it.
Harmony, BepInEx, MonoMod, and *possibly* Unity libs are okay.
However under **no circumstances can Assembly-CSharp.dll be redistributed.** This contains copyrighted game code.