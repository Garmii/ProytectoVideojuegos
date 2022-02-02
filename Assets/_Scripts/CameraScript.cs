using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform player;

    private void Update()
    {
        transform.position = player.position;
    }


}
