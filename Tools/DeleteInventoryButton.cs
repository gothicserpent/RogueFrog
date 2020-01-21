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

	bool isAPurchasableItem = false;

	if (amountClicks==0)
	{
		gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Deleting inventory, Are you sure? (purchased items are kept)";
		gameObject.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.red;
		amountClicks++;
	}
	else if (amountClicks == 1)
	{
		//perform delete

		for (int i = 0; i<SaveManager.Instance.InventoryItems.Length; i++)
		{

			isAPurchasableItem=false;

			for (int j = 0; j<SaveManager.Instance.PurchasableInventoryItems.Length; j++)
			{
				if (SaveManager.Instance.InventoryItems[i].ItemID==SaveManager.Instance.PurchasableInventoryItems[j].ItemID) isAPurchasableItem=true;
			}

			if(!isAPurchasableItem) //only delete non purchasable items
			{
				SaveManager.Instance.INIKeyDelete("Inventory",SaveManager.Instance.InventoryItems[i].ItemID);
			}

		}

		gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Deleted inventory (changes take effect next screen)";
		gameObject.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.white;
		amountClicks++;
	}

}

}
