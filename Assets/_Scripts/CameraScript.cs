using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameObject spawnedPlayer;
    

    private void Start()
    {
       
    }

    private void Update()
    {
        if (spawnedPlayer != null)
        {
            spawnedPlayer = FindObjectOfType<Player>().gameObject;
            transform.position = new Vector3(spawnedPlayer.transform.position.x + 4,
                spawnedPlayer.transform.position.y + 2, transform.position.z);
        }
    }
}
