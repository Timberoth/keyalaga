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
		VeryHard,
		SuperHard,
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
		this.wordDatabase.Add( WordDifficulty.Medium, ParseWords( wordSet, "medium" ) );		
		this.wordDatabase.Add( WordDifficulty.Hard, ParseWords( wordSet, "hard" ) );
		this.wordDatabase.Add( WordDifficulty.VeryHard, ParseWords( wordSet, "very_hard" ) );
		this.wordDatabase.Add( WordDifficulty.SuperHard, ParseWords( wordSet, "super_hard" ) );
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
		// Not factoring in case sensitivity for now
		inputBuffer = inputBuffer.ToLower();		
				
		// Go through entire wordObjects list and check for matches
		string[] words = inputBuffer.Split(' ');
		foreach( WordObject wordObject in wordObjects )
		{		
			// 1st pass check if the buffer contains the word or phrase
			if( inputBuffer.Contains( wordObject.word ) )
			{
				// 2nd pass check for exact match
				
				// We're looking for a single word match
				if( !wordObject.word.Contains(" ") )
				{
					// Double check that the word is an exact match				
					for( int i = 0; i < words.Length; i++ )
					{
						if(	wordObject.word == words[i] )		
						{
							wordObject.ReactToMatch(PickRandomWord(DetermineDifficulty( wordObject )));
							return;
						}
					}			
				}
				
				// We're looking for a phrase match
				else
				{					
					// Double check that the phrase is an exact match
					int startIndex = inputBuffer.IndexOf( wordObject.word );					
					int endIndex = startIndex + wordObject.word.Length - 1;					
					char charBeforePhrase = ' ';
					char charAfterPhrase = ' ';
					if( startIndex > 0 )
						charBeforePhrase = inputBuffer[startIndex-1];
					if( endIndex < inputBuffer.Length - 1 )
						charAfterPhrase = inputBuffer[endIndex+1];
				
					// There a bunch of cases to check for a match
					if( inputBuffer.Length == wordObject.word.Length )
					{
						wordObject.ReactToMatch(PickRandomWord(DetermineDifficulty( wordObject )));
						return;
					}
					
					else if( charBeforePhrase == ' ' && charAfterPhrase == ' ' )
					{
						wordObject.ReactToMatch(PickRandomWord(DetermineDifficulty( wordObject )));
						return;
					}
				}				
			}
		}
	}	
	
	private WordDifficulty DetermineDifficulty( WordObject wordObject )
	{
		// Check the height to determine the difficulty of the next word
		float height = wordObject.rigidbody.position.y;
		WordDifficulty difficulty = WordDifficulty.Easy;
		
		if( height > 800f )
			difficulty = WordDifficulty.SuperHard;
		else if( height > 500f )
			difficulty = WordDifficulty.VeryHard;
		else if( height > 200f )
			difficulty = WordDifficulty.Hard;
		else if( height > 100f )
			difficulty = WordDifficulty.Medium;
		
		return difficulty;		
	}
	
	public string PickRandomWord( WordDifficulty difficulty )
	{
		// TODO Keep track of which words are already used to prevent duplicates
		List<string> words = (List<string>)this.wordDatabase[difficulty];
		int random = UnityEngine.Random.Range(0, words.Count);
		return words[random];
	}
}