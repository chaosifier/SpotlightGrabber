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


#### What the app to run every time you unlock your screen and have your wallpeper updated? Follow the steps to create a Task Schedule to do exactly the same:
1. Open Task Scheduler (Run "taskschd.msc")
2. Create a New Task and give it a meaningful name, like "Spotlight Grabber"
3. Under Triggers, add a new Trigger and under Begin the task, select On workstation unlock.. Save and close the trigger window.
4. Under Actions, add a new Action. Select Start a program under Action and browse to the location of SpotlightGrabber.exe.
5. Add arguments as required, e.g. c:\SpotlightImages 500 yes
6. Press ok and exit New Action window.
7. Under Conditions, remove Start the task only if the computer is on AC power. (optional)
Open Task Scheduler (Run > "taskschd.msc").
8. Press OK and you're all set up.

Now everytime you unlock your screen and new Spotlight image is available, it will automatically be saved to your specified diretory and the wallpeper will be set to the largest version of spotlight image.


### <a href="https://github.com/chaosifier/SpotlightGrabber/raw/master/SpotlightGrabber/bin/Release/SpotlightGrabber.exe">Download SpotlightGrabber.exe</a>
