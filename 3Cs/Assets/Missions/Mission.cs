using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Mission {

	public enum MissionStatus
	{
		MS_Invalid = -1,
		MS_Acquired = 0,
		MS_InProgress = 1,
		MS_Completed = 2,
		MS_ForceComplete = 3
	};

	public bool activated;
	public bool visible;
	public MissionStatus status;
	public string displayName;
	public string description;
	public List<MissionToken> tokens;	// the set of tokens (ids) which define the requirements for completing this mission
	public int points;
	public GameObject reward;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InvokeReward() {
		// if the mission is finished, instatiate the reward callback
		if (reward != null) {
			GameObject.Instantiate(reward);
			this.activated = false;
			this.visible = false;
			this.status = MissionStatus.MS_Completed;
		}
	}
}
