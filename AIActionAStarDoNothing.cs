/*
 * AI ACTION TO DISABLE MOVING IN A* PATHS
 */

﻿using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using static namespace Pathfinding.AIPath;

using Pathfinding;

namespace MoreMountains.TopDownEngine
{

    /// <summary>
    /// As the name implies, an action that does nothing. Just waits there.
    /// </summary>
    public class AIActionAStarDoNothing : AIAction
    {

        IAstarAI ai;

        /// <summary>
        /// On init we grab our CharacterMovement ability
        /// </summary>
        protected override void Initialization()
        {
          ai = GetComponent<IAstarAI>();
        }

        /// <summary>
        /// On PerformAction we do nothing
        /// </summary>
        public override void PerformAction()
        {
          if(ai.canMove)
          {
            ai.canMove = false;
            //Debug.Log("setting ai.canMove to false");
          }
        }
    }
}
