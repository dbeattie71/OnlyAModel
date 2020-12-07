using Core.Event;
using Models.Character;
using System;
using System.Collections.Generic;

namespace MultiPlayer
{
	internal class SessionData
	{
		internal string User { get; set; }
		internal Character SelectedCharacter { get; set; }		
		internal EventHandler<MessageEventArgs> OnMessageReceived { get; set; }
	}
}
