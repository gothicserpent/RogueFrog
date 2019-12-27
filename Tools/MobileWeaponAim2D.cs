
//CLASS SHOULD BE ADDED ABOVE WEAPONAIM2D COMPONENT IN PLAYABLE CHARACTERS WEAPON PREFABS SO THAT IT FIRES PROPERLY ON MOBILE
//SHOULD KEEP AN EYE ON Assets\TopDownEngine\Common\Scripts\Characters\Weapons\WeaponAim2D.cs line 206 GetMouseAim() to see if it changes
﻿using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine.UI;

namespace MoreMountains.TopDownEngine
{
[AddComponentMenu("TopDown Engine/Weapons/Mobile Weapon Aim 2D")]
public class MobileWeaponAim2D : WeaponAim2D
{

//override the getmouseaim method so that mobile users can truly aim at the targets while running or moving about.
public override void GetMouseAim()
{
	//(float)Screen.width / (float)Screen.height;
	//Debug.Log("Screen Width: " + (float)Screen.width + "Screen Height: " + (float)Screen.height);
	/*for mobile for each position, if the position is not on the gamepad, use that touch*/
	//all of the ui buttons MUST BE ANCHORED to prevent these values from being erroneous - this way when the screen size varies, the anchors value still remains the same from the extremities of the screen
	#if UNITY_ANDROID || UNITY_IPHONE
	int JoyPad = 310;   // bottom left joypad area
	int XYBATopY = 360;   // bottom right XYBA area Y
	int XYBALeftX = (int)Screen.width - 350;   // bottom right XYBA area X
	int PauseY = (int)Screen.height - 250;   // top left pause area y
	int PauseX = 225;   // top left pause area x
	int InvX = (int)Screen.width - 300;   // Top right inventory area X
	for(int i = 0; i < Input.touchCount; i++)
	{
		if (Input.GetTouch(i).position.x>JoyPad && Input.GetTouch(i).position.x<XYBALeftX && Input.GetTouch(i).position.y<XYBATopY)
		{ _mousePosition = Input.GetTouch(i).position; }
		else if (Input.GetTouch(i).position.y>=XYBATopY && Input.GetTouch(i).position.y<=PauseY)
		{ _mousePosition = Input.GetTouch(i).position; }
		else if (Input.GetTouch(i).position.y>PauseY && Input.GetTouch(i).position.x>PauseX && Input.GetTouch(i).position.x<InvX)
		{ _mousePosition = Input.GetTouch(i).position; }
	}
	#else
	_mousePosition = Input.mousePosition;
	#endif
	_mousePosition.z = 10;

	_direction = _mainCamera.ScreenToWorldPoint(_mousePosition);
	_direction.z = transform.position.z;

	_reticlePosition = _direction;

	_currentAimAbsolute = _direction - transform.position;
	if (_weapon.Owner.Orientation2D.IsFacingRight)
	{
		_currentAim = _direction - transform.position;
		_currentAimAbsolute = _currentAim;
	}
	else
	{
		_currentAim = transform.position - _direction;
	}





	//base.GetMouseAim();


	/*for mobile for each position, if the position is not on the gamepad, use that touch*/
	/*
	   bool OutOfBounds = false;

	 #if UNITY_ANDROID || UNITY_IPHONE

	   if (Input.touchCount == 0)
	   {
	    if (Input.GetTouch(0).position.x>430 && Input.GetTouch(0).position.x<1700 && Input.GetTouch(0).position.y<430)
	      { _mousePosition = Input.GetTouch(0).position; }
	    else if (Input.GetTouch(0).position.y>=430)
	      { _mousePosition = Input.GetTouch(0).position; }
	   }
	   else //more than 1 touch
	   {
	    for(int i = 0; i < Input.touchCount; i++)
	    {
	      if (Input.GetTouch(i).position.x>430 && Input.GetTouch(i).position.x<1700 && Input.GetTouch(i).position.y<430)
	        {
	          _mousePosition = Input.GetTouch(i).position;
	          OutOfBounds = true;
	          break;
	        }
	      else if (Input.GetTouch(i).position.y>=430)
	        {
	          _mousePosition = Input.GetTouch(i).position;
	          OutOfBounds = true;
	          break;
	        }
	      //else
	      //  { _mousePosition = Input.mousePosition; }
	    }

	    if (OutOfBounds == false) _mousePosition = Input.mousePosition;


	   }
	 #else
	   _mousePosition = Input.mousePosition;
	 #endif

	            _mousePosition.z = 10;

	            _direction = _mainCamera.ScreenToWorldPoint(_mousePosition);
	            _direction.z = transform.position.z;

	            _reticlePosition = _direction;

	            _currentAimAbsolute = _direction - transform.position;
	            if (_weapon.Owner.Orientation2D.IsFacingRight)
	            {
	                _currentAim = _direction - transform.position;
	                _currentAimAbsolute = _currentAim;
	            }
	            else
	            {
	                _currentAim = transform.position - _direction;
	            }

	 */

}




}
}
