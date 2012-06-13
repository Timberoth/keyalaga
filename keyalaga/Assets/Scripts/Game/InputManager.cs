using UnityEngine;
using System.Collections.Generic;

public class InputManager 
{
	string inputBuffer;
	
	// Use this for initialization
	public void Initialize () 
	{
		
	}
	
	public void Update () 
	{
		if( Input.inputString.Length <= 0 )
			return;
		
		
		// For now you can only delete a single character per frame.
		// This might have to be time buffered a bit to prevent multiple
		// characters getting deleted by a single press.
		
		// ASSUMPTION: The backspace key is the one pressed this frame.
		if( Input.GetKey(KeyCode.Backspace) )
		{
			// Delete the last key pressed
			if( this.inputBuffer.Length > 1 )
				this.inputBuffer = this.inputBuffer.Remove(this.inputBuffer.Length-1);
			else if( this.inputBuffer.Length == 1 )
				this.inputBuffer = "";
			
			Game.instance.hudManager.userInputLabel.text = this.inputBuffer;
			
			// Don't do any more processing this frame.
			return;
		}
		
		
		// Add to the buffer
		this.inputBuffer += Input.inputString;
		
		// Look for enter/return key to check for matches and empty the inputBuffer.
		if( Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return) )
		{
			// Strip off the return character
			this.inputBuffer = this.inputBuffer.Remove( this.inputBuffer.Length-1 );
			
			// Search through the words for matches.
			Game.instance.wordManager.CheckForMatches( this.inputBuffer );
			
			this.inputBuffer = "";
		}
		
		Game.instance.hudManager.userInputLabel.text = this.inputBuffer;
	}
}
