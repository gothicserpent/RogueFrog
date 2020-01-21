/*
 * CAUSE THE AI TO RESCAN THE FIELD WHEN A DOOR OPENS SO ENEMIES CAN PATH THROUGH DOORWAYS
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Pathfinding;

public class DoorScan : MonoBehaviour
{

//AstarPath aiPath;
// Start is called before the first frame update
void Start()
{
}

// Update is called once per frame
void Update()
{
}

public void PathScan()
{
	Invoke("CompleteScan", 0.5f);

	//aiPath.Scan();
}

protected void CompleteScan()
{
	GameObject.Find("A*").GetComponent<AstarPath>().Scan(); //actually reference the A* gameobject to do a scan half a second after a door is opened
	//Debug.Log("Scanning");
}


}
