using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NumpadLevel : MonoBehaviour
{

    public LoadNextLevel manager;

    public VRIO_Button[] Buttons = new VRIO_Button[10];

    int index = 0;

    bool holding = false;

    int[] password = { 6, 9, 4, 2, 0 };

    bool firstGrab = false;
    public AudioClip omg;
    public VRIO_Pickup recorder;

    private void Update()
    {
        if (!firstGrab && recorder.grabbed)
        {
            firstGrab = true;
            GetComponent<AudioSource>().clip = omg;
            GetComponent<AudioSource>().PlayDelayed(recorder.gameObject.GetComponent<AudioSource>().clip.length + 0.3f);
        }
    }

    public void Press(string name)
    {
        int number = int.Parse(name);

        if (password[index] == number)
        {
            index += 1;
            if (index == password.Length)
            {
                StopAllCoroutines();
                manager.NextLevel("Level3");
            }
        }
        else
        {
            index = 0;
        }
    }
}
