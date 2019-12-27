/*
 * HANDLE MELEE ENEMIES AI, CONTROLLER, AND ANGLE / DISTANCE TO PLAYER SO THEY CAN ATTACK AND ROTATE PROPERLY
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

using Pathfinding;

namespace MoreMountains.TopDownEngine
{
/// <summary>
/// Add this ability to a character and it'll rotate or flip to face the direction of movement or the weapon's, or both, or none
/// Only add this ability to a 2D character
/// </summary>
[AddComponentMenu("TopDown Engine/Character/Abilities/Character Melee")]
public class CharacterMelee : CharacterAbility
{

[Information("Can use the Degree / TargetDistance animation parameters to make AI follow in a realistic way.", MoreMountains.Tools.InformationAttribute.InformationType.Info, false)]
protected float _degree;
protected float _targetDistance;
protected int _degreeAnimationParameter;
protected int _targetDistanceAnimationParameter;
protected IAstarAI ai;

/// <summary>
/// On awake we init our facing direction and grab components
/// </summary>
protected override void Awake()
{
	base.Awake();

	ai = GetComponent<IAstarAI>();

}

/// <summary>
/// On process ability, we flip to face the direction set in settings
/// </summary>
public override void ProcessAbility()
{
	base.ProcessAbility();

	FindDegreeAndTargetDistance();
	DeathAndLifeCycles();
}

protected void FindDegreeAndTargetDistance()
{
	/*
	   var deltaX = x2 - x1;
	   var deltaY = y2 - y1;
	   var rad = Math.atan2(deltaY, deltaX); // In radians
	   Then you can convert it to degrees as easy as:

	   var deg = rad * (180 / Math.PI)

	   var rad = Math.atan2(_verticalDirection, _horizontalDirection); // In radians
	   Then you can convert it to degrees as easy as:

	   var deg = Math.atan2(_verticalDirection, _horizontalDirection) * (180 / Math.PI)
	 */

	_degree = Mathf.Atan2(GameObject.FindWithTag("Player").transform.position.y-_controller.transform.position.y, GameObject.FindWithTag("Player").transform.position.x-_controller.transform.position.x) * Mathf.Rad2Deg;     // the degree between the player and the AI controller position to do things like animate for left, right, up and down movement
	//Debug.Log(_degree);

	_targetDistance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, _controller.transform.position);     // gets the distance to help with attack animation

	//Debug.Log(_targetDistance);
}

protected void DeathAndLifeCycles()
{
	//Debug.Log(_animator.GetBool("Alive"));
	//Debug.Log(Time.frameCount);
	//get the alive boolean to see if the animator should be stopped to prevent death anims from playing over each other
	if (_animator!=null && _animator.GetBool("Alive") == false && Time.frameCount > 10)   // need to wait 10 frames because there's a delay error here, alive is not set until a few frames in
	{
		//Debug.Log("Destroying animator");
		//Destroy(_animator); //look into changing this so that when the character presses reload, the ai animator isn't broken
		//m_Animator.GetComponent<Animator>().enabled = false;
		_animator.GetComponent<Animator>().enabled = false;
		ai.canMove = false; //stop moving on death
		//Debug.Log("setting ai.canMove to false");
	}

	if (_animator!=null && _animator.GetBool("Alive") == true && Time.frameCount > 10)   // need to wait 10 frames because there's a delay error here, alive is not set until a few frames in
	{
		//Debug.Log("Destroying animator");
		//Destroy(_animator); //look into changing this so that when the character presses reload, the ai animator isn't broken
		_animator.GetComponent<Animator>().enabled = true;
		//ai.canMove = false; //stop moving on death
		//Debug.Log("setting ai.canMove to false");
	}


}


/// <summary>
/// Adds required animator parameters to the animator parameters list if they exist
/// </summary>
protected override void InitializeAnimatorParameters()
{
	RegisterAnimatorParameter("Degree", AnimatorControllerParameterType.Float, out _degreeAnimationParameter);
	RegisterAnimatorParameter("TargetDistance", AnimatorControllerParameterType.Float, out _targetDistanceAnimationParameter);
}

/// <summary>
/// At the end of each cycle, sends Jumping states to the Character's animator
/// </summary>
public override void UpdateAnimator()
{
	MMAnimatorExtensions.UpdateAnimatorFloat(_animator, _degreeAnimationParameter, _degree, _character._animatorParameters);
	MMAnimatorExtensions.UpdateAnimatorFloat(_animator, _targetDistanceAnimationParameter, _targetDistance, _character._animatorParameters);
}

}
}
