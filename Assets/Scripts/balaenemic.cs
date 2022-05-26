using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaenemic : MonoBehaviour
{
    public float life = 3;
    private bool impacte;
    private MeshRenderer mr;

    void Awake()
    {
        Destroy(gameObject, life);
        mr = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
                if (other.gameObject.tag.Equals("Player"))
                {
                    PlayerController player = other.GetComponent<PlayerController>();
                    player.activarSang(1);
                if (player.health <= 0)
                {
                    player.isDead = true;
                    player.anim.Play("dead", -1, 0f);
                }
                Destroy(gameObject);


                }
                else if (!other.gameObject.tag.Equals("Enemy") && !other.gameObject.tag.Equals("Bullet"))
                {
                    Destroy(gameObject);
                }

        }
    }
}
