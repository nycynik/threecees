using UnityEngine;
using System.Collections;

/***
 * GameCam Class
 * 
 * 3rd person canera class.
 * Follows the player, keeping player in view.
 * Uses linear interpolation and dampening to keep the camera facing the right
 * way and not joslign around.
 * 
 */
public class GameCam : MonoBehaviour {
	
	public GameObject trackObj;  // What to track, the player mostly.
	public float height;
	public float desiredDistance; // how far behind.
	public float heightDamp;
	public float rotDamp;

	// Use this for initialization
	void Start () {
	
	}

	void UpdateRotAndTrans ()
	{
		
		float DesiredRotationalAngle;
		float DesiredHeight;


		if (trackObj) {
			// tracked object
			// y is horizontal angle of the object
			DesiredRotationalAngle = trackObj.transform.eulerAngles.y;
			DesiredHeight = trackObj.transform.position.y + height;

			// camera
			float RotAngle = transform.eulerAngles.y;
			float Height = transform.position.y;

			RotAngle = Mathf.LerpAngle (RotAngle, DesiredRotationalAngle, rotDamp);
			Height = Mathf.LerpAngle (Height, DesiredHeight, heightDamp * Time.deltaTime); // depends on real time, not frame rate

			// move camera in the opposite direction to where player is facing (so we can put the cam behind the player)
			Quaternion CurrentRotation = Quaternion.Euler(0.0f, RotAngle, 0.0f);
			Vector3 pos = trackObj.transform.position;
			pos -= CurrentRotation * Vector3.forward * desiredDistance;
			pos.y = Height;

			// set the whole thing all at once, only way.
			transform.position = pos;

			transform.LookAt(trackObj.transform.position);

		} else {
			Debug.Log("[900] Game camera Error: nothing to track.");
		}
	}
	
	// Update is called once per frame
	void Update () {

		UpdateRotAndTrans();
	}
}
