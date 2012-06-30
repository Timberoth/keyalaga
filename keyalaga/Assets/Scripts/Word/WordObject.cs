using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (Rigidbody))]
public class WordObject : MonoBehaviour
{	
	public string word;
	
	private const float MAX_SPEED = 10f;
	private const float MAX_IMPLUSE = 20f;
	private const float ROTATION_SPEED = 10f;
	
	private float timer = 0f;
	private float updateTimer = 0.1f;
	
	private bool headingRight = true;
	private bool jumping = false;
	
	private exSprite sprite;
	private exSpriteAnimation spriteAnimation;
	
	public void Awake()
	{		
	}
	
	// Use this for initialization
	public void Start () 
	{		
		this.rigidbody.SetMaxAngularVelocity(14f);
		this.sprite = this.GetComponent<exSprite>();
		this.spriteAnimation = this.GetComponent<exSpriteAnimation>();
	}
	
	// Update is called once per frame
	public void Update () 
	{				
		// Update height text
		Game.instance.hudManager.heightLabel.text = Mathf.RoundToInt(this.rigidbody.position.y-1) + " M";
		
		// Cap velocity to ensure the camera can track it
		/*
		Vector3 newVelocity = this.rigidbody.velocity;
		newVelocity.x = Math.Max( newVelocity.x, -MAX_SPEED );
		newVelocity.x = Math.Min( newVelocity.x, MAX_SPEED );
		newVelocity.y = Math.Max( newVelocity.y, -MAX_SPEED );
		newVelocity.y = Math.Min( newVelocity.y, MAX_SPEED );
		this.rigidbody.velocity = newVelocity;
		*/		
	}
	
	public void FixedUpdate()
	{		
		this.timer += Time.fixedDeltaTime;
		
		if( this.timer > this.updateTimer )
		{
			this.rigidbody.angularVelocity *= 1.1f;
			this.timer = 0f;
		}
		
		// Check if we've peaked and should go into a roll
		if( this.jumping )
		{
			// Check if we've peaked
			if( this.rigidbody.velocity.y < 0 )
			{
				Debug.Log("Going into roll");
				this.jumping = false;
				this.spriteAnimation.Play("Cat_Roll");
				
				if( this.headingRight )
				{
					this.rigidbody.AddTorque(Vector3.forward * -ROTATION_SPEED);	
				}
				else
				{
					this.rigidbody.AddTorque(Vector3.forward * ROTATION_SPEED);	
				}
			}
		}
	}
	
	// Do something interesting when this object has been matched in game.
	public void ReactToMatch( string newWord )
	{				
		this.word = newWord;
		
		Game.instance.hudManager.currentWordLabel.text = this.word;
		
		Jump();
	}
	
	private void Jump()
	{
		// Zero out y velocity if we're falling.
		if( this.rigidbody.velocity.y < 0.0f )
		{
			this.rigidbody.velocity = Vector3.zero;			
		}
		
		// Zero out the x velocity so the impluse doesn't cancel itself out
		this.rigidbody.velocity = new Vector3(0f, this.rigidbody.velocity.y, 0f);
				
		Vector3 force = Vector3.up * MAX_IMPLUSE;
		
		// Heading Right, want to blast left
		if( this.headingRight )
		{
			force.x = UnityEngine.Random.Range(-10,-6);
			this.headingRight = false;
			
			// Kill rotation
			this.rigidbody.angularVelocity = Vector3.zero;
			
			// When heading left scale should be positive
			this.sprite.scale = new Vector2(Mathf.Abs(this.sprite.scale.x), this.sprite.scale.y);
		}
		
		// Heading Left, want to blast right
		else
		{
			force.x = UnityEngine.Random.Range(6,10);
			this.headingRight = true;
			
			// Kill rotation
			this.rigidbody.angularVelocity = Vector3.zero;
						
			// When heading right scale should be negative
			this.sprite.scale = new Vector2(-Mathf.Abs(this.sprite.scale.x), this.sprite.scale.y);
		}
				
		this.rigidbody.AddForce( force, ForceMode.Impulse );
		
		// Go back to base rotation so cat looks like it's jumping upward
		this.rigidbody.transform.eulerAngles = Vector3.zero;
		
		// Play sound FX
		
		// Play particle
		
		// Play animation		
		this.spriteAnimation.Play("Cat_Jump");		
		
		this.jumping = true;		
	}
}
