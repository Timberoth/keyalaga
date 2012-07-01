using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager 
{	
	private GameObject soundEffectsPlayer;
	private AudioSource soundEffectsSource;
	
	private GameObject musicTrackPlayer;
	private AudioSource musicTrackSource;
	
	private Dictionary<string, AudioClip> soundEffects;
	private Dictionary<string, AudioClip> musicTracks;
	
	public void Initialize() 
	{
		this.soundEffectsPlayer = new GameObject();
		this.soundEffectsPlayer.name = "Sound Effects Player";
		this.soundEffectsPlayer.AddComponent<AudioSource>();
		this.soundEffectsSource = (AudioSource)this.soundEffectsPlayer.GetComponent<AudioSource>();
		
		this.musicTrackPlayer = new GameObject();
		this.musicTrackPlayer.name = "Music Track Player";
		this.musicTrackPlayer.AddComponent<AudioSource>();
		this.musicTrackSource = (AudioSource)this.musicTrackPlayer.GetComponent<AudioSource>();
		
		this.soundEffects = new Dictionary<string, AudioClip>();
		this.musicTracks = new Dictionary<string, AudioClip>();
		
		// Register all the tracks
		RegisterSoundEffect("jump", "Audio/SoundEffects/normal_jump");
		RegisterSoundEffect("super_jump", "Audio/SoundEffects/super_jump");
		
		RegisterMusicTrack("ChronoTrigger", "Audio/Music/ChronoTrigger");
		
		PlayMusicTrack("ChronoTrigger");
	}
		
	public void Update () 
	{
	
	}
	
	private void RegisterSoundEffect( string name, string file )
	{
		GameUtils.Assert(!this.soundEffects.ContainsKey(name), 
				"Trying to re-register a sound effect: "+name+" - "+file);			
				
		AudioClip clip = (AudioClip)Resources.Load(file);
		GameUtils.Assert(clip!=null, "Unable to load resource: "+file);
		this.soundEffects.Add( name, clip );
	}
	
	private void RegisterMusicTrack( string name, string file )
	{
		GameUtils.Assert(!this.musicTracks.ContainsKey(name),
			"Trying to re-register a music track: "+name+" - "+file);
				
		AudioClip clip = (AudioClip)Resources.Load(file);
		GameUtils.Assert(clip!=null, "Unable to load resource: "+file);
		this.musicTracks.Add( name, clip );		
	}
	
	public void PlaySoundEffect( string name )
	{
		GameUtils.Assert(this.soundEffects.ContainsKey(name),
			"Trying to play a sound effect that hasn't been registered: "+name);
		
		this.soundEffectsSource.PlayOneShot(this.soundEffects[name]);
	}
	
	public void PlayMusicTrack( string name, bool loop=false )
	{
		GameUtils.Assert(this.musicTracks.ContainsKey(name),
			"Trying to play a music track that hasn't been registered: "+name);
		
		if( loop )
			this.musicTrackSource.loop = true;
		
		this.musicTrackSource.clip = this.musicTracks[name];
		this.musicTrackSource.Play();
		
		Debug.Log("Playing Music");
	}
	
	public void StopMusicTrack()
	{
		this.musicTrackSource.Stop();
	}
}
