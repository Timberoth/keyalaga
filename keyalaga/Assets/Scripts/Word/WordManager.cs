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
	
	/// <summary>
	/// List of all WordObjects that currently exist in the game
	/// </summary>
	List<WordObject> wordObjects;

	// Use this for initialization
	public void Initialize() 
	{
		this.words = new List<string>();
		this.wordObjects = new List<WordObject>();
	}
	
	public void AddWordObject( WordObject wordObject )
	{
		this.wordObjects.Add( wordObject );	
	}
	
	public void CheckForMatches( string inputBuffer ) 
	{		
		// TODO Optimize this
		// Go through entire wordObjects list and check for matches
		foreach( WordObject wordObject in wordObjects )
		{
			if( inputBuffer.Contains( wordObject.word ) )
			{
				wordObject.ReactToMatch();	
			}
		}
	}
	
	private List<string> ConvertArrayToList( string[] array )
	{
		List<string> list = new List<string>();
		for( int i = 0; i < array.Length; i++ )
		{
			list.Add( array[i] );	
		}
		return list;
	}
}
