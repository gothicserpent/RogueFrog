/*
 * LOAD A SCENE BASED ON STRING
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LoadScene(string level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

}
