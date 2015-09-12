# TextDisplay
a text display driver framework

Usage:

1. Either build/add the Windows Runtime Component as a binary reference to your solution of add the TextDisplayManager project to you solution.
2. Edit the "screens.config" file to match your setup.
3. Call:
            var displays = await TextDisplayManager.GetDisplays();
4. The 'displays' variable will contain a list of configured displays for the system (as defined in screens.config).
