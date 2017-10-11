using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravity : MonoBehaviour {

    float gravity = -1f;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().AddForce(new Vector3(-gravity, 0, 0));
    }

}
