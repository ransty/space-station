using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityZ : MonoBehaviour {

    float gravity = -9.8f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().AddForce(new Vector3(-gravity, 0, 0));
	}
}
