using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool correctDoor = false;

    public LoadNextLevel manager;

    public GameObject pinata;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Key(Clone)" && col.gameObject.GetComponent<VRIO_Pickup>().held)
        {
            Destroy(col.gameObject);
            if (correctDoor) {
                manager.NextLevel("Level4");
            }
            else
            {
                pinata.GetComponent<Pinata>().Respawn();
            }
        }
    }
}
