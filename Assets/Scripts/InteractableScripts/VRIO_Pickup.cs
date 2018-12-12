using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRIO_Pickup : VRInteractableObject
{

    public bool isParented = false;

    Vector3 prevPos = new Vector3(0, 0, 0);
    Vector3 velocity;

    [HideInInspector]
    public bool held = false;

    public override void Grab(GameObject controller)
    {
        base.Grab(controller);
        if (GetComponent<Return>() != null)
        {
            GetComponent<Return>().beingHeld = true;
        }

        prevPos = controller.transform.position;
        if (isParented)
        {
            transform.parent = controller.transform;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            FixedJoint fx = controller.AddComponent<FixedJoint>();
            fx.breakForce = 2000;
            fx.breakTorque = 2000;
            fx.connectedBody = GetComponent<Rigidbody>();
        }

        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }
        held = true;
    }

    public override void Release(GameObject controller)
    {
        if (GetComponent<Return>() != null)
        {
            GetComponent<Return>().beingHeld = false;
        }
        if (controller == currentController)
        {
            if (isParented)
            {
                transform.parent = null;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().velocity = velocity;
                GetComponent<Rigidbody>().angularVelocity = SteamVR_Controller.Input((int)controller.GetComponent<SteamVR_TrackedObject>().index).angularVelocity;
            }
            else if (controller.GetComponent<FixedJoint>())
            {
                controller.GetComponent<FixedJoint>().connectedBody = null;
                Destroy(controller.GetComponent<FixedJoint>());
                GetComponent<Rigidbody>().velocity = velocity;
                GetComponent<Rigidbody>().angularVelocity = SteamVR_Controller.Input((int)controller.GetComponent<SteamVR_TrackedObject>().index).angularVelocity;
            }
        }
        held = false;
        base.Release(controller);
    }

    private void FixedUpdate()
    {
        if (grabbed)
        {
            velocity = (currentController.transform.position - prevPos) * (1f/Time.fixedDeltaTime);
            prevPos = currentController.transform.position;
        }
    }
}
