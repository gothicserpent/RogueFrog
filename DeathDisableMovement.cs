/*
 * RESCAN FOR A* WHEN AN OBJECT IS REMOVED SO AI CAN PATH THRU THE AREA
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

using Pathfinding;
//using Health;

public class DeathDisableMovement : MonoBehaviour
{
    // Start is called before the first frame update
    IAstarAI ai;
    Health health;
    void Start()
    {
      ai = GetComponent<IAstarAI>();
      health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
      CheckForDeath();
    }

    protected virtual void CheckForDeath()
    {
      if (health.CurrentHealth == 0) //if (healthobj.CurrentHealth == 0 & !ScanCompleted)
      {
        if(ai.canMove)
        {
          ai.canMove = false;
        }
      }
    }

}
