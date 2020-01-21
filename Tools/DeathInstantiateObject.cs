/*
 * RESCAN FOR A* WHEN AN OBJECT IS REMOVED SO AI CAN PATH THRU THE AREA
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

//using Pathfinding;
//using Health;

public class DeathInstantiateObject : MonoBehaviour
{
// Start is called before the first frame update
Health health;
protected bool instantiated = false;

[Header("Death")]
/// a gameobject (usually a particle system) to instantiate when the healthbar reaches zero
public GameObject InstantiatedOnDeath;



void Start()
{
	health = GetComponent<Health>();
}

// Update is called once per frame
void Update()
{
	CheckForDeath();
}

protected virtual void CheckForDeath()
{
	if (health.CurrentHealth == 0 && InstantiatedOnDeath != null && !instantiated)
	{
		instantiated = true;
		Instantiate(InstantiatedOnDeath, this.transform.position, this.transform.rotation);
	}
}

}
