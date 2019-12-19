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

public class SpawnRate : MonoBehaviour
{
    // Start is called before the first frame update
    //protected bool instantiated = false;

    [Header("Object to Spawn")]
    /// a gameobject (usually a particle system) to instantiate when the healthbar reaches zero
    public GameObject SpawnObject;
    [Header("Spawn Effect")]
    public GameObject SpawnEffect;

    [Header("Rate of Spawn")]
    public float FireRate;
    protected float CurrentFire;

    [Header("Number of Times to Spawn")]
    public int TimesToSpawn = 1;
    protected int TimesSpawned = 0;

    void Start()
    {
      CurrentFire = FireRate;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
      if((CurrentFire -= Time.deltaTime)>0)// && Instantiated && Time.frameCount < 10) //spawn projectile based on fire rate
        return;

      if(!GetComponent<IAstarAI>().canMove) //only spawn the projectile if the AI is able to move (the ai brain has detected the player)
        return;

      CurrentFire = FireRate;
      Spawn();
    }

    protected virtual void Spawn()
    {
      if (TimesSpawned < TimesToSpawn && SpawnObject != null)
      {
        TimesSpawned++;
        var spawnobject = (GameObject) Instantiate(SpawnObject, this.transform.position, this.transform.rotation);
        spawnobject.GetComponentInChildren<Animator>().SetBool("Alive", true);

        if (SpawnEffect!=null)
        {
          Instantiate(SpawnEffect,transform.position,transform.rotation);
        }
      }
    }


}
