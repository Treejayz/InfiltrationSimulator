using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneLevel : MonoBehaviour
{

    public LoadNextLevel manager;

    public AudioClip thatSoundNice;

    string[] order = { "F", "D", "A", "C7", "Done" };

    int index = 0;

    bool unhit = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitNote(string note)
    {
        if (unhit)
        {
            unhit = false;
            GetComponent<AudioSource>().clip = thatSoundNice;
            GetComponent<AudioSource>().PlayDelayed(1.5f);
        }

        if (note == order[index])
        {
            index += 1;
            if (index == 4)
            {
                manager.NextLevel("Level5");
            }
        } else
        {
            index = 0;
        }
    }

}

