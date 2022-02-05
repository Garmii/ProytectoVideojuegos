using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public GameObject Bullet;
    public Transform firePoint;
    public Animator animator;
    private bool canShoot = true;
    
    [SerializeField] private GameObject player;
    private GameObject spawnedPlayer;

    private void Start()
    {
        spawnedPlayer = FindObjectOfType<Player>().gameObject;
    }
    


    private void Update()
    {
        
        if (spawnedPlayer == null) return;
        
        float distance = transform.position.x - spawnedPlayer.transform.position.x;

        if (canShoot == true && (distance < 10 && distance > 0))
        {
            animator.SetTrigger("Fire");
            Shoot();
            canShoot = false;
            Invoke("CooldownUp", 2);
        }
    }


    private void Shoot()
    {
        FindObjectOfType<AudioManager>().PlaySound("Cannon");
        Instantiate(Bullet, firePoint.position, Quaternion.identity);
    }

    private void CooldownUp()
    {
        canShoot = true;
    }
}
