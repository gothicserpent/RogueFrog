using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class DeleteInventoryButton : MonoBehaviour
{

int amountClicks;

// Start is called before the first frame update
void Start()
{

	amountClicks=0;

}

// Update is called once per frame
void Update()
{

}

public void ClickDelete()
{

	if (amountClicks==0)
	{
		gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Deleting inventory, Are you sure? (purchased items are kept)";
		gameObject.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.red;
		amountClicks++;
	}
	else if (amountClicks == 1)
	{
		//perform delete
		INIDelete("Inventory","FrogAmberStaff");
		INIDelete("Inventory","FrogBronzeCoin");
		INIDelete("Inventory","FrogEmeraldStaff");
		INIDelete("Inventory","FrogGoldCoin");
		INIDelete("Inventory","FrogSapphireStaff");
		INIDelete("Inventory","FrogSilverCoin");
		INIDelete("Inventory","FrogSword");
		INIDelete("Inventory","FrogTopazStaff");
		gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Deleted inventory (purchased items were kept)";
		gameObject.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.white;
		amountClicks++;
	}

}

protected void INIDelete(string section, string key)
{
	INIParser ini = new INIParser();
	// Open the save file. If the save file does not exist, INIParser automatically create
	// one
	ini.Open(SaveManager.Instance.DetermineINIPath());
	// Read the score. If the section/key does not exist, default score to 0
	//if (_targetInventory.InventoryTypes == IventoryType.Main)

	ini.KeyDelete(section, key);
	//inivalue += 1;
	//ini.WriteValue("InAppPurchases",removeads,1);  //make the ads be gone!
	//Debug.Log("test: " + _targetInventory.transform.parent.name);
	ini.Close();
}



}
