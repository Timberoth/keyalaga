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
	
	public void Awake()
	{		
	}
	
	// Use this for initialization
	public void Start () 
	{		
		this.rigidbody.SetMaxAngularVelocity(14f);
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
	}
	
	// Do something interesting when this object has been matched in game.
	public void ReactToMatch( string newWord )
	{				
		this.word = newWord;
		
		Game.instance.hudManager.currentWordLabel.text = this.word;
		
		if( this.rigidbody.velocity.y < 0.0f )
		{
			this.rigidbody.velocity = Vector3.zero;			
		}
				
		Vector3 force = Vector3.up * MAX_IMPLUSE;
		
		// Heading Right
		if( this.headingRight )
		{
			force.x = UnityEngine.Random.Range(-8,-4);
			this.headingRight = false;
			
			// Give it a little spin
			this.rigidbody.angularVelocity = Vector3.zero;
			this.rigidbody.AddTorque(Vector3.back * -ROTATION_SPEED);
		}
		
		// Heading Left
		else
		{
			force.x = UnityEngine.Random.Range(4,8);
			this.headingRight = true;
			
			// Give it a little spin
			this.rigidbody.angularVelocity = Vector3.zero;
			this.rigidbody.AddTorque(Vector3.back * ROTATION_SPEED);			
		}
		this.rigidbody.AddForce( force, ForceMode.Impulse );
		
		
	}
}
