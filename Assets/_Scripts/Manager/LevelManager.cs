using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager instance;
    
    public Transform respawnPoint;
    public GameObject playerPrefab;
    public CinemachineVirtualCamera vCamera;

    private void Awake()
    {
        instance = this;
    }

    public void Respawn()
    {
        GameObject player =  Instantiate(playerPrefab,respawnPoint.position,Quaternion.identity);
        vCamera.Follow = player.transform;
    }
}
