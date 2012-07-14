using UnityEngine;
using System.Collections;

public class RunnerWordObject : WordObject {

	// Use this for initialization
	public override void Start() 
	{
		this.MAX_IMPLUSE = 6f;
		this.MAX_SPEED = 50f;
		
		this.rigidbody.SetMaxAngularVelocity(14f);
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update() 
	{
		// Calculate distanceTraveled
		this.distanceTraveled += this.rigidbody.velocity.x * Time.deltaTime;
		int roundeddistanceTraveled = Mathf.RoundToInt(this.distanceTraveled);
		
		// Update distanceTraveled text
		Game.instance.hudManager.distanceTraveledLabel.text = roundeddistanceTraveled + "M";
		
		// Update max distanceTraveled text
		if( distanceTraveled > this.maxDistanceTraveled )
		{
			this.maxDistanceTraveled = roundeddistanceTraveled;			
			Game.instance.hudManager.maxDistanceTraveledLabel.text = "Max distanceTraveled: "+this.maxDistanceTraveled.ToString()+"M";
		}
				
		/*
		// Check for end game conditions
		if( roundeddistanceTraveled >= 1000f )
		{
			Debug.Log("END GAME");
			
			// TODO Pop up score screen, give players chance to retry
			
			// TEMP HACK - reload screen
			Application.LoadLevel("Sandbox");
		}
		*/		
	}
	
	public override void FixedUpdate()
	{
		this.wrapTimer.x += Time.fixedDeltaTime;
		this.wrapTimer.y += Time.fixedDeltaTime;
		
		this.timer += Time.fixedDeltaTime;
		
		// We only want to increase the spin speed after a certain bit of time
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
				this.jumping = false;
				this.spriteAnimation.Play("Cat_Roll");
				
				if( this.rigidbody.velocity.x > 0f )
				{
					this.rigidbody.AddTorque(Vector3.forward * -ROTATION_SPEED);	
				}
				else
				{
					this.rigidbody.AddTorque(Vector3.forward * ROTATION_SPEED);	
				}
				
				// Slow gravity depending on the difficulty
				Game.instance.SetGravity( new Vector3(0f, -4.9f, 0f) );			
			}
		}
		
		// Cap X velocity
		if( this.rigidbody.velocity.x >= this.MAX_SPEED )
		{
			Vector3 newVelocity = this.rigidbody.velocity;
			newVelocity.x = this.MAX_SPEED;		
			this.rigidbody.velocity = newVelocity;					
		}
		
		HandleScreenWrapping();
	}
	
	public override void HandleScreenWrapping()
	{
		Vector2 tileSize = Game.instance.backgroundManager.backgroundTileSize;
		
		// X Wrapping
		if( this.wrapTimer.x > 0.25 && 
			(this.rigidbody.position.x <= -tileSize.x || this.rigidbody.position.x >= tileSize.x) )
		{
			Vector3 newPosition = this.rigidbody.position;
			newPosition.x *= -1;
			this.rigidbody.position = newPosition;
			
			this.wrapTimer.x = 0f;
		}
		
		// Y Capping
		if( this.rigidbody.position.y >= tileSize.y )
		{
			Vector3 newPosition = this.rigidbody.position;
			newPosition.y = tileSize.y;
			this.rigidbody.position = newPosition;			
		}			
	}
	
	public override void Jump()
	{			
		// Counter act gravity to create the jump effect
		this.rigidbody.velocity = new Vector3(this.rigidbody.velocity.x, 9.8f, 0f);
		
		Vector3 force = Vector3.one * MAX_IMPLUSE;
		force.z = 0;
		
		this.rigidbody.AddForce( force, ForceMode.Impulse );
		
		// Go back to base rotation so cat looks like it's jumping upward
		this.rigidbody.transform.eulerAngles = Vector3.zero;
		
		// Play sound FX
		Game.instance.audioManager.PlaySoundEffect("super_jump");
		
		// Play particle
		
		// Play animation		
		this.spriteAnimation.Play("Cat_Jump");		
		
		this.jumping = true;
		
		Game.instance.SetGravity( new Vector3(0f, -9.81f, 0f) );
		
		// Restore Time to normal if we're in slow mode
		Game.instance.SlowTime(false);	
	}
	
	public override WordManager.WordDifficulty GetDifficulty()
	{ 		
		return WordManager.WordDifficulty.Easy;		
	}
}
