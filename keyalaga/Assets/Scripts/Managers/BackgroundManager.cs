using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager 
{
	// Keep a ref to the main camera
	private Camera gameCamera;
	
	// Keep a ref to the background Tile prefab so we can instantiate them
	private GameObject backgroundTile;
	
	// Keep a list of background tiles in use
	private List<GameObject> backgroundTiles;
	
	// This is used to determine how background tiles are spaced out in the world.
	// These values are in world space
	private Vector2 backgroundTileSize;
	
	// This is used to determine the camera boundaries and detect when new background tiles
	// need to be spawned in or when background tiles need to clean themselves up.
	// This is the distance between the camera's position and the upper right boundary.
	private Vector2 cameraBoundaryOffsets;	
	
	// Array used every frame to calculate the camera's corner points
	// Index starts with lower left in clock wise direction
	private Vector2[] cameraCorners = new Vector2[4];
	
	// Use this for initialization
	public void Initialize() 
	{		
		this.backgroundTiles = new List<GameObject>();
		
		this.backgroundTile = (GameObject)Resources.Load("Prefabs/Background/Sky2");		
		GameObject startingTile = (GameObject)GameObject.Instantiate( this.backgroundTile );
		this.backgroundTiles.Add( startingTile );
		
		GameObject gameCameraObject = GameObject.Find("Main Camera");
		this.gameCamera = gameCameraObject.GetComponent<Camera>();
		
		// Calculate the camera extents
		Vector3 center = this.gameCamera.ScreenToWorldPoint( new Vector3( Screen.width/2f, Screen.height/2f, 0f) );
		Vector3 upperRight = this.gameCamera.ScreenToWorldPoint( new Vector3(Screen.width, Screen.height, 0f) );
				
		// Want to increase the offset size a little bigger than the camera view so that we can
		// spawn in background tiles before the camera sees them.
		//this.cameraBoundaryOffsets = 1.2f * (upperRight-center);
		this.cameraBoundaryOffsets = 1.2f * (upperRight-center);
				
		// Calculate the background tile size, so we can dynamically position the backgrounds
		this.backgroundTileSize = new Vector2();
		exSprite sprite = this.backgroundTile.GetComponent<exSprite>();
		this.backgroundTileSize.x = sprite.scale.x * sprite.width;
		this.backgroundTileSize.y = sprite.scale.y * sprite.height;
	}
	
	// Update is called once per frame
	public void Update() 
	{
		// Calculate the camera extents corner points
		
		// Lower left
		this.cameraCorners[0] = new Vector2( this.gameCamera.transform.position.x - this.cameraBoundaryOffsets.x,
			this.gameCamera.transform.position.y - this.cameraBoundaryOffsets.y );
		
		// Upper left
		this.cameraCorners[1] = new Vector2( this.gameCamera.transform.position.x - this.cameraBoundaryOffsets.x, 
			this.gameCamera.transform.position.y + this.cameraBoundaryOffsets.y );
		
		// Upper right
		this.cameraCorners[2] = new Vector2( this.gameCamera.transform.position.x + this.cameraBoundaryOffsets.x,
			this.gameCamera.transform.position.y + this.cameraBoundaryOffsets.y );
		
		// Lower Right
		this.cameraCorners[3] = new Vector2( this.gameCamera.transform.position.x + this.cameraBoundaryOffsets.x,
			this.gameCamera.transform.position.y - this.cameraBoundaryOffsets.y );
		
		// See if any of the existing tiles contain the corner points.  
		// If not we need to spawn a new tile and align it properly.
		bool lowerLeftContained = IsPointOnAnyTile( this.cameraCorners[0] );
		bool upperLeftContained = IsPointOnAnyTile( this.cameraCorners[1] );
		bool upperRightContained = IsPointOnAnyTile( this.cameraCorners[2] );
		bool lowerRightContained = IsPointOnAnyTile( this.cameraCorners[3] );
		
		/*
		// Left side not contained
		if( !lowerLeftContained && !upperLeftContained )
		{
			// Spawn tile to the left
			Vector2 midPoint = (this.cameraCorners[1]+this.cameraCorners[0])/2f;			
			
			int roundedCameraX = Mathf.RoundToInt( midPoint.x );
			int roundedTileSizeX = Mathf.RoundToInt(this.backgroundTileSize.x);
			int newTileX = roundedCameraX + ( roundedCameraX % roundedTileSizeX );
			
			int roundedCameraY = Mathf.RoundToInt( midPoint.y );
			int roundedTileSizeY = Mathf.RoundToInt(this.backgroundTileSize.y);
			int newTileY = roundedCameraY - ( roundedCameraY % roundedTileSizeY );
			
			// Create new tile at newTile X/Y
			GameObject newTile = (GameObject)GameObject.Instantiate(this.backgroundTile, new Vector3(newTileX, newTileY, 0f), Quaternion.identity);
			this.backgroundTiles.Add( newTile );
		}		
		
		// Right side not contained
		if( !lowerRightContained && !upperRightContained )
		{
			// Spawn tile to the right
			Vector2 midPoint = (this.cameraCorners[2]+this.cameraCorners[3])/2f;			
			
			int roundedCameraX = Mathf.RoundToInt( midPoint.x );
			int roundedTileSizeX = Mathf.RoundToInt(this.backgroundTileSize.x);
			int newTileX = roundedCameraX + ( roundedCameraX % roundedTileSizeX );
			
			int roundedCameraY = Mathf.RoundToInt( midPoint.y );
			int roundedTileSizeY = Mathf.RoundToInt(this.backgroundTileSize.y);
			int newTileY = roundedCameraY - ( roundedCameraY % roundedTileSizeY );
			
			// Create new tile at newTile X/Y
			GameObject newTile = (GameObject)GameObject.Instantiate(this.backgroundTile, new Vector3(newTileX, newTileY, 0f), Quaternion.identity);
			this.backgroundTiles.Add( newTile );
		}
		*/	
		
		// Top not contained
		if( !upperLeftContained && !upperRightContained )
		{
			// Spawn tile on top
			Vector2 midPoint = (this.cameraCorners[1]+this.cameraCorners[2])/2f;
			
			int roundedCameraX = Mathf.RoundToInt( midPoint.x );
			int roundedTileSizeX = Mathf.RoundToInt(this.backgroundTileSize.x);
			int newTileX = roundedCameraX + ( roundedCameraX % roundedTileSizeX );
			
			int roundedCameraY = Mathf.RoundToInt( midPoint.y );
			int roundedTileSizeY = Mathf.RoundToInt(this.backgroundTileSize.y);
			int newTileY = roundedCameraY - ( roundedCameraY % roundedTileSizeY ) + roundedTileSizeY;
			
			// Create new tile at newTile X/Y
			GameObject newTile = (GameObject)GameObject.Instantiate(this.backgroundTile, new Vector3(0f, newTileY, 0f), Quaternion.identity);
			this.backgroundTiles.Add( newTile );
		}	
		
		
		// Bottom not contained
		if( !lowerLeftContained && !lowerRightContained )
		{
			// Spawn tile on the bottom
			Vector2 midPoint = (this.cameraCorners[0]+this.cameraCorners[3])/2f;
			
			int roundedCameraX = Mathf.RoundToInt( midPoint.x );
			int roundedTileSizeX = Mathf.RoundToInt(this.backgroundTileSize.x);
			int newTileX = roundedCameraX + ( roundedCameraX % roundedTileSizeX );
			
			int roundedCameraY = Mathf.RoundToInt( midPoint.y );
			int roundedTileSizeY = Mathf.RoundToInt(this.backgroundTileSize.y);
			int newTileY = roundedCameraY - ( roundedCameraY % roundedTileSizeY ) - roundedTileSizeY;
			
			// Create new tile at newTile X/Y
			GameObject newTile = (GameObject)GameObject.Instantiate(this.backgroundTile, new Vector3(0f, newTileY, 0f), Quaternion.identity);
			this.backgroundTiles.Add( newTile );
		}	
	}
	
	// Helper function to check if this point exists on any background tile
	private bool IsPointOnAnyTile( Vector2 point )
	{		
		foreach( GameObject tile in this.backgroundTiles )
		{
			if( DoesTileContainPoint( tile, point ) )
			{
				return true;
			}			
		}		
		return false;
	}
	
	private bool DoesTileContainPoint( GameObject tile, Vector2 point )
	{		
		float leftBounds = tile.transform.position.x - this.backgroundTileSize.x/2f;
		float rightBounds = tile.transform.position.x + this.backgroundTileSize.x/2f;
		float upperBounds = tile.transform.position.y + this.backgroundTileSize.y/2f;
		float lowerBounds = tile.transform.position.y - this.backgroundTileSize.y/2f;
		
		if( point.x >= leftBounds && point.x <= rightBounds && point.y >= lowerBounds && point.y <= upperBounds )
			return true;
		else
			return false;	
	}
}
