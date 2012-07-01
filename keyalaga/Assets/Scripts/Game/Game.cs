using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public InputManager inputManager;
	public HUDManager hudManager;
	public WordManager wordManager;
	public CameraManager cameraManager;
	public AudioManager audioManager;
	
	public int lives = 3;
	
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
		
		this.hudManager = new HUDManager();
		this.hudManager.Initialize();
		
		this.wordManager = new WordManager();
		this.wordManager.Initialize();
		
		this.cameraManager = new CameraManager();
		this.cameraManager.Initialize();
		
		this.audioManager = new AudioManager();
		this.audioManager.Initialize();
		
		
		GameObject character = GameObject.FindGameObjectWithTag("Character");
		WordObject wordObject = character.GetComponent<WordObject>();
		this.wordManager.AddWordObject( wordObject );
		
		// Update the current word text for the 1st frame
		this.hudManager.currentWordLabel.text = wordObject.word;
		this.hudManager.userInputLabel.text = "";
		
		// CameraManager needs tracking ref
		this.cameraManager.AddTrackingObject( character );
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.inputManager.Update();
		this.hudManager.Update();
		this.cameraManager.Update();
		this.audioManager.Update();
	}
}
