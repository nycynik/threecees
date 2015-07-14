using UnityEngine;
using System.Collections;

public class CustomGameObj : MonoBehaviour {

	public CustomGameObjectType objectType;
	public string displayName;

	public enum CustomGameObjectType
	{
		Invalid = -1,
		Unique = 0,
		Coin,
		Ruby,
		Emerald,
		Diamond
	}
	
	// Use this for initialization
	void Start () {
	
	}

	// validate
	// ensures displayname is not empty
	public void validate() {
		if (displayName == "") {
			displayName = "unnamed_object";
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
