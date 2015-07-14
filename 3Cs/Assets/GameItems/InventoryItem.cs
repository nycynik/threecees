using UnityEngine;
using System.Collections;

/*
 * InventoryItem
 * 
 * pojo for inteventory items.
 * NOTE: sysstem serializable annotation gets it to show in unity UI
 */

[System.Serializable]
public class InventoryItem {

	public Texture texture = null;
	public GameObject item = null;
	public GameObject popup = null;
	public int quantity = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
