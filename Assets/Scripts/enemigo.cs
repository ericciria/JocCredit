using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{

    public Animator anim;

    public float rangoAlerta , rangoAprop;

    public LayerMask capaDelJugador;

    bool estarAlerta, estarAprop;

    public Transform jugador, attackPosition;

    public float speed;

    public bool perseguir, dead, atacant;

    public bool comprovar;

    public int vida;

    public ParticleSystem sang;
    public Rigidbody rb;
    private PlayerController player;

    


    private void Awake()
    {
        sang = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        comprovar = true;
        dead = false;
        player = GameObject.Find("Player/Body").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (comprovar && !dead)
        {
            estarAlerta = Physics.CheckSphere(transform.position, rangoAlerta, capaDelJugador);
            estarAprop = Physics.CheckSphere(transform.position, rangoAprop, capaDelJugador);
            if (estarAlerta && !estarAprop)
            {
                //transform.LookAt(jugador);


                transform.LookAt(new Vector3(jugador.position.x, transform.position.y, jugador.position.z));

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(jugador.position.x, transform.position.y, jugador.position.z), speed * Time.deltaTime);
                anim.SetBool("perseguir", true);
            }
            else if(estarAlerta && estarAprop && !atacant)
            {
                anim.SetBool("perseguir", true);
                StartCoroutine(Atacar());
            }
            else
            {
                anim.SetBool("perseguir", false);
            }
        }

    }

    IEnumerator Atacar()
    {
        atacant = true;
        comprovar = false;
        anim.SetBool("atack", true);
        yield return new WaitForSeconds(0.7f);
        if(Physics.CheckSphere(attackPosition.position, rangoAprop / 2, capaDelJugador))
        {
            player.activarSang(1);
        }
        yield return new WaitForSeconds(1.3f);
        anim.SetBool("atack", false);
        atacant = false;
        comprovar = true;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoAlerta);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, rangoAprop/2);
    }
}
