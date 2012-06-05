using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof (Rigidbody))]
public class WordObject : MonoBehaviour
{	
	public string word;
	
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
	}
	
	// Do something interesting when this object has been matched in game.
	public void ReactToMatch()
	{				
		if( this.rigidbody.velocity.y < 0.0f )		
		{
			this.rigidbody.velocity = Vector3.zero;			
		}
		
		// TODO Need to cap velocity at some value
		
		Vector3 force = this.transform.up*8;
		
		if( this.headingRight )
		{
			force.x = UnityEngine.Random.Range(-4,-2);
			this.headingRight = false;
		}
		else
		{
			force.x = UnityEngine.Random.Range(2,4);
			this.headingRight = true;
		}
		this.rigidbody.AddForce( force, ForceMode.Impulse );			
	}
}
