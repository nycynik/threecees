using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour {

	public List<Mission> missions;
	public List<MissionToken> missionTokens = new List<MissionToken>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// add a new mission to the list of missions.
	public void AddMission (MissionToken mt) {

		bool uniquetoken = true;
		if (missionTokens != null) {

			// check all the mission tokens, and make sure this one is not there.
			// TODO: what is there no find in list?
			// if (missionTokens.Exists(mt)) ?? MTL
			for (int i=0; i<missionTokens.Count; i++) {
				if (missionTokens[i].GetInstanceID == mt.id) {
					// duplicate token found, so abort the insert
					uniqueToken = false;
					break;
				}
			}
		}

		// insert if unique.
		if (uniquetoken) {
			missionTokens.Add(mt);
		}

	}

	public bool IsMissionComplete(int missionID) {
		bool isComplete = false;
		if (missionID < missions.Count) {
			if (missions[missionID].status == Mission.missionStatus.MS_Completed) {
				isComplete = true;
			}
		}
		return isComplete;
	}

	public bool Validate(Mission m) {
		bool missionComplete = true; // woudl prefer this reverse, and set to false to start.

		// a mission with no tokens is always false.
		if (m.tokens.Count <= 0) {
			missionComplete = false;
		}

		// for all tokens in the mission.
		// todo make this a lambda?
		for (int i=0; i<m.tokens.Count; i++) {

			// search for each one of the tokens
			// if it is found, then abort - this mission is not compeleted yet.
			bool tokenFound = false;
			for (int j=0; j<missionTokens.Count; j++) {
				if (missionTokens[j] != null && (m.tokens[i].id == missionTokens[j].id)) {
					tokenFound = true;
					break;
				}
			}

			// at this point, if the token was not found, it's incomplete
			if (!tokenFound) {
				missionComplete = false;
				break;
			}
		}

		// award the player if the mission is finished.
		if (missionComplete) {
			// get the playerData and add to score.
			// TODO: move this to a static until class.
			GameObject go = GameObject.Find ("Player");
			if (go == null) {
				go = GameObject.Find("Player1");
			}

			if (go) {
				PlayerData pd = go.GetComponent<PlayerData>();
				if (pd) {
					pd.AddScore(m.points);
				}
			}
		}

		return missionComplete;
	}

	/**
	 * ValidateAllMissions
	 * 
	 * For each mission, if it is finished, reward the player
	 */
	void ValidateAllMissions() {

		for (int i=0; i<missions.Count; i++) {
			Mission m = missions[i];

			if (m.status == Mission.MissionStatus.MS_ForceComplete) {
				// complete it
				m.InvokeReward();
				m.status = Mission.MissionStatus.MS_Invalid;
			}

			// if not completed and not invalid check it.
			if ((m.status != Mission.MissionStatus.MS_Completed)
			    && m.status != Mission.MissionStatus.MS_Invalid)) {

				bool missionSuccess = Validate(m);

				if (missionSuccess) {
					m.InvokeReward();
					//TODO: figure out why this is not also setting status to completed
				}
			}
		}
	}
}
