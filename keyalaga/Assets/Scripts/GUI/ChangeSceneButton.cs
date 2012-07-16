using UnityEngine;
using System.Collections;

public class ChangeSceneButton : MonoBehaviour {
	
	public string scene = null;
	
	private void OnClick()
	{
		GameUtils.Assert(scene!=null, "ChangeSceneButton does have a scene to change to.");		
		Application.LoadLevel(scene);
	}
}
