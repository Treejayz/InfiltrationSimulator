using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonLevel : MonoBehaviour
{

    public LoadNextLevel manager;

    public GameObject[] lights;

    public Material[] baseMats;
    public Material[] flashMats;

    bool holding = false;

    // Red, Green, Blue, Yellow = 0, 1, 2, 3
    int[] order = { 2, 1, 1, 0, 2, 3, 3, 3 };
    int[] realOrder = { 2, 1, 2, 0, 1, 2, 3, 3 };

    int index = 0;

    private void Start()
    {
        StartCoroutine("Flashing");
    }

    public void Press(string name)
    {
        int color = -1;
        if (name == "Red")
        {
            color = 0;
        } else if (name == "Green")
        {
            color = 1;
        } else if (name == "Blue")
        {
            color = 2;
        } else if (name == "Yellow")
        {
            color = 3;
        }
        if (realOrder[index] == color)
        {
            index += 1;
            if (index == realOrder.Length)
            {
                manager.NextLevel("Level5");
            }
        } else
        {
            index = 0;
        }
    }

    IEnumerator Flashing ()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < order.Length; i++)
            {
                lights[order[i]].GetComponent<Light>().enabled = true;
                lights[order[i]].GetComponent<MeshRenderer>().material = flashMats[order[i]];
                yield return new WaitForSeconds(0.3f);
                lights[order[i]].GetComponent<Light>().enabled = false;
                lights[order[i]].GetComponent<MeshRenderer>().material = baseMats[order[i]];
                yield return new WaitForSeconds(0.15f);

            }
        }
    }

}

