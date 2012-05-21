using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Word manager keeps track of all the WordObjects in the world as well as
/// a list of all the words currently "in play".
/// </summary>
public class WordManager 
{
	/// <summary>
	/// List of all words that are visible on screen and can be matched.
	/// </summary>
	List<string> words;
	
	

	// Use this for initialization
	public void Initialize() 
	{
		words = new List<string>();
		
	}
	
	// Update is called once per frame
	public void Update() 
	{
	
	}
}
