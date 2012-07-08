using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public InputManager inputManager;
	public HUDManager hudManager;
	public WordManager wordManager;
	public CameraManager cameraManager;
	public AudioManager audioManager;
	public BackgroundManager backgroundManager;
	
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
	private void Start () 
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
		
		this.backgroundManager = new BackgroundManager();
		this.backgroundManager.Initialize();
		
		GameObject character = GameObject.FindGameObjectWithTag("Character");
		WordObject wordObject = character.GetComponent<WordObject>();
		this.wordManager.AddWordObject( wordObject );
		
		// Update the current word text for the 1st frame
		this.hudManager.currentWordLabel.text = wordObject.word;
		this.hudManager.userInputLabel.text = "";
		
		// CameraManager needs tracking ref
		this.cameraManager.AddTrackingObject( character );
		
		// Disable gravity until the first word is match
		SetGravity( Vector3.zero );
	}
	
	// Update is called once per frame
	private void Update () 
	{
		this.inputManager.Update();
		this.hudManager.Update();
		this.cameraManager.Update();
		this.audioManager.Update();
	}
	
	public void SetGravity( Vector3 newGravity )
	{
		Physics.gravity = newGravity;	
	}	
	
	// Slow down time, calling without args will disable slow time
	public void SlowTime( bool enable )
	{		
		// Default value is 1/60 0.016666		
		if( enable )
		{
			Time.timeScale = 0.5f;
			Time.fixedDeltaTime = 0.01666f * (1f/(1f-Time.timeScale));
		}
		else
		{
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.01666f;
		}		
	}
}
