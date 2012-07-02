using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager 
{
	// Keep a ref to the main camera
	private Camera gameCamera;
	
	// Need 4 backgrounds to be able tile properly
	private List<GameObject> backgroundTiles;
	
	private float backgroundSizeX;
	private float backgroundSizeY;
	
	// Use this for initialization
	public void Initialize() 
	{
		this.backgroundTiles = new List<GameObject>(4);
		
		GameObject background = (GameObject)Resources.Load("Prefabs/Backgrounds/Sky2");
		this.backgroundTiles.Add( background );
		
		background = (GameObject)Resources.Load("Prefabs/Backgrounds/Sky2");
		this.backgroundTiles.Add( background );

		background = (GameObject)Resources.Load("Prefabs/Backgrounds/Sky2");
		this.backgroundTiles.Add( background );
		
		background = (GameObject)Resources.Load("Prefabs/Backgrounds/Sky2");
		this.backgroundTiles.Add( background );
		
		GameObject gameCameraObject = GameObject.Find("Main Camera");
		this.gameCamera = gameCameraObject.GetComponent<Camera>();		
	}
	
	// Update is called once per frame
	public void Update() 
	{
	
	}
}
