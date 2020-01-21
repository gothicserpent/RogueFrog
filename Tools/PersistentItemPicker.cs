using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace MoreMountains.InventoryEngine
{
/// <summary>
/// Add this component to an object so it can be picked and added to an inventory
/// </summary>
public class PersistentItemPicker : ItemPicker
{

/// <summary>
/// Describes what happens when the object is successfully picked
/// </summary>
protected override void PickSuccess()
{
	base.PickSuccess();

	//int itemcount = _targetInventory.GetQuantity(Item.ItemID);
	//Debug.Log("Pick Success" + Item.ItemID + " total items: " + itemcount);
	//Debug.Log("Saving to: " + SaveManager.Instance.DetermineINIPath());

	int inivalue = SaveManager.Instance.INIRead("Inventory",Item.ItemID);
	inivalue+=1;
	SaveManager.Instance.INIWrite("Inventory",Item.ItemID,inivalue);

	//INIParser ini = new INIParser();
	// Open the save file. If the save file does not exist, INIParser automatically create
	// one
	//ini.Open(SaveManager.Instance.DetermineINIPath());
	// Read the score. If the section/key does not exist, default score to 0
	//if (_targetInventory.InventoryTypes == IventoryType.Main)
	//int inivalue = ini.ReadValue("Inventory",Item.ItemID,0);
	//inivalue += 1;
	//ini.WriteValue("Inventory",Item.ItemID,inivalue);
	//Debug.Log("test: " + _targetInventory.transform.parent.name);
	//ini.Close();


}

}
}
