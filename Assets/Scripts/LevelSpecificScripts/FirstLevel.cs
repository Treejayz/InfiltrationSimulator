using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevel : MonoBehaviour {

    public LoadNextLevel manager;

    public VRIO_Button Button;

    int index = 0;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Button.pressed)
        {
            manager.NextLevel("Level2");
        }
	}
}
