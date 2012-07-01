
#define DEBUG

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

public class GameUtils 
{	
    [Conditional("DEBUG")]
    public static void Assert(bool condition, string message)
    {
        if (!condition)
		{
			UnityEngine.Debug.LogError(message);
			throw new Exception();
		}
    }	
	
	public static string GetStreamingAssetsPath()
	{
#if UNITY_EDITOR
		return Application.dataPath + "/StreamingAssets";
#elif UNITY_IPHONE
		return Application.dataPath + "/Raw";
#elif UNITY_ANDROID
		return "jar:file://" + Application.dataPath + "!/assets/";
#else
		return Application.dataPath + "/StreamingAssets";
#endif
	}	
}
