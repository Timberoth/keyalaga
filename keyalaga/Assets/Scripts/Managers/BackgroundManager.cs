using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager 
{
	// Keep a ref to an individual background Tile prefab so we can pull information from it.
	private GameObject backgroundTile;
	
	// This is the actual background which is a prefab made of several background tiles.
	private GameObject background;
	
	// This is used to determine how background tiles are spaced out in the world.
	// These values are in world space
	public Vector2 backgroundTileSize;
	
	// Use this for initialization
	public void Initialize() 
	{		
		this.backgroundTile = (GameObject)Resources.Load("Prefabs/Background/SkyTile2");
		this.background = GameObject.Find("SkyWall2");
					
		// Calculate the background tile size, so we can dynamically position the backgrounds
		this.backgroundTileSize = new Vector2();
		exSprite sprite = this.backgroundTile.GetComponent<exSprite>();
		this.backgroundTileSize.x = sprite.scale.x * sprite.width;
		this.backgroundTileSize.y = sprite.scale.y * sprite.height;
	}	
}
