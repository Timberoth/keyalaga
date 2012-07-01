using UnityEngine;
using System.Collections;

public class HUDManager 
{
	public UILabel heightLabel;
	public UILabel maxHeightLabel;
	public UILabel currentWordLabel;
	public UILabel userInputLabel;
	
	// Use this for initialization
	public void Initialize () 
	{
		GameObject heightObject = GameObject.Find("HeightText");
		this.heightLabel = heightObject.GetComponent<UILabel>();
		
		GameObject maxHeightObject = GameObject.Find("MaxHeightText");
		this.maxHeightLabel = maxHeightObject.GetComponent<UILabel>();
		
		GameObject activeWordObject = GameObject.Find("CurrentWordText");
		this.currentWordLabel = activeWordObject.GetComponent<UILabel>();
		
		GameObject userInputObject = GameObject.Find("UserInputText");
		this.userInputLabel = userInputObject.GetComponent<UILabel>();
	}
	
	public void Update () 
	{		
	}
}
