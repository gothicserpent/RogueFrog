using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;

/// <summary>
/// Should be added to the player prefab (not any persistent classes because it wont be called when the scenes start). Equips the wand for the player if a wand is not already equipped, so that the player doesn't start empty, to make it slightly easier.
/// </summary>
public class EquipWandOnStart : MonoBehaviour
{
// Start is called before the first frame update
void Start()
{
	Invoke("EquipItem", 0.2f);
}

private void EquipItem()
{
	SaveManager.Instance.ini.Open(SaveManager.Instance.DetermineINIPath());
	Inventory FrogWeaponInventory = null;
	List<int> FrogWeaponInventoryList = null;
	Inventory FrogInventory = null;
	List<int> FrogInventoryList = null;
	bool FrogWeaponInventoryHasItems = false;

	if (GameObject.Find("FrogInventory") != null) FrogInventory = GameObject.Find("FrogInventory").GetComponent<Inventory>();

	if (GameObject.Find("FrogWeaponInventory") != null) FrogWeaponInventory = GameObject.Find("FrogWeaponInventory").GetComponent<Inventory>();

	//check to see if the weapon inventory has any items
	for (int i = 0; i<SaveManager.Instance.InventoryItems.Length; i++)
	{
		if (SaveManager.Instance.ini.ReadValue("Inventory",SaveManager.Instance.InventoryItems[i].ItemID,0) >= 1)
		{
			FrogWeaponInventoryList = FrogWeaponInventory.InventoryContains(SaveManager.Instance.InventoryItems[i].ItemID);
			if (FrogWeaponInventoryList.Count > 0)
			{
				FrogWeaponInventoryHasItems = true;
				break;
			}
		}
	}

	if (!FrogWeaponInventoryHasItems) //if the weapon inventory has no items
	{
		for (int i = 0; i<SaveManager.Instance.InventoryItems.Length; i++)
		{
			if (SaveManager.Instance.ini.ReadValue("Inventory",SaveManager.Instance.InventoryItems[i].ItemID,0) >= 1)
			{
				FrogInventoryList = FrogInventory.InventoryContains(SaveManager.Instance.InventoryItems[i].ItemID);

				//Debug.Log("Trying to equip" + SaveManager.Instance.InventoryItems[i].ItemID + ", from index " + FrogInventoryList[FrogInventoryList.Count - 1]);
				FrogInventory.EquipItem(SaveManager.Instance.InventoryItems[i], FrogInventoryList[FrogInventoryList.Count - 1]); //equip wand

				break; //break out of loop as soon as the wand is equipped so other wands aren't equipped
			}
		}
	}
	SaveManager.Instance.ini.Close();
}

// Update is called once per frame
void Update()
{

}


}
