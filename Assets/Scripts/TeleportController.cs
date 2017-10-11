using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour {

    Transform destination;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(4000, 1.990295f, -9.419082f);
        Camera.main.transform.position = new Vector3(4000, 1.990295f, -9.419082f);
    }
}
