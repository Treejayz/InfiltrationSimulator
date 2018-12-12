using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public VRControllerInput right, left;

    float holdtime = 0f;

    bool loading = false;

	// Update is called once per frame
	void Update () {
		
        if (right.device.GetHairTrigger() && left.device.GetHairTrigger())
        {
            holdtime += Time.deltaTime;
            if (holdtime >= 1f && !loading)
            {
                loading = true;
                SteamVR_LoadLevel.Begin("Level1");
            }
        }
        else if (holdtime > 0)
        {
            holdtime = 0;
        }

	}



}
