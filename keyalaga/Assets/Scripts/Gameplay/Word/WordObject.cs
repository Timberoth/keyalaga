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
	
	public const float MAX_SPEED = 10f;
	public const float MAX_IMPLUSE = 20f;
	public const float ROTATION_SPEED = 20f;
	
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
	
	public virtual void Jump(){}
	
	// Determine the difficulty of the word based the game type.
	// For FlyerWordObjects difficulty is based on how high it is,
	// For RunnerWordObject it's based on hos fast it is.
	public virtual WordManager.WordDifficulty GetDifficulty(){ return WordManager.WordDifficulty.Easy; }
}
