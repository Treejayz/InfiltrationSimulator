using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeLevel : MonoBehaviour
{

    public LoadNextLevel manager;

    public VRIO_Safe safe;

    public AudioClip loop;

    int[] order = { 8, 15, 10 };

    int index = 0;

    int prevValue = 0;

    AudioSource aud;

    // Use this for initialization
    void Start()
    {
        aud = GetComponents<AudioSource>()[1];
        StartCoroutine("StartLoop");
    }

    // Update is called once per frame
    void Update()
    {
        int value = safe.value;
        if (value != prevValue) {
            if (index == 0) {
                if (value == order[0] && prevValue ==  order[0] + 1) {
                    index += 1;
                    safe.Click();
                    aud.Play();
                }
            }
            else if (index == 1) {
                if (value < order[0]) {
                    index = 0;
                } 
                else if (value == order[1] && prevValue ==  order[1] - 1) {
                    index += 1;
                    safe.Click();
                    aud.Play();
                }
            } 
            else if (index == 2) {
                
                if (value > order[1]) {
                    index = 0;
                } 
                else if (value == order[2] && prevValue ==  order[2] + 1) {
                    safe.Click();
                    aud.Play();
                    manager.NextLevel("Level6");
                }
            }


            prevValue = value;
        }

    }

    IEnumerator StartLoop()
    {
        AudioSource voiceAud = GetComponent<AudioSource>();
        yield return new WaitForSeconds(voiceAud.clip.length);
        voiceAud.loop = true;
        voiceAud.clip = loop;
        voiceAud.Play();
    }


}

