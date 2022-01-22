using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private PrefabManager prefabManager;

    

    private void Start()
    { 
        
    }

    private void Update()
    {
        transform.position = new Vector3(prefabManager.spawnedPlayer.transform.position.x+4, prefabManager.spawnedPlayer.transform.position.y+2, transform.position.z);
    }
}
