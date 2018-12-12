using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour {

    Vector3 startPos;
    Quaternion startRot;

    public bool beingHeld = false;

    public GameObject poof;

    float time = 0f;
    Rigidbody rb;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        startRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		if (!beingHeld && Vector3.Distance(transform.position, startPos) > 0.5f)
        {
            time += Time.deltaTime;
            if (time > 5f)
            {
                if (poof != null)
                {
                    Instantiate(poof, transform.position, Quaternion.identity);
                    Instantiate(poof, startPos, Quaternion.identity);
                }

                transform.position = startPos;
                transform.rotation = startRot;
                
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        } else
        {
            time = 0f;
        }
	}
}
