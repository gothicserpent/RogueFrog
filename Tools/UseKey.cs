using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using MoreMountains.TopDownEngine;

/// <summary>
/// Delete a wooden or metal key based on string if the item is in the player inventory
/// </summary>
public class UseKey : MonoBehaviour
{
Inventory FrogInventory = null;
List<int> FrogInventoryList = null;

void Start()
{
	if (GameObject.Find("FrogInventory") != null) FrogInventory = GameObject.Find("FrogInventory").GetComponent<Inventory>(); //get the main inventory
}

// Update is called once per frame
void Update()
{
}

public void UseKeyAction(string itemName) //use a key by removing it from the inventory
{
	FrogInventoryList = FrogInventory.InventoryContains(itemName);
	if (FrogInventoryList.Count > 0)
	{
		FrogInventory.RemoveItem(FrogInventoryList[FrogInventoryList.Count - 1], 1);
	}
}
}
