using UnityEngine;
using System.Collections;

public class SlowTimeButton : MonoBehaviour {
	
	private bool timeSlowed = false;
		
	private void OnClick()
	{
		if( !this.timeSlowed )
		{
			Game.instance.SlowTime( true );
		}
		else
		{
			Game.instance.SlowTime( false );	
		}
		
		this.timeSlowed = !this.timeSlowed;
	}
}
