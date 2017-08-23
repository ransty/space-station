using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float playerSpeed = 5.0f, sensitivity = 2f, jumpForce = 10f, zTeleport = 3f;
	public bool invertedAxis = false;

	CharacterController player;

	private bool hasJumped;

	public GameObject eyes;

	float moveFB, moveLR, verticalVelocity, rotX, rotY;



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
	}

	void Movement() {
		moveFB = Input.GetAxis("Vertical") * playerSpeed;
		moveLR = Input.GetAxis("Horizontal") * playerSpeed;

		rotX = Input.GetAxis ("Mouse X") * sensitivity;
		rotY = Input.GetAxis ("Mouse Y") * sensitivity;

		Vector3 movement = new Vector3 (moveLR, verticalVelocity, moveFB);
		transform.Rotate (0, rotX, 0);

		if (!invertedAxis) {
			eyes.transform.Rotate (-rotY, 0, 0);
		} else {
			eyes.transform.Rotate (rotY, 0, 0);
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
