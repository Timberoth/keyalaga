using UnityEngine;
using System.Collections;
using System;

public class CameraManager 
{
	// Ref to camera in scene
	public GameObject camera;
	
	// Ref to game object being tracked
	// TODO Extend this to track multiple items
	private GameObject trackingObject;
	private Rigidbody trackingObjectRigidBody;
		
	private Vector3 destinationCameraPosition;
		
	private float cameraSpeed = 10.0f;
	
	// Adapted from Aubrey Hesselgren's function at 
	// http://answers.unity3d.com/questions/17076/how-to-lerp-between-to-vector3-positions-c.html
	public static Vector3 InterpolateStepOverTime(Vector3 Current, Vector3 Target, float DeltaTime, float InterpSpeed)
	{
	    Vector3 delta = Target - Current;		
		
		//distance to our target is zero. Already at our target.
	    if ( delta.sqrMagnitude < Mathf.Epsilon)
	    {
	        return Target;
	    }	    
	
	    Vector3 vectorDir = Vector3.Normalize(delta);
	    Vector3 stepSize = DeltaTime * InterpSpeed * vectorDir;//How far to travel this frame.
	    Vector3 finalPos = Current + stepSize;

		// Check if we're close enough to the target to snap to it.
		if( (finalPos - Target).magnitude <= 0.25 )
			finalPos = Target;
		
	    return finalPos;
	}
	
	// Use this for initialization
	public void Initialize() 
	{
		// Find the game's camera object
		this.camera = GameObject.Find("Main Camera");		
		this.destinationCameraPosition = this.camera.transform.position;
	}
	
	public void Update() 
	{										
		Vector3 ballOffset = -Vector3.Normalize(this.trackingObjectRigidBody.velocity) * 1.75f;
		this.destinationCameraPosition = this.trackingObjectRigidBody.position + ballOffset;						
				
		this.camera.transform.position = InterpolateStepOverTime( 
			this.camera.transform.position, 
			this.destinationCameraPosition, 
			Time.deltaTime, 
			this.cameraSpeed );		
	}
	
	public void AddTrackingObject( GameObject newObject )
	{			
		DebugUtils.Assert( newObject != null );
		this.trackingObject = newObject;
		
		this.trackingObjectRigidBody = this.trackingObject.GetComponent<Rigidbody>();
	}
}