using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    bool cantshoot;

    private void Start()
    {
        cantshoot = true;
    }
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (cantshoot)
            {
                StartCoroutine(shoot());
            }
           
            
        }
    }

    IEnumerator shoot()
    {
        cantshoot = false;
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        yield return new WaitForSeconds(1);
        cantshoot = true;

    }
}