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
		
		// Swap in the proper UI and Font atlases based on the device resolution
		SetupAtlases();
	}
	
	// Update is called once per frame
	private void Update () 
	{
		this.inputManager.Update();
		this.wordManager.Update();
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
	
	
	// Swap in the proper UI and Font atlases based on screen resolution
	private void SetupAtlases()
	{				
		GameObject referenceFontObject = (GameObject)Resources.Load("Art/Fonts/ArialFont-Reference");
		UIFont referenceFont = referenceFontObject.GetComponent<UIFont>();
				
		GameObject properFontObject = (GameObject)Resources.Load(GetProperFontPrefab());
		UIFont properFont = properFontObject.GetComponent<UIFont>();
		
		referenceFont.replacement = properFont;
	}
	
	// This is based on landscape orientation
	// TODO Specially support Android resolutions instead of
	// assuming they fall into the same basic resolution categories
	private string GetProperFontPrefab()
	{		
		if( Screen.height >= 1536 )
			return "Art/Fonts/ArialFont-iPadHD";
		else if( Screen.height >= 768 )
			return "Art/Fonts/ArialFont-HD";
		else
			return "Art/Fonts/ArialFont-SD";
	}
}
