# Camelot! Camelot! Camelot!
It's Only A Model!&trade;

Only A Model! is a game server for the greatest MMORPG of all time, [Dark Age of Camelot](https://darkageofcamelot.com/). This project does not attempt to receate any of the content from the official servers. It's more like a toolkit for creating new games that use the DAoC client.

This project currently requires client version 1.125. I plan to eventually support newer versions, but I'm developing with 1.125 for several reasons. First, that version was around for a very long time and was well supported by the other server emulators that I'm reverse engineering. Second, you used to be able to launch the client to connect to a server emulator using a command like this:

```
start game.dll 127.0.0.1 13013 1 user pass
```	 
	 
At some point, the client stopped respecting the request not to use encryption. Now you have to launch with [DAoCPortal](https://github.com/Dawn-of-Light/DAoCPortal), which performs some kind of voodoo on the running client. DAoCPortal might support 1.126 now, but I don't think it supports 1.127 yet. I may eventually support encryption, but for now it's good old 1.125 and DAoCPortal. (Since DAoCPortal is not open source, I can't vouch for what it does. Use it at your own risk!) Finally, the fact that 1.126 and 1.127 already exist means I won't have to wait for a new version to come out when it's time to test supporting multiple versions.

Unfortunately, you can't download old client versions from Mythic. If you don't already have version 1.125, you might have to, uh... borrow it from a friend. Once you have it, I recommend putting your entire client folder in a Git repository and committing each time a new version comes out. Then you can easily switch between versions. Use this `.gitignore`:

```
logs/
*.myp
*.log
```	
