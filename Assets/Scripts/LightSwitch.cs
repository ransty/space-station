using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{

    public Light dirLight;
    public Light secondLight;
    public Material lightOn;
    public Material lightOff;

    bool isColliding = false;
    public bool multipleLights = false;

    bool isLightOn = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject)
        {
            // light is on = light is off
            isLightOn = !isLightOn;
            // enabled = false
            dirLight.enabled = isLightOn;
            if (multipleLights)
            {
                secondLight.enabled = isLightOn;
            }
            if (isLightOn)
            {
                this.GetComponent<Renderer>().material = lightOn;
                if (multipleLights)
                {
                    this.GetComponent<Renderer>().material = lightOn;
                }

            }
            else
            {
                this.GetComponent<Renderer>().material = lightOff;

            }
        }
    }
}
