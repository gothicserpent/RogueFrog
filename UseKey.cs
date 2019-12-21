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

  Inventory[] allinventories;
  Inventory inventory;

    void Start()
    {
      allinventories = GameObject.Find("Frog Managers").GetComponentsInChildren<Inventory>();
      for (int i = 0; i < allinventories.Length; i++)
        {
          if (allinventories[i].InventoryType == (Inventory.InventoryTypes)Inventory.InventoryTypes.Main) //access main inv
          {
            inventory = allinventories[i];
          }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UseKeyAction(string itemName)
    {
      List<int> list = inventory.InventoryContains(itemName);
      if (list.Count > 0)
      {
        inventory.RemoveItem(list[list.Count - 1], 1);
      }
    }
}
