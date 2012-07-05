using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BoundaryWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider other )
	{
		// If we've hit the boundary, zap us to the opposite side of the screen.
		if( other.gameObject.name == "Cat" )
		{
			Vector3 newPosition = other.transform.position;
			newPosition.x *= -1;
			if( newPosition.x < 0 )
				newPosition.x += 0.5f;
			else
				newPosition.x -= 0.5f;
			other.gameObject.transform.position = newPosition;
		}
	}
}
