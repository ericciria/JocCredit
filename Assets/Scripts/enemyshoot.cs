using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyshoot : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public Animator anim;

    public float rangoAlerta;

    public LayerMask capaDelJugador;

    public bool estarAlerta;

    public Transform jugador;

    public float speed;

    public bool dead, atacant;

    public bool comprovar;

    public int vida, maxVida;

    public ParticleSystem sang;
    public Rigidbody rb;
    private PlayerController player;

    public Image sliderhealth;

    bool canShoot;


    private void Awake()
    {
        sang = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        sliderhealth = GetComponentInChildren<Image>();
    }
    void Start()
    {
        comprovar = true;
        dead = false;
        player = GameObject.Find("Player/Body").GetComponent<PlayerController>();
        jugador = player.transform;
        vida = maxVida;
    }

    // Update is called once per frame
    void Update()
    {

        if (comprovar && !dead)
        {
            estarAlerta = Physics.CheckSphere(transform.position, rangoAlerta, capaDelJugador);
            if (estarAlerta)
            {
                //transform.LookAt(jugador);


                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position), 5 * Time.deltaTime);
                anim.SetBool("shoot", true);
                if(!atacant)
                {
                    StartCoroutine(Atacar());
                }
                
            }
            else
            {
                anim.SetBool("shoot", false);
            }
        }
     

    }

    IEnumerator Atacar()
    {
        atacant = true;

        
        yield return new WaitForSeconds(0.5f);

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * player.shootSpeed;
        bullet.GetComponent<MeshRenderer>().material.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        atacant = false;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoAlerta);
    }
}
