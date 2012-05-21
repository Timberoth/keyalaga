using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	/*
	 * Singleton Code
	 */	
	public static GameManager instance;
	void Awake(){		
		if(instance == null){
			instance = this;
		}
		else{
			Debug.LogWarning("There should only be one of these");
		}
	}
	
	
	
	/*
	 * Unity Functions 
	 */
	
	// Use this for initialization
	void Start () 
	{
		GameObject bubble = GameObject.Instantiate(Resources.Load("Prefabs/Bubble")) as GameObject;
		bubble.transform.position = new Vector3( 0f, 10f, 0f );
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check for mouse input
		CheckForInput();	
	}
	
	
	/*
	 * Input Functions
	 */
	private void CheckForInput()
	{

	}	
}
