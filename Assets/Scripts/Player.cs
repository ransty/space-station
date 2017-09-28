using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public float playerSpeed = 5.0f, sensitivity = 2f, jumpForce = 10f, zTeleport = 3f;
	public bool invertedAxis = false;

	CharacterController player;

	private bool hasJumped;

	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	//public GameObject eyes;

	float moveFB, moveLR, verticalVelocity, rotX, rotY;


	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	// Use this for initialization
	void Start () {

		player = GetComponent<CharacterController> ();

		//Player Spawn Point

		//This is where our player will start when the game is played.

		//player = gameObject, gameObject = transform
		//Vector3 = position
		transform.position = new Vector3 (5f, 0.5f, 0); //player spawn

	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		ApplyGravity ();

		if (Input.GetButtonDown ("Jump")) {
			hasJumped = true;
		}

		//Debug.Log(Controller.GetAxis());
		if (Controller.GetAxis().y > 0.1 && Controller.GetAxis().y < 0.5)
		{
			player.transform.position = player.transform.position + Camera.main.transform.forward * 0.01f;            
		}
		else if(Controller.GetAxis().y > -0.5 && Controller.GetAxis().y < -0.1)
		{
			player.transform.position = player.transform.position - Camera.main.transform.forward * 0.01f;
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
	}

	void Movement() {
		// playerSpeed = controller pad direction
		// -1 and 1 LR, FB
		// if Axis2D.PrimaryThumbstick.ID == 1 // horizontal
		// if Axis2D.PrimaryThumbstick.ID == 2 // Vertical
		// 4 horizontal
		// 5 vertical
		moveFB = Input.GetAxis("Vertical") * playerSpeed;
		moveLR = Input.GetAxis("Horizontal") * playerSpeed;

		rotX = Input.GetAxis ("Mouse X") * sensitivity;
		rotY = Input.GetAxis ("Mouse Y") * sensitivity;

		Vector3 movement = new Vector3 (moveLR, verticalVelocity, moveFB);
		transform.Rotate (0, rotX, 0);

		if (!invertedAxis) {
			//eyes.transform.Rotate (-rotY, 0, 0);
		} else {
			//eyes.transform.Rotate (rotY, 0, 0);
		}

		movement = transform.rotation * movement;
		player.Move (movement * Time.deltaTime);
	}

	void ApplyGravity()
	{
		if (player.isGrounded == true) {
			if (!hasJumped) {
				verticalVelocity = Physics.gravity.y;
			} else {
				verticalVelocity = jumpForce;
			}

		} else {
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
			verticalVelocity = Mathf.Clamp (verticalVelocity, -50f, jumpForce);
			hasJumped = false;
		}
	}

}
