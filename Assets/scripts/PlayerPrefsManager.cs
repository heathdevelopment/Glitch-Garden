﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour
{

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";

	public static void SetMasterVolume (float volume)
	{
		if (volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError ("Master volume out of range");
		}
	}

	public static float GetMasterVolume ()
	{
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}

	public static void UnlockLevel (int level)
	{
		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			PlayerPrefs.SetInt (LEVEL_KEY + level.ToString (), 1);//use 1 for true
		} else {
			Debug.LogError ("Trying to unlock level not in build seetings");
		}
	}

	public static bool IsLevelUnlocked (int level)
	{
		int levelValue = PlayerPrefs.GetInt (LEVEL_KEY + level.ToString ());
		bool isLevelUnlocked = (levelValue == 1);

		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			return isLevelUnlocked;
		} else {
			Debug.LogError ("Trying to query level not in build seetings");
			return false;
		}
	}

	public static void SetDifficulty (float diff)
	{
		if (diff >= 1f && diff <= 3f) {
			PlayerPrefs.SetFloat (DIFFICULTY_KEY, diff);
		} else {
			Debug.LogError ("Setting difficulty to the wrong level");
		}

	}

	public static float GetDifficulty ()
	{
		return PlayerPrefs.GetFloat (DIFFICULTY_KEY);

	}
 
}
