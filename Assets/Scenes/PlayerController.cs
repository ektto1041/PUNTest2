using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private InputManager p1, p2;
    
    #region MonoBehaviour Callbacks
    
    private void Update()
    {
        if (p1 == null || p2 == null)
        {
            InputManager[] inputManagers = FindObjectsOfType<InputManager>();

            if (inputManagers.Length != 2) return;
            
            p1 = inputManagers[0];
            p2 = inputManagers[1];
        }
        else
        {
            float x = p1.x + p2.x;
            float y = p1.y + p2.y;

            x = x * 10f * Time.deltaTime;
            y = y * 10f * Time.deltaTime;

            transform.position = new Vector2(transform.position.x + x, transform.position.y + y);    
        }
    }

    #endregion
}
