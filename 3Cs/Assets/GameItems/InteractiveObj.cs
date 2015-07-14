using UnityEngine;
using System.Collections;

/***
 * Interactive Object
 * 
 * Enables simple animations and permits player interations
 */
public class InteractiveObj : MonoBehaviour {

	public Vector3 rotAxis;
	public float rotSpeed;

	public ObjectInteraction OnCloseEnough;

	private CustomGameObj gameObjectInfo;

	// Use this for initialization
	void Start () {
	
		gameObjectInfo = this.gameObject.GetComponent<CustomGameObj>();
		if (gameObjectInfo) {
			gameObjectInfo.validate();
		}
	}

	// Update is called once per frame
	void Update () {
	
		transform.Rotate(rotAxis, rotSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {

		if(other.gameObject.tag == "Player") {

			if (OnCloseEnough != null) {
				OnCloseEnough.handleInteraction();
			}
		}
	}
}
