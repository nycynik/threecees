using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public Vector3 moveDirection = Vector3.zero;

	public float rotateSpeed;
	public float moveSpeed = 0.0f;
	private float speedSmoothing = 10.0f;


	// Use this for initialization
	void Start () {
	
	}

	void UpdateMovement ()
	{
		Vector3 cameraForward = Camera.main.transform.TransformDirection(Vector3.forward);

		cameraForward.y = 0.0f;
		cameraForward.Normalize();

		// TIP: we find the right vector by flipping the x and z components and negating the last component
		// faster than extracting and transforming the right vector. (normally x,y,z,so flipped is z 0 x)
		Vector3 cameraRight = new Vector3(cameraForward.z, 0.0f, -cameraForward.x);

		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");

		// target direction
		Vector3 targetDirection = ((h * cameraRight) + (v * cameraForward));

		moveDirection = Vector3.RotateTowards (moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
		moveDirection = moveDirection.normalized;

		// smoth the speed
		float curSmooth = speedSmoothing * Time.deltaTime;
		float targetSpeed = Mathf.Min (targetDirection.magnitude, 1.0f);
		moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);

		// displacement vector
		Vector3 displacement = moveDirection * moveSpeed * Time.deltaTime;

		// move character.
		this.GetComponent<CharacterController>().Move(displacement);
		transform.rotation = Quaternion.LookRotation (moveDirection);
	

	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement();
	}
}
