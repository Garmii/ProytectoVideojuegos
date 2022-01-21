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
        transform.position = new Vector3(prefabManager.spawnedPlayer.transform.position.x, prefabManager.spawnedPlayer.transform.position.y, transform.position.z);
    }
}
