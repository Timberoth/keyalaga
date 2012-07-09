using UnityEngine;
using System.Collections.Generic;

public class InputManager 
{
	string inputBuffer;
	
	TouchScreenKeyboard keyboard;
	
	// Use this for initialization
	public void Initialize () 
	{		
#if UNITY_IPHONE || UNITY_ANDROID
		// Open the on screen keyboard
		TouchScreenKeyboard.hideInput = true;
		TouchScreenKeyboard.autorotateToLandscapeLeft = true;
		TouchScreenKeyboard.autorotateToLandscapeRight = true;
		this.keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, true);		
#endif
	}
	
	public void Update () 
	{
#if UNITY_EDITOR
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
		
#elif UNITY_IPHONE || UNITY_ANDROID
		
		// The return key was pressed
		if( this.keyboard.done )
		{			
			// Keep the keyboard up the entire time
			this.keyboard.active = true;
			
			// Search through the words for matches.
			Game.instance.wordManager.CheckForMatches( this.keyboard.text );
			
			// Clear the text
			this.keyboard.text = "";
		}
		
		Game.instance.hudManager.userInputLabel.text = this.keyboard.text.ToLower();
#endif
	}
}
