using UnityEngine;
using System.Collections;

public class UIManager 
{
	public UILabel heightLabel;
	public UILabel currentWordLabel;
	public UILabel userInputLabel;
	
	// Use this for initialization
	public void Initialize () 
	{
		GameObject heightObject = GameObject.Find("HeightText");
		heightLabel = heightObject.GetComponent<UILabel>();
		
		GameObject activeWordObject = GameObject.Find("CurrentWordText");
		currentWordLabel = activeWordObject.GetComponent<UILabel>();
		
		GameObject userInputObject = GameObject.Find("UserInputText");
		userInputLabel = userInputObject.GetComponent<UILabel>();
	}
	
	public void Update () 
	{		
	}
}
