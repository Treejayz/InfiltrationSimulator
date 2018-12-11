using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRIO_Hold : VRInteractableObject
{

    public bool isParented = false;

    public Vector3 offset;
    public Vector3 angle;

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
        transform.position = controller.transform.position + controller.transform.rotation * offset;
        transform.rotation = controller.transform.rotation * Quaternion.Euler(angle);
        if (isParented)
        {
            transform.parent = controller.transform;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            FixedJoint fx = controller.AddComponent<FixedJoint>();
            fx.breakForce = 20000;
            fx.breakTorque = 20000;
            fx.connectedBody = GetComponent<Rigidbody>();
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
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
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

    public void SendPulse(ushort pulse)
    {
        if (grabbed)
        {
            SteamVR_Controller.Input((int)currentController.GetComponent<SteamVR_TrackedObject>().index).TriggerHapticPulse(pulse);
        }
    }
}
