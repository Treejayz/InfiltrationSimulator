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


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
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
                    manager.NextLevel("Level7");
                }
            } else {
                index = 0;
            }
        }
        
    }
}
