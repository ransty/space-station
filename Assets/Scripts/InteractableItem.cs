using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InteractableItem : MonoBehaviour {

    public Rigidbody rigidbody;

    private bool currentlyInteracting;
    private PlayerVR attachedWand;
    private Transform interactionPoint;

    private float velocityFactor = 20000f;
    private float rotationFactor = 400f;

    private Vector3 posDelta;
    private Quaternion rotationDelta;
    private float angle;
    private Vector3 axis;


	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        interactionPoint = new GameObject().transform;
        velocityFactor /= rigidbody.mass;
        rotationFactor /= rigidbody.mass;
	}
	
	// Update is called once per frame
	void Update () {
		if (attachedWand && currentlyInteracting)
        {
            posDelta = attachedWand.transform.position - interactionPoint.position;
            this.rigidbody.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;

            rotationDelta = attachedWand.transform.rotation * Quaternion.Inverse(interactionPoint.rotation);
            rotationDelta.ToAngleAxis(out angle, out axis);

            if (angle > 180)
            {
                angle -= 360;
            }

            this.rigidbody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor * Math.Abs(angle) / 180;
        }
	}

    public void BeginInteraction(PlayerVR wand)
    {
        attachedWand = wand;
        interactionPoint.position = wand.transform.position;
        interactionPoint.rotation = wand.transform.rotation;
        interactionPoint.SetParent(transform, true);

        currentlyInteracting = true;
    }

    public void EndInteraction(PlayerVR wand)
    {
        if (wand = attachedWand)
        {
            attachedWand = null;
            currentlyInteracting = false;
        }
    }

    public bool isInteracting()
    {
        return currentlyInteracting;
    }
}
