using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DoorController : VRTK_InteractableObject {

    public bool flipped = false;
    public bool rotated = false;

    private float sideFlip = -1;
    private float side = -1;
    private float smooth = 270.0f;
    private float doorOpenAngle = 0f;
    private bool open = false;

    private Vector3 defaultRotation;
    private Vector3 openRotation;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        SetDoorRotation(usingObject.transform.position);
        SetRotation();
        open = !open;
    }

    protected void Start()
    {
        defaultRotation = transform.eulerAngles;
        SetRotation();
        sideFlip = (flipped ? 1 : -1);
    }

    protected override void Update()
    {
        base.Update();
        if (open)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 5);
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * 5);
        }
    }

    private void SetRotation()
    {
        openRotation = Vector3.up;
    }

    private void SetDoorRotation(Vector3 interacterPosition)
    {
        side = ((rotated == false && interacterPosition.z > transform.position.z) || (rotated == true && interacterPosition.x > transform.position.x) ? -1 : 1);
    }
}
