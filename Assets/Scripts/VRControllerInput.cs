using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VRControllerInput : MonoBehaviour {

    [HideInInspector]
    public GameObject currentHeld;

    Vector3 regularTriggerSize;
    Vector3 holdingTriggerSize;

    float holdTime = 0f;
    bool loading = false;

    protected SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    void Awake()
    {
        //Instantiate lists
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        regularTriggerSize = GetComponent<BoxCollider>().size;
        holdingTriggerSize = (GetComponent<BoxCollider>().size) * 2.5f;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Interactable") {
            device.TriggerHapticPulse(3999);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        //If object is an interactable item
        VRInteractableObject interactable = collider.GetComponent<VRInteractableObject>();
        if (interactable != null)
        {
            if (currentHeld == null) {
                device.TriggerHapticPulse(200);
            } else {
                device.TriggerHapticPulse(50);
            }
            //If trigger button is down
            if (device.GetHairTriggerDown() && currentHeld == null)
            {
                //Pick up object
                device.TriggerHapticPulse(2000);
                interactable.Grab(this.gameObject);
                currentHeld = collider.gameObject;
                GetComponent<BoxCollider>().size = holdingTriggerSize;
            }
            if (device.GetHairTriggerUp())
            {
                //Pick up object
                
                interactable.Release(this.gameObject);
                if (currentHeld != interactable && currentHeld != null)
                {
                    GetComponent<BoxCollider>().size = regularTriggerSize;
                    currentHeld.GetComponent<VRInteractableObject>().Release(this.gameObject);
                    currentHeld = null;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentHeld == other.gameObject)
        {
            currentHeld.GetComponent<VRInteractableObject>().Release(this.gameObject);
            GetComponent<BoxCollider>().size = regularTriggerSize;
        }
    }

    private void Update()
    {
        if (device.GetHairTriggerUp() && currentHeld != null)
        {
            currentHeld.GetComponent<VRInteractableObject>().Release(this.gameObject);
            GetComponent<BoxCollider>().size = regularTriggerSize;
            currentHeld = null;
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            holdTime += Time.deltaTime;
            if (holdTime > 5f && !loading)
            {
                loading = true;
                int index = SceneManager.GetActiveScene().buildIndex + 1;
                if (index >= SceneManager.sceneCountInBuildSettings)
                {
                    index = 0;
                }
                string path = SceneUtility.GetScenePathByBuildIndex(index);
                int slash = path.LastIndexOf('/');
                string name = path.Substring(slash + 1);
                int dot = name.LastIndexOf('.');
                SteamVR_LoadLevel.Begin(name.Substring(0, dot));
            }
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            holdTime = 0f;
        }
    }
}
