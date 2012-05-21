using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	private InputManager inputManager;
	private UIManager uiManager;
	
	
	// Singleton Code
	public static GameManager instance;
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
		this.uiManager = new UIManager();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.inputManager.Update();
		this.uiManager.Update();
	}
}
