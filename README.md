# Camelot! Camelot! Camelot!
It's Only A Model!&trade;

Only A Model! is a game server for the greatest MMORPG of all time, [Dark Age of Camelot](https://darkageofcamelot.com/).  This project does not attempt to replicate any of the content from the official servers. It's more like a toolkit for experimenting with the DAoC client and creating new games with customized content and mechanics.

Although OAM shares no code with [Dawn of Light](https://github.com/Dawn-of-Light/DOLSharp), it's still a derivative work because I studied DOL extensively when writing it. Therefore, OAM is also licensed under the GPL. A huge amount of credit goes to whoever originally reverse engineered the DAoC client-server protocol for DOL.

This project currently requires client version 1.125. I plan to support newer versions, but I'm developing with 1.125 for several reasons. First, that version was the latest for long time, and therefore was well supported by the other open source servers that I'm using as a reference. Second, you used to be able to launch the client to connect to a server emulator using a command like this:

```
start game.dll 127.0.0.1 13013 1 user pass
```	 
	 
At some point, the client stopped respecting the parameter that says not to use encryption. Now you have to launch with [DAoCPortal](https://github.com/Dawn-of-Light/DAoCPortal), which performs some kind of bit-flipping voodoo to disable encryption in the running client process. DAoCPortal might support 1.126 now, but I don't think it supports 1.127 yet. (Since DAoCPortal is not open source, I can't vouch for what it does. Use it at your own risk!) Finally, the fact that 1.126 and 1.127 already exist means I won't have to wait for a new version to come out when it's time to figure out how to support multiple versions.

Unfortunately, you can't download old client versions from Mythic. If you don't already have version 1.125, you might have to, uh... borrow it from a friend. Once you have it, I recommend putting your entire client folder in a Git repository and committing each time a new patch comes out. Then you can easily switch between versions. Use this `.gitignore`:

```
logs/
*.myp
*.log
```	
