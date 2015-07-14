using UnityEngine;
using System.Collections;

public class ObjectInteraction : MonoBehaviour {

	public enum InteractionAction
	{
		Invalid = -1,
		PutInInventory = 0,
		Use = 1
	}

	public enum InteractionType 
	{
		Invaild = -1,
		Unique = 0,
		Accumulate = 1
	}

	public InteractionAction interaction;
	public InteractionType interactionType;

	public Texture texture;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void HandleInteraction() {

		InventoryMgr iManager = null;

		// find player
		GameObject player = GameObject.Find ("Player");
		if (player == null) {
			player = GameObject.Find ("Player1");
		}

		if (player) {
			iManager = player.GetComponent<inventoryMgr>();

			switch (interaction) {
				case InteractionAction.PutInInventory: 
				if (iManager) {
					iManager.Add(this.gameObject.GetComponent<InteractiveObj>());
				}
				break;
			}

		}
	}

}
