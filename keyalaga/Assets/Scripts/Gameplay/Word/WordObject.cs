using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (Rigidbody))]
public class WordObject : MonoBehaviour
{	
	public string word;
	
	// Distance traveled from the starting spot.
	public float distanceTraveled = 0f;
	public int maxDistanceTraveled = 0;
	
	public float MAX_SPEED = 10f;
	public float MAX_IMPLUSE = 20f;
	public float ROTATION_SPEED = 20f;
	
	protected float timer = 0f;
	protected float updateTimer = 0.1f;	
	protected bool jumping = false;
	
	// Used to prevent the object from getting caught between wrapping back and 
	// forth.  Without a slight buffer time the object would bounce back and forth
	// between sides.
	protected Vector2 wrapTimer = new Vector2(0f,0f);
	
	protected exSprite sprite;
	protected exSpriteAnimation spriteAnimation;
	
	public void Awake(){}
	
	// Use this for initialization
	public virtual void Start() 
	{				
		this.sprite = this.GetComponent<exSprite>();
		this.spriteAnimation = this.GetComponent<exSpriteAnimation>();
	}
	
	// Update is called once per frame
	public virtual void Update(){}
	

	public virtual void FixedUpdate(){}
	
	// Do something interesting when this object has been matched in game.
	public virtual void ReactToMatch( string newWord )
	{				
		this.word = newWord;
		
		Game.instance.hudManager.currentWordLabel.text = this.word;
		
		Jump();
	}
	
	public virtual void HandleScreenWrapping()
	{
		Vector2 tileSize = Game.instance.backgroundManager.backgroundTileSize;
		// Check for wrap conditions
		if( this.wrapTimer.x > 0.25 && 
			(this.rigidbody.position.x <= -tileSize.x || this.rigidbody.position.x >= tileSize.x) )
		{
			Vector3 newPosition = this.rigidbody.position;
			newPosition.x *= -1;
			this.rigidbody.position = newPosition;
			
			this.wrapTimer.x = 0f;
		}
		
		if( this.wrapTimer.y > 0.25 && 
			(this.rigidbody.position.y <= -tileSize.y || this.rigidbody.position.y >= tileSize.y) )
		{
			Vector3 newPosition = this.rigidbody.position;
			newPosition.y *= -1;
			this.rigidbody.position = newPosition;
			
			this.wrapTimer.y = 0f;
		}			
	}
	
	public virtual void Jump(){}
	
	// Determine the difficulty of the word based the game type.
	// For FlyerWordObjects difficulty is based on how high it is,
	// For RunnerWordObject it's based on hos fast it is.
	public virtual WordManager.WordDifficulty GetDifficulty(){ return WordManager.WordDifficulty.Easy; }
}
