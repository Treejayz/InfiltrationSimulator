using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour {
	
	bool triggered = false;
    public AudioClip endClip;

    public void NextLevel(string level)
    {
        if (!triggered)
        {
            triggered = true;
            StartCoroutine("LoadLevel", level);
        }
    }

	IEnumerator LoadLevel(string level) {
		GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().clip = endClip;
        GetComponent<AudioSource>().PlayDelayed(0.5f);
        yield return new WaitForSeconds(6f);
		SteamVR_LoadLevel.Begin(level);
	}
}
