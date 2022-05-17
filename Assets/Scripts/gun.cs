using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    bool cantshoot;
    private PlayerController player;

    private void Start()
    {
        cantshoot = true;
        player = gameObject.GetComponentInParent<PlayerController>();
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
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * player.shootSpeed;
        yield return new WaitForSeconds(1/player.shootRate);
        cantshoot = true;

    }
}