using UnityEngine;
using System.Collections;

public class FlyerWordObject : WordObject {

	// Use this for initialization
	public override void Start () 
	{
		this.rigidbody.SetMaxAngularVelocity(14f);
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		// Calculate distanceTraveled
		this.distanceTraveled += this.rigidbody.velocity.y * Time.deltaTime;
		int roundeddistanceTraveled = Mathf.RoundToInt(this.distanceTraveled);
		
		// Update distanceTraveled text
		Game.instance.hudManager.distanceTraveledLabel.text = roundeddistanceTraveled + "M";
		
		// Update max distanceTraveled text
		if( distanceTraveled > this.maxDistanceTraveled )
		{
			this.maxDistanceTraveled = roundeddistanceTraveled;			
			Game.instance.hudManager.maxDistanceTraveledLabel.text = "Max distanceTraveled: "+this.maxDistanceTraveled.ToString()+"M";
		}
				
		
		// Check for end game conditions
		if( roundeddistanceTraveled <= -1 )
		{
			Debug.Log("END GAME");
			
			// TODO Pop up score screen, give players chance to retry
			
			// TEMP HACK - reload screen
			Application.LoadLevel("Sandbox");
		}		
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
		
		// Falling
		else
		{
			// Cap our falling velocity Y		
			Vector3 newVelocity = this.rigidbody.velocity;
			newVelocity.y = Mathf.Max( this.rigidbody.velocity.y, -15f );		
			this.rigidbody.velocity = newVelocity;			
		}
		
		
		// Check for wrap conditions
		if( this.wrapTimer.x > 0.25 && 
			(this.rigidbody.position.x <= -20f || this.rigidbody.position.x >= 20f) )
		{
			Vector3 newPosition = this.rigidbody.position;
			newPosition.x *= -1;
			this.rigidbody.position = newPosition;
			
			this.wrapTimer.x = 0f;
		}
		
		if( this.wrapTimer.y > 0.25 && 
			(this.rigidbody.position.y <= -30f || this.rigidbody.position.y >= 30f) )
		{
			Vector3 newPosition = this.rigidbody.position;
			newPosition.y *= -1;
			this.rigidbody.position = newPosition;
			
			this.wrapTimer.y = 0f;
		}		
	}
	
	public override void Jump()
	{
		bool headingRight = (this.rigidbody.velocity.x > 0f);
		
		// Zero out y velocity if we're falling.
		if( this.rigidbody.velocity.y < 0.0f )
		{
			this.rigidbody.velocity = Vector3.zero;			
		}
		
		// Zero out the x velocity so the impluse doesn't cancel itself out
		this.rigidbody.velocity = new Vector3(0f, this.rigidbody.velocity.y, 0f);
				
		Vector3 force = Vector3.up * MAX_IMPLUSE;
		
		// Heading Right, want to blast left
		if( headingRight )
		{
			force.x = UnityEngine.Random.Range(-10,-6);			
			
			// Kill rotation
			this.rigidbody.angularVelocity = Vector3.zero;
			
			// When heading left scale should be positive
			this.sprite.scale = new Vector2(Mathf.Abs(this.sprite.scale.x), this.sprite.scale.y);
		}
		
		// Heading Left, want to blast right
		else
		{
			force.x = UnityEngine.Random.Range(6,10);			
			
			// Kill rotation
			this.rigidbody.angularVelocity = Vector3.zero;
						
			// When heading right scale should be negative
			this.sprite.scale = new Vector2(-Mathf.Abs(this.sprite.scale.x), this.sprite.scale.y);
		}
				
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
		// Check the height to determine the difficulty of the next word
		float height = this.distanceTraveled;
		WordManager.WordDifficulty difficulty = WordManager.WordDifficulty.Easy;
		
		if( height > 800f )
			difficulty = WordManager.WordDifficulty.SuperHard;
		else if( height > 500f )
			difficulty = WordManager.WordDifficulty.VeryHard;
		else if( height > 200f )
			difficulty = WordManager.WordDifficulty.Hard;
		else if( height > 100f )
			difficulty = WordManager.WordDifficulty.Medium;
		
		return difficulty;		
	}
}
