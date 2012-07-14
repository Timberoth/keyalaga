using UnityEngine;
using System.Collections;

public class HUDManager 
{
	public UILabel distanceTraveledLabel;
	public UILabel maxDistanceTraveledLabel;
	public UILabel currentWordLabel;
	public UILabel userInputLabel;
	public UILabel comboLabel;
	
	// Use this for initialization
	public void Initialize () 
	{
		GameObject gameObj = GameObject.Find("DistanceTraveledText");
		this.distanceTraveledLabel = gameObj.GetComponent<UILabel>();
		
		gameObj = GameObject.Find("MaxDistanceTraveledText");
		this.maxDistanceTraveledLabel = gameObj.GetComponent<UILabel>();
		
		gameObj = GameObject.Find("CurrentWordText");
		this.currentWordLabel = gameObj.GetComponent<UILabel>();
		
		gameObj = GameObject.Find("UserInputText");
		this.userInputLabel = gameObj.GetComponent<UILabel>();
		
		gameObj = GameObject.Find("ComboText");
		this.comboLabel = gameObj.GetComponent<UILabel>();
	}
	
	public void Update () 
	{		
	}
}
