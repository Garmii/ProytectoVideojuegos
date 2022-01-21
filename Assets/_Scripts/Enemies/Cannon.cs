using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public GameObject Bullet;
    public Transform firePoint;
    public Animator animator;
    public Transform player;
    private bool canShoot = true;
    
    
    private void Update()
    {
        float distance = transform.position.x - player.position.x;

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
        Instantiate(Bullet, firePoint.position, Quaternion.identity);
    }

    private void CooldownUp()
    {
        canShoot = true;
    }
}
