using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    AudioSource tuto;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    bool cantshoot;
    private PlayerController player;
    public Color color;
    public Vector3 tamanyBala;
    public ParticleSystem flash;
    private movment asd;

    private void Start()
    {
        cantshoot = true;
        player = gameObject.GetComponentInParent<PlayerController>();
        color = new Color(0.5f, 0.5f, 0.5f);
        tamanyBala = new Vector3(0.3f,0.3f,0.3f);
        flash = GetComponentInChildren<ParticleSystem>();
        asd = player.gameObject.GetComponentInParent<movment>();

    }
    void Update()
    {
        tuto = GetComponent<AudioSource>();
        if (Input.GetMouseButton(0) && !asd.isPaused && !player.isDead)
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
        flash.Play();
        tuto.Play();
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * player.shootSpeed;
        bullet.GetComponent<MeshRenderer>().material.color = color;
        bullet.transform.localScale = tamanyBala;
        bullet.GetComponent<Bullet>().extraAttack = player.attack;
        yield return new WaitForSeconds(1/player.shootRate);
        cantshoot = true;

    }
}