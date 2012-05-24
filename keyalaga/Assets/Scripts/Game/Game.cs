using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public InputManager inputManager;
	public UIManager uiManager;
	public WordManager wordManager;
	
	
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
		GameObject bubble = GameObject.Instantiate(Resources.Load("Prefabs/Bubble")) as GameObject;
		bubble.transform.position = new Vector3( 0f, 10f, 0f );
		
		this.inputManager = new InputManager();
		this.inputManager.Initialize();
		
		this.uiManager = new UIManager();
		this.uiManager.Initialize();
		
		this.wordManager = new WordManager();
		this.wordManager.Initialize();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.inputManager.Update();
		this.uiManager.Update();		
	}
}
