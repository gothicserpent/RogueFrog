/*
 * AI ACTION TO ENABLE MOVING IN A* PATHS
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
public class AIActionAStarMove : AIAction
{

Animator animator;
IAstarAI ai;

/// <summary>
/// On init we grab our CharacterMovement ability
/// </summary>
public override void Initialization()
{
	animator = GetComponentInChildren<Animator>();
	ai = GetComponent<IAstarAI>();
}

/// <summary>
/// On PerformAction we do nothing
/// </summary>
public override void PerformAction()
{
	if(!ai.canMove)
	{
		ai.canMove = true;
	}
	if(!animator.GetBool("Moving"))
	{
		animator.SetBool("Moving",true);
	}
}
}
}
