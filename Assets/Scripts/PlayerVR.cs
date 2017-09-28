using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVR : MonoBehaviour {

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_TrackedController _controller;
    public CharacterController player;

    private SteamVR_TrackedObject trackedObj;


    HashSet<InteractableItem> objectHovingOver = new HashSet<InteractableItem>();

    private InteractableItem closestItem;
    private InteractableItem interactingItem;


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start () {
         _controller = GetComponent<SteamVR_TrackedController>();
	}

    // Update is called once per frame
    void Update () {
        if (Controller.GetAxis().y > 0.1 && Controller.GetAxis().y < 0.5)
        {
            player.transform.position = player.transform.position  + Camera.main.transform.forward * 0.01f;
        }
        else if (Controller.GetAxis().y > -0.5 && Controller.GetAxis().y < -0.1)
        {
            player.transform.position = player.transform.position  - Camera.main.transform.forward * 0.01f;
        }
        else if (Controller.GetAxis().y >= 0.5)
        {
            player.transform.position = player.transform.position + Camera.main.transform.forward * 0.04f;
        }
        else if (Controller.GetAxis().y <= -0.5)
        {
            player.transform.position = player.transform.position - Camera.main.transform.forward * 0.04f;
        }

        
        if (Controller.GetAxis().x > 0.1 && Controller.GetAxis().x < 0.5)
        {
            player.transform.position = player.transform.position - Vector3.Cross(Camera.main.transform.forward, Camera.main.transform.up) * 0.01f;
        }
        else if (Controller.GetAxis().x > -0.5 && Controller.GetAxis().x < -0.1)
        {
            player.transform.position = player.transform.position + Vector3.Cross(Camera.main.transform.forward, Camera.main.transform.up) * 0.01f;
        }
        else if (Controller.GetAxis().x >= 0.5)
        {
            player.transform.position = player.transform.position - Vector3.Cross(Camera.main.transform.forward, Camera.main.transform.up) * 0.04f;
        }
        else if (Controller.GetAxis().x <= -0.5)
        {
            player.transform.position = player.transform.position + Vector3.Cross(Camera.main.transform.forward, Camera.main.transform.up) * 0.04f;
        }

        // grip
        if (Controller.GetPressDown(triggerButton) || Controller.GetPressDown(gripButton))
        {
            float minDistance = float.MaxValue;

            float distance;

            foreach (InteractableItem item in objectHovingOver)
            {
                distance = (item.transform.position - transform.position).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestItem = item;
                }
            }

            interactingItem = closestItem;
            closestItem = null;

            if (interactingItem)
            {
                if (interactingItem.isInteracting())
                {
                    interactingItem.EndInteraction(this);
                }

                interactingItem.BeginInteraction(this);
            }
        }

        if (Controller.GetPressUp(triggerButton) || Controller.GetPressUp(gripButton) && interactingItem != null)
        {
            interactingItem.EndInteraction(this);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractableItem collidedItem = other.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            objectHovingOver.Add(collidedItem);
        }

        Debug.Log("Trigger entered");
    }

    private void OnTriggerExit(Collider other)
    {
        InteractableItem collidedItem = other.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            objectHovingOver.Remove(collidedItem);
        }

        Debug.Log("Trigger entered");
    }
}
