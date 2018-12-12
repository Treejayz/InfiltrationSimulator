using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinata : MonoBehaviour {

    public GameObject key;

    public float maxDamage = 3f;

    float damage = 0f;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Bat")
        {
            damage += 1f;
            col.gameObject.SendMessage("SendPulse", (ushort)4000);
            if (damage > maxDamage)
            {
                GameObject spawnedKey = Instantiate(key, transform.position, transform.rotation);
                spawnedKey.GetComponent<Rigidbody>().velocity = col.impulse * -0.3f;
                transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<Collider>().isTrigger = true;
            }
            GetComponent<AudioSource>().Play();
        }
    }

    public void Respawn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<Collider>().isTrigger = false;
    }
}
