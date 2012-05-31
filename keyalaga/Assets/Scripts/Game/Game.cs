using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public InputManager inputManager;
	public UIManager uiManager;
	public WordManager wordManager;
	public GameObject gameCamera;
	
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
		
		GameObject bubble = GameObject.Find("Bubble");	
		WordObject wordObject = bubble.GetComponent<WordObject>();
		this.wordManager.AddWordObject( wordObject );
		
		this.gameCamera = GameObject.Find("Main Camera");		
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.inputManager.Update();
		this.uiManager.Update();		
	}
}
