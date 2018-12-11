using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightSwitchLevel : MonoBehaviour
{

    public LoadNextLevel manager;

    int[] password = { 8, 8, 8, 8, 8, 8, 8, 8 };

    int index = 0;

    bool power = false;
    public GameObject lights;

    public List<AudioClip> waitingClips;
    AudioSource aud;

    private void Start()
    {
        StartCoroutine("Waiting");
    }

    public void Press(string name)
    {
        if (name == "LightSwitch")
        {
            power = !power;
            lights.SetActive(!power);
        } 
        if (power) {
            if (name == "8") {
                index += 1;
                if (index == 7) {
                    StopAllCoroutines();
                    manager.NextLevel("Level7");
                }
            } else {
                index = 0;
            }
        }
    }

    IEnumerator Waiting()
    {
        aud = GetComponent<AudioSource>();
        List<AudioClip> usedClips = new List<AudioClip>();
        yield return new WaitForSeconds(aud.clip.length);

        int index;

        while (true)
        {
            yield return new WaitForSeconds(10f);
            index = Random.Range(0, waitingClips.Count);
            aud.clip = waitingClips[index];
            aud.Play();
            usedClips.Add(waitingClips[index]);
            waitingClips.RemoveAt(index);
            if (waitingClips.Count == 0)
            {
                foreach(AudioClip clip in usedClips)
                {
                    waitingClips.Add(clip);
                }
                usedClips.Clear();
            }
            yield return new WaitForSeconds(aud.clip.length);
        }
    }

}
