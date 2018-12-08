using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinata : MonoBehaviour {

    public GameObject key;

    public float maxDamage = 30f;

    float damage = 0f;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Bat")
        {
            print(col.impulse.magnitude);
            damage += col.impulse.magnitude;
            if (damage > maxDamage)
            {
                GameObject spawnedKey = Instantiate(key, transform.position, transform.rotation);
                spawnedKey.GetComponent<Rigidbody>().velocity = col.impulse * -0.3f;
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
