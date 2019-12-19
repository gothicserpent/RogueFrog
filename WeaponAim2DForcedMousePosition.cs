using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine.UI;

namespace MoreMountains.TopDownEngine
{
    [RequireComponent(typeof(Weapon))]
    public class WeaponAim2DForcedMousePosition : WeaponAim
    {

        /// <summary>
        /// Computes the current aim direction
        /// </summary>
        protected override void GetCurrentAim()
        {
                GetMouseAim();
        }


        public virtual void GetMouseAim()
        {
          //Debug.Log("Attempting mousepos");
            #if UNITY_ANDROID || UNITY_IPHONE
                      /*
                      for each position, if the position is not on the gamepad, use that touch
                      */
                      for(int i = 0; i < Input.touchCount; i++)
              {

                          if (Input.GetTouch(i).position.x>430 && Input.GetTouch(i).position.x<1700 && Input.GetTouch(i).position.y<430)
                          {
                              _mousePosition = Input.GetTouch(i).position;
                          }
                          else if (Input.GetTouch(i).position.y>=430)
                          {
                              _mousePosition = Input.GetTouch(i).position;
                          }

              }



                       #endif
        }



        /// <summary>
        /// Every frame, we compute the aim direction and rotate the weapon accordingly
        /// </summary>
        protected override void Update()
        {
            GetCurrentAim();
        }

    }
}
