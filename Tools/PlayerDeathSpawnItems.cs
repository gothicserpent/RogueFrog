/// <summary>
/// Store player items in the INI file on death so it can be spawned in the level until the player gets them again.
/// </summary>

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;


public class PlayerDeathSpawnItems : MonoBehaviour
{
Health health;
private bool instantiated = false;

void Start()
{
	health = GetComponent<Health>();
	InvokeRepeating("CheckForDeath", 0, 0.1F);
	//InvokeRepeating("SetInstantiatedFalse", 0, 1.0F);
}

// Update is called once per frame
void Update()
{
	//CheckForDeath();
}

protected virtual void CheckForDeath()
{
	if (health.CurrentHealth == 0 && !instantiated)
	{
		instantiated = true;


		int DeathSectionNumber = SaveManager.Instance.PlayerDeathINI(this.transform.position.x,this.transform.position.y);


		SaveManager.Instance.PlayerDeathSpawnItemsFromINI(true, DeathSectionNumber); //call this on death so the items respawn without the savemanager start() having to be called. -- this executes when the player dies.

		//iniwrite all the values to the Death section for each item in the inventory, and iniwrite the X Y location at the time of death
		//when any level is loaded (try to save the script in a game manager that is a prefab), then check to see if the death section exists and if it does spawn the items in the X Y location specified
		//make sure to check if the player died multiple times, just re-write the same file, do not change x/y locations.
		//if the player dies multiple times, can have [Death1], [Death2] etc.

		//Instantiate(InstantiatedOnDeath, this.transform.position + new Vector3(offsetX, offsetY, 0), this.transform.rotation);

		Invoke("Reset", 3.0f); //reset in 2 seconds so it can be called again. if this isn't done, death sections can be written multiple times.
	}
}

protected virtual void Reset()
{
	instantiated = false;
	//SaveManager.Instance.EmptyInventory();
}
}
