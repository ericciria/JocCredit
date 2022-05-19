using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{

    public Animator anim;

    public float rangoAlerta;

    public LayerMask capaDelJugador;

    public bool estarAlerta;

    public Transform jugador;

    public float speed;

    public bool atac, perseguir, dead;

    public bool comprovar;

    public int vida;

    public ParticleSystem sang;
    public Rigidbody rb;

    


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
    }

    // Update is called once per frame
    void Update()
    {

        if (comprovar)
        {
            if (atac)
            {

            }
            else
            {
                anim.SetBool("atack", false);
            }
            estarAlerta = Physics.CheckSphere(transform.position, rangoAlerta, capaDelJugador);
            if (estarAlerta == true)
            {
                //transform.LookAt(jugador);


                transform.LookAt(new Vector3(jugador.position.x, transform.position.y, jugador.position.z));

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(jugador.position.x, transform.position.y, jugador.position.z), speed * Time.deltaTime);
                anim.SetBool("perseguir", true);
            }
            else
            {
                anim.SetBool("perseguir", false);
            }
        }

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
        
    }

    public IEnumerator RebreMal()
    {
        comprovar = false;
        anim.Play("hurt", -1, 0f);
        Debug.LogError("AAAAAA");
        yield return new WaitForSeconds(2.5f);
        Debug.LogError("BBBBBBBBBBB");
        if (!dead)
        {
            Debug.LogError("CCCCCCCCCC");
            comprovar = true;
        }
        
    }


}
