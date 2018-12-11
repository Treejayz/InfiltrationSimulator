using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneBar : MonoBehaviour {

    public XylophoneLevel manager;

    bool cooldown = false;

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "Drumstick" && !cooldown) {
            manager.HitNote(name);
			GetComponent<AudioSource>().Play();
            col.gameObject.SendMessage("SendPulse", (ushort)4000);
            cooldown = true;
            StartCoroutine("Wait");
		}
	}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        cooldown = false;
    }
}
