using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour 
{

	public List<InventoryItem> inventoryObjects = new List<InventoryItem>();

	public int numCells;
	public float height;
	public float width;
	public float yPosition;
	public MissionManager missionManager;

	// Use this for initialization
	void Start () 
	{
		GameObject go = GameObject.Find ("Game");
		if (go) {
			missionManager = go.GetComponent<MissionManager>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onGUI() {
		DisplayInventory();
	}

	void Insert(InteractiveObj iObj) 
	{
		// slot into first available slot.
		ObjectInteraction oi = iObj.OnCloseEnough;

		InventoryItem ii = new InventoryItem();
		ii.item = iObj.gameObject;
		ii.quantity = 1;
		ii.displayTexture = oi._texture;
		ii.item.SetActive(false);
		inventoryObjects.Add (ii);

		// update mission manager
		MissionToken mt = ii.item.GetComponent<MissionToken>();
		if (mt!=null) {
			_missionManager.add(mt);
		}

		// if there is a popinfo, instantiate it on pick up.
		Instantiate (ii.item.GetComponent<CustomGameObj>().popUpInfo, Vector3.zero, Quaternion.identity);
	}

	void Add(InteractiveObj iObj) 
	{
		ObjectInteraction oi = iObj.OnCloseEnough;

		switch(oi.interactionType) 
		{
			case (ObjectInteraction.InteractionType.Unique):
				Insert(iObj);
				break;

			case (ObjectInteraction.InteractionType.Accumulate):
				bool inserted = false;
				
				// find object of same type to increase the count.
			CustomGameObj cgo = iObj.gameObject.GetComponent<CustomGameObj>();
			CustomGameObj.CustomGameObjectType ot = CustomGameObj.CustomGameObjectType.Invalid;

			if (cgo != null) {
				ot = cgo.objectType;
			}

			for (int i = 0; i < inventoryObjects.Count; i++) {
				CustomGameObj cgoi = inventoryObjects[i].item.GetComponent<CustomGameObj>();
				CustomGameObj.CustomGameObjectType io = CustomGameObj.CustomGameObjectType.Invalid;
				if (cgoi != null) 
				{
					io = cgoi.objectType;
				}

				if (ot == io) 
				{
					inventoryObjects[i].quantity++;
					// add token from this object to mission Manager to track it
					MissionToken mt = iObj.gameObject.GetComponent<missionToken>();
					if (mt != null) {
						_missionMgr.add(mt);
					}

					iObj.gameObject.SetActive(false);
					inserted = true;
					break;
				}
			}

			// if we get this far, it means a dupe was found in the inventory!
			if (inserted) {
				Insert(iObj);
			}
				break;
		}

	}

	/**
	 * iterates through the inventory and display items 
	 * also gives opportunity to click and 'use' objects (show their popup)
	 */
	void DisplayInventory()
	{
		Texture t = null;
		
		float sw = Screen.width;
		float sh = Screen.height;
		
		int totalCellsToDisplay = inventoryObjects.Count;
		for (int i = 0; i < totalCellsToDisplay; i++)
		{
			InventoryItem ii = inventoryObjects[i];
			t = ii.displayTexture;
			int quantity = ii.quantity;
			
			float totalCellLength = sw - (numCells*width);
			Rect r = new Rect(totalCellLength - (0.5f * (totalCellLength)) + (width * i), 
			                  (yPosition * sh), 
			                  width, 
			                  height);
			if (GUI.Button(r, t))
			{
				// todo - fill in what to do when user clicks on an item
				if (ii.popup == null)
				{
					ii.popup = (GameObject)Instantiate (ii.item.GetComponent<customGameObject>().popUpInfo, Vector3.zero, Quaternion.identity);
				}
				else
				{
					Destroy(ii.popup);
					ii.popup = null;
				}
			}
			
			Rect r2 = new Rect(totalCellLength - 0.5f*(totalCellLength) + (width*i), yPosition*sh, 0.5f*width, 0.5f*height);
			string s = quantity.ToString();
			GUI.Label(r2, s);
		}
	}
}
