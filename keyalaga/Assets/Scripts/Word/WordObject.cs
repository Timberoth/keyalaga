using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (Rigidbody))]
public class WordObject : MonoBehaviour
{	
	public string word;
	
	private const float MAX_SPEED = 10f;
	private const float MAX_IMPLUSE = 20f;
	
	private bool headingRight = true;
	
	public void Awake()
	{		
	}
	
	// Use this for initialization
	public void Start () 
	{			
	}
	
	// Update is called once per frame
	public void Update () 
	{				
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
	
	// Do something interesting when this object has been matched in game.
	public void ReactToMatch( string newWord )
	{				
		this.word = newWord;
		
		if( this.rigidbody.velocity.y < 0.0f )
		{
			this.rigidbody.velocity = Vector3.zero;			
		}
				
		Vector3 force = this.transform.up * MAX_IMPLUSE;
		
		if( this.headingRight )
		{
			force.x = UnityEngine.Random.Range(-6,-3);
			this.headingRight = false;
		}
		else
		{
			force.x = UnityEngine.Random.Range(3,6);
			this.headingRight = true;
		}
		this.rigidbody.AddForce( force, ForceMode.Impulse );
	}
}
