namespace VRTK.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ModelVillage_TeleportLocation : VRTK_DestinationMarker
    {
        public Transform firstDestination;
        
        private bool lastUsePressedState = false;

        private void OnTriggerStay(Collider collider)
        {
            VRTK_ControllerEvents controller;
            controller = (collider.GetComponent<VRTK_ControllerEvents>() ? collider.GetComponent<VRTK_ControllerEvents>() : collider.GetComponentInParent<VRTK_ControllerEvents>());
            if (controller != null)
            {
                if (lastUsePressedState == true && !controller.triggerPressed)
                {
                    float distance = Vector3.Distance(transform.position, firstDestination.position);
                    VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(controller.gameObject);
                    OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, firstDestination, new RaycastHit(), firstDestination.position, controllerReference));
                }
                lastUsePressedState = controller.triggerPressed;
            }
        }
    }
}