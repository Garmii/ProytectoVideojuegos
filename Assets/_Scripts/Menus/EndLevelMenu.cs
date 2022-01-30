using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelMenu : MonoBehaviour
{


    public GameManager gameManager;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           gameManager.CompleteLevel();
        }
    }
}
