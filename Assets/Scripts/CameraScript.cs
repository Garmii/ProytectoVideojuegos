using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject knight1; //Referencia al Jugador


    void Update()
    {
        Vector3 position = transform.position;
        position.x = knight1.transform.position.x; //la posicion en x del vector es la posicion en x del jugador
        transform.position = position;
    }
}
