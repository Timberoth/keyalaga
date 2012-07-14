using UnityEngine;
using System.Collections;

public class RunnerWordObject : WordObject {

	// Use this for initialization
	public override void Start() 
	{
		this.rigidbody.SetMaxAngularVelocity(14f);
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update() 
	{
	
	}
	
	public override WordManager.WordDifficulty GetDifficulty()
	{ 		
		return WordManager.WordDifficulty.Easy;		
	}
}
