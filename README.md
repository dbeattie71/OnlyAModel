# Camelot! Camelot! Camelot!
It's Only A Model!&trade;

Only A Model! is a game server for the greatest MMORPG of all time, [Dark Age of Camelot](https://darkageofcamelot.com/). This project does not attempt to replicate any of the content from the official servers. It's more like a toolkit for experimenting with the DAoC client and creating new games with customized content and mechanics.

Although OAM shares no code with [Dawn of Light](https://github.com/Dawn-of-Light/DOLSharp), it's still a derivative work because I studied DOL extensively when writing it. Therefore, OAM is also licensed under the GPL. A huge amount of credit goes to whoever originally reverse engineered the DAoC message protocol for DOL.

This project currently requires client version 1.125. I plan to support newer versions, but I'm developing with 1.125 because that version was the latest for long time and was well supported by DOL and other projects that I'm using for reference. Unfortunately, you can't download old client versions from Mythic. If you don't already have version 1.125, you might have to, uh... borrow it from a friend. Once you have it, I recommend putting your entire client folder in a Git repository and committing each time a new patch comes out. Then you can easily switch between versions. Use this `.gitignore`:

```
logs/
*.myp
*.log
```	

## Quick Start

The `Demo` project is a minimalist example of how to use the `Core` and `Protocol` libraries. You can log in and run around, but not much else. The server listens on the local loopback address, port 13013.

## Disabling Encryption

You used to be able to connect to an unofficial server by running a command like this:

```
start game.dll 127.0.0.1 13013 1 user pass
```	 
	 
At some point around version 1.115, the client stopped respecting the parameter that tells it not to use encryption. Now you have to launch the client with [DAoCPortal](https://github.com/Dawn-of-Light/DAoCPortal), which disables encryption by patching the running process. Since DAoCPortal is not open source, I can't vouch for exactly what it does.