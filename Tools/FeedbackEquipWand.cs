using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;

namespace MoreMountains.Feedbacks
{
/// <summary>
/// This feedback will trigger an equip pickup. I wrote this to auto equip items due to a bug where the resource item file does not auto equip items on pickup
/// </summary>
[AddComponentMenu("")]
[FeedbackHelp("This will trigger equipping of the most recent wand.")]
[FeedbackPath("GameObject/EquipWand")]
public class FeedbackEquipWand : MMFeedback
{
/// sets the inspector color for this feedback
	#if UNITY_EDITOR
public override Color FeedbackColor {
	get { return MMFeedbacksInspectorColors.CameraColor; }
}
	#endif


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

/// <summary>
/// On Play we trigger a flash event
/// </summary>
/// <param name="position"></param>
/// <param name="attenuation"></param>
protected override void CustomPlayFeedback(Vector3 position, float attenuation = 1.0f)
{
	if (Active)
	{

		Invoke("EquipItem", 0.2f);

	}
}
}
}
