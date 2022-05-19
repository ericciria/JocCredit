using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{

    Animator anim;

    public float rangoAlerta , rangoAprop;

    public LayerMask capaDelJugador;

    bool estarAlerta, estarAprop;

    public Transform jugador;

    public float speed;

    public bool perseguir, dead, atacant;

    public bool comprovar;

    public int vida;

    public ParticleSystem sang;
    private PlayerController player;

    


    private void Awake()
    {
        sang = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        comprovar = true;
        player = GameObject.Find("Player/Body").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (comprovar)
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
        yield return new WaitForSeconds(2);
        player.health--;
        anim.SetBool("atack", false);
        atacant = false;
        comprovar = true;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoAlerta);
        Gizmos.color = Color.red;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Destroy(collision.gameObject);
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            sang.Play();
            Debug.Log("hola");
            vida--;
            if (vida == 5)
            {
                StartCoroutine(RebreMal());
            }
            if(vida<=0)
            {
                comprovar = false;
                anim.Play("dead", -1, 0f);
                GetComponent<BoxCollider>().enabled=false;
            }
            
        }


    }

    IEnumerator RebreMal()
    {
        comprovar = false;
        anim.Play("hurt", -1, 0f);
        yield return new WaitForSeconds(2.5f);
        if (!dead)
        {
            comprovar = true;
        }
        
    }


}
