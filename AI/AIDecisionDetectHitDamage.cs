/*
 * AI DECISION TO MOVE TOWARDS PLAYER AS SOON AS THERE IS DAMAGE TO AI
 */

﻿using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.TopDownEngine
{
/// <summary>
/// This decision will return true if an object on its TargetLayer layermask is within its specified radius, false otherwise. It will also set the Brain's Target to that object.
/// </summary>
public class AIDecisionDetectHitDamage : AIDecision
{

Health health;
/// <summary>
/// On init we grab our Character component
/// </summary>
public override void Initialization()
{
	health = GetComponent<Health>();
}

/// <summary>
/// On Decide we check for our target
/// </summary>
/// <returns></returns>
public override bool Decide()
{
	return DetectTarget();   //this gets passed to return value of transition to indicate positive value
}

/// <summary>
/// Returns true if a target is found within the circle
/// </summary>
/// <returns></returns>
protected virtual bool DetectTarget()
{
	if (health.CurrentHealth < health.MaximumHealth)
	{
		_brain.Target = GameObject.FindWithTag("Player").transform; // make the brain target properly for AIActionMoveTowardsTarget2D.cs
		return true; // return a true transition if the current health is ever lower than max
	}
	else return false;
}

}
}
