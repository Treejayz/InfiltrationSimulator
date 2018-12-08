using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneLevel : MonoBehaviour
{

    public LoadNextLevel manager;

    string[] order = { "F", "D", "A", "C7" };

    int index = 0;

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
        if (note == order[index])
        {
            index += 1;
            if (index == 4)
            {
                manager.NextLevel("Level4");
            }
        } else
        {
            index = 0;
        }
    }

}

