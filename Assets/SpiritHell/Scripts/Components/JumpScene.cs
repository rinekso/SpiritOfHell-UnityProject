﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpScene : MonoBehaviour
{
    public void jumpScene(string scene){
        SceneManager.LoadScene(scene);
    }
}
