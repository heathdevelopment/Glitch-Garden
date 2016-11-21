using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

	public AudioClip[] levelMusicChangeArray;

	private AudioSource audiosource;


	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		Debug.Log ("Don't Destroy on load: " + name);
	}

	void Start ()
	{
		audiosource = GetComponent<AudioSource> ();
		audiosource.volume = PlayerPrefsManager.GetMasterVolume ();
	}

	void OnLevelWasLoaded (int level)
	{
		AudioClip thisLevelMusic = levelMusicChangeArray [level];
		Debug.Log ("Playing clip: " + thisLevelMusic);

		if (thisLevelMusic) {
			audiosource.clip = thisLevelMusic;
			audiosource.loop = true;
			audiosource.Play ();
		}

	}

	public void SetVolume (float volume)
	{
		audiosource.volume = volume;
		
	}
}