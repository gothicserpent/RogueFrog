/*
 * FORCE CURSOR TO BE SHOWN
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      if (!Cursor.visible) Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
      if (!Cursor.visible) Cursor.visible = true;
    }
}
