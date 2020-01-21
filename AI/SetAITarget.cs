/*
 * SET AI TARGET FOR ASTAR PROJECT PER ENEMY
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SetAITarget : MonoBehaviour
{
IAstarAI ai;

void Start()
{
	ai = GetComponent<IAstarAI>();
}

// Update is called once per frame
void Update()
{
	if ( ai != null && ai.canMove) ai.destination = GameObject.FindWithTag("Player").transform.position;
}

}
