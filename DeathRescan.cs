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

public class DeathRescan : MonoBehaviour
{
    protected bool ScanCompleted = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
          DeathAndLifeCycles();
    }

    protected virtual void DeathAndLifeCycles()
    {
      //Debug.Log(_animator.GetBool("Alive"));
      //Debug.Log(Time.frameCount);
      //get the alive boolean to see if the animator should be stopped to prevent death anims from playing over each other
      //if (_animator!=null && _animator.GetBool("Alive") == false && Time.frameCount > 10) // need to wait 10 frames because there's a delay error here, alive is not set until a few frames in
      if (GetComponent<Health>().CurrentHealth == 0 && !ScanCompleted) //if (healthobj.CurrentHealth == 0 & !ScanCompleted)
      {
        ScanCompleted = true;
        Invoke("CompleteScan", 0.5f);
      }
    }

    //Invoke("CompleteScan", 0.5f);

    protected void CompleteScan()
    {
      GameObject.Find("A*").GetComponent<AstarPath>().Scan(); //actually reference the A* gameobject to do a scan half a second after a door is opened
      Debug.Log("Scanning");
    }

}
