using Core.Event;
using Models.Character;
using System;
using System.Collections.Generic;

namespace MultiPlayer
{
	internal class SessionState
	{
		internal List<Character> Characters { get; } = new List<Character>(new Character[30]);
		internal Character SelectedCharacter { get; set; }
		
		internal EventHandler<MessageEventArgs> OnMessageReceived;

		internal SessionState(EventHandler<MessageEventArgs> handler)
		{
			OnMessageReceived = handler;
		}
	}
}
