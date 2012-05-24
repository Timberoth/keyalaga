using UnityEngine;
using System.Collections;

public class WordObject 
{
	public GameObject gameObject;
	public string word;
	
	// Use this for initialization
	public WordObject( GameObject gameObject, string word ) 
	{
		this.gameObject = gameObject;
		this.word = word;
	}
	
	// This defines what this object does when it's word has been matched.
	// This could be different per object.  Maybe it explodes into particles,
	// maybe it rockets into the air, maybe an animation plays, etc.
	public void ReactToMatch()
	{
		// As a test just destroy this object.
		GameObject.Destroy( this.gameObject );
	}
}
