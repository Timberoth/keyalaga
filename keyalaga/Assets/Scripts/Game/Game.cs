using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public InputManager inputManager;
	public UIManager uiManager;
	public WordManager wordManager;
	public CameraManager cameraManager;		
	
	// Singleton Code
	public static Game instance;
	void Awake(){		
		if(instance == null){
			instance = this;
		}
		else{
			Debug.LogWarning("There should only be one of these");
		}
	}
	
	
	
	// Use this for initialization
	void Start () 
	{		
		this.inputManager = new InputManager();
		this.inputManager.Initialize();
		
		this.uiManager = new UIManager();
		this.uiManager.Initialize();
		
		this.wordManager = new WordManager();
		this.wordManager.Initialize();
		
		this.cameraManager = new CameraManager();
		this.cameraManager.Initialize();
		
		GameObject ball = GameObject.Find("Ball");	
		WordObject wordObject = ball.GetComponent<WordObject>();
		this.wordManager.AddWordObject( wordObject );
		
		// Update the current word text for the 1st frame
		this.uiManager.currentWordLabel.text = wordObject.word;
		this.uiManager.userInputLabel.text = "";
		
		// CameraManager needs tracking ref
		this.cameraManager.AddTrackingObject( ball );
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.inputManager.Update();
		this.uiManager.Update();
		this.cameraManager.Update();
	}
}
