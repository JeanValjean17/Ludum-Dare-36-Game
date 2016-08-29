using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class LevelsContainer : MonoBehaviour {

    public static LevelsContainer sceneManager;
    public static float HealthPlayer = 100.0f;

    public List<int> LevelList = new List<int>();
    [HideInInspector]
    public bool isLevelZero = true;

    void Awake()
    {
        if (!sceneManager)
        {
            sceneManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

      
}
