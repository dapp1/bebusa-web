using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private bool isMoving;
    public Action startMoving;
    void Start()
    {
        isMoving = false;
    }

    void Update()
    {
        if(SwipeManager.instance.tap && !isMoving)
        {
            isMoving = true;
            startMoving?.Invoke();
        }
    }
}
