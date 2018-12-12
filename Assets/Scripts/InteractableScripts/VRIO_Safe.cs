using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRIO_Safe : VRInteractableObject
{

    public float numTurns;
    public GameObject origin;
    public GameObject model;

    [HideInInspector]
    public int value = 0;
    float angularVelocity;

    Vector3 prevHandPosition;

    float angle = 10f;


    public override void Grab(GameObject controller)
    {
        base.Grab(controller);
        prevHandPosition = origin.transform.InverseTransformPoint(currentController.transform.position);
        prevHandPosition.x = 0;
    }

    public override void Release(GameObject controller)
    {
        base.Release(controller);
    }

    private void Update()
    {
        // If we are grabbing something, update the angle
        if (grabbed)
        {
            Vector3 currentHandPosition = origin.transform.InverseTransformPoint(currentController.transform.position);
            currentHandPosition.x = 0;
            float angularDelta = (Mathf.Atan2(currentHandPosition.y, currentHandPosition.z) - Mathf.Atan2(prevHandPosition.y, prevHandPosition.z)) * Mathf.Rad2Deg;
            prevHandPosition = currentHandPosition;
            model.transform.Rotate(0f, angularDelta, 0f, Space.Self);
            angularVelocity = angularDelta * (1f / Time.deltaTime);
            angle += angularDelta;

            if (angle <= 0f) { angle += 360f; }
            if (angle > 360f) { angle -= 360f; }

            int temp = (int)(angle / 20f);
            if (temp != value) {
                value = temp;
                SteamVR_Controller.Input((int)currentController.GetComponent<SteamVR_TrackedObject>().index).TriggerHapticPulse(2000);
                print(value + ", " + angle);
                if (GetComponent<AudioSource>() != null)
                {
                    GetComponent<AudioSource>().Play();
                }
            }
        }

    }

    public void Click() {
        SteamVR_Controller.Input((int)currentController.GetComponent<SteamVR_TrackedObject>().index).TriggerHapticPulse(4000);
    }

}
