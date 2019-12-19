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

public class ToadTargetDistance : MonoBehaviour
{

    Animator animator;
    IAstarAI ai;
    // Start is called before the first frame update
    void Start()
    {
      animator = GetComponentInChildren<Animator>();
      ai = GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void Update()
    {
      animator.SetFloat("TargetDistance",ai.remainingDistance);
    }
}
