using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class WordObject : MonoBehaviour
{	
	public string word;
	
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
			this.rigidbody.velocity = Vector3.zero;					
		
		this.rigidbody.AddForce( this.transform.up * 8, ForceMode.Impulse );
	}
}
