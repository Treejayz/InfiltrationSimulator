using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour {

	public VRIO_Button button;
	
	bool triggered = false;
	// Update is called once per frame
	void Update () {
		if (!triggered && button.pressed){
			triggered = true;
			StartCoroutine("LoadLevel");
		}
	}

	IEnumerator LoadLevel() {
		GetComponent<ParticleSystem>().Play();
		yield return new WaitForSeconds(6f);
		SteamVR_LoadLevel.Begin("Level2");
	}
}
