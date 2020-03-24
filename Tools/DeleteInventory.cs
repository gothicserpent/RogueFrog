using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class DeleteInventory : MonoBehaviour
{



// Start is called before the first frame update
void Start()
{
DeleteTheInventory();
}

// Update is called once per frame
void Update()
{

}

public void DeleteTheInventory()
{

	bool isAPurchasableItem = false;

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

}

}
