using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

	public Light dirLight;
	public Material lightOn;
	public Material lightOff;

	bool isLightOn = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		// light is on = light is off
		isLightOn = !isLightOn;
		// enabled = false
		dirLight.enabled = isLightOn;
		if (isLightOn) {
			this.GetComponent<Renderer> ().material = lightOn;
		} else {
			this.GetComponent<Renderer> ().material = lightOff;
		}
	}

}
