using UnityEngine;
using System.Collections.Generic;
using System.IO;
using MiniJSON;

/// <summary>
/// Word manager keeps track of all the WordObjects in the world.
/// </summary>
public class WordManager 
{	
	public enum WordDifficulty
	{
		Easy = 0,
		Medium,
		Hard,		
	}
	
	/// <summary>
	/// List of all WordObjects that currently exist in the game
	/// </summary>
	private List<WordObject> wordObjects;
	
	// Database of all the words that may appear in the game categorized
	// by difficulty.	
	private Dictionary<WordDifficulty,List<string>> wordDatabase;

	// Use this for initialization
	public void Initialize() 
	{		
		this.wordObjects = new List<WordObject>();
		this.wordDatabase = new Dictionary<WordDifficulty, List<string>>();
		
		// Load the word database
		LoadWordDatabase();
	}
	
	private void LoadWordDatabase()
	{
		string fileContent = File.ReadAllText(GameUtils.GetStreamingAssetsPath()+"/WordDatabase.json");				
		Dictionary<string, object> wordDictionary = (Dictionary<string,object>)Json.Deserialize( fileContent );
			
		// Load word sets
		// TODO Support word sets - Make this a separate class altogether
		LoadSet( "default", wordDictionary );
	}
	
	private void LoadSet( string setName, Dictionary<string,object> wordDictionary )
	{
		Dictionary<string,object> wordSet = (Dictionary<string,object>) wordDictionary[setName];
		
		// Parse by difficulty			
		this.wordDatabase.Add( WordDifficulty.Easy, ParseWords( wordSet, "easy" ) );		
	}
	
	private List<string> ParseWords( Dictionary<string,object> dictionary, string name )
	{
		Dictionary<string,object> wordDict = (Dictionary<string,object>)dictionary[name];
		List<string> words = new List<string>();		
		foreach( string word in (List<object>)wordDict["words"] )
		{		
			words.Add(word.ToLower());
		}
		return words;
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
}
