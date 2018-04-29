# Spotlight Image Grabber

Console app to grab Windows Spotlight images and automatically set as wallpaper.

- Grabs Windows Spotlight wallpapers and optionally stores in Specified directory.
- Minimum image size to grab can be specified.
- Can optionally set the largest image as wallpaper.

<b>Arguments</b>

*First:* Destination directory to store grabbed images to. Uses SpotlightImages directory in teporary system path to store images if no parameter is provided. Files that already exists in the directory are not replaced.

*Second:* Minimum image size to grab. The spotlight image size must exceed the provided size in KB to be grabbed. The default value is 200KB.

*Third:* Yes/No value that indicates whether to automatically change the wallpaper with the largest grabbed image. Default mode of Stretch is used to set wallpaper. The default value is Yes.

<b>Usage:</b>
```
SpotlightGrabber.exe c:\SpotlightImages 500 yes
```
>Grabs and stores spotlight images that are above 500KB in size and sets the largest one as wallpeper.



```
SpotlightGrabber.exe
```
>Grabs and stores spotlight images above the default 200KB size mark in temporary directory and sets the largest one as wallpaper.
