using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool correctDoor = false;

    public LoadNextLevel manager;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Key(Clone)" && col.gameObject.GetComponent<VRIO_Pickup>().held)
        {
            if (correctDoor) {
                manager.NextLevel("Level8");
                Destroy(col.gameObject);
            }
            else
            {

            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
