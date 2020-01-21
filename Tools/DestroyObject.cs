/*
 * HIDE OR UNHIDE WALLS FOR SWITHCES
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class DestroyObject : MonoBehaviour
{

void Start()
{
}

void Update()
{
}

// DESTROY THE OBJECT

public virtual void SetInactive()
{
	gameObject.SetActive(false);
}

// SHOW THE OBJECT

public virtual void SetActive()
{
	gameObject.SetActive(true);
}

}
