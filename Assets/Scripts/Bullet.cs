using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        if (!impacte)
        {
            mr.enabled = false;
            impacte = true;
            if (other.gameObject.tag.Equals("enemy"))
            {
                
                enemigo enemy = other.GetComponent<enemigo>();
                if (!enemy.dead)
                {
                    enemy.sang.Play();
                    enemy.vida--;
                    enemy.sliderhealth.fillAmount= (float)enemy.vida / enemy.maxVida;
                    if (enemy.vida == 5)
                    {
                        StartCoroutine(RebreMal(enemy));
                    }
                    else if (enemy.vida <= 0 && !enemy.dead)
                    {
                        enemy.comprovar = false;
                        enemy.dead = true;
                        enemy.anim.Play("dead", -1, 0f);
                        other.GetComponent<BoxCollider>().enabled = false;
                    }
                }

            }
            else if (!other.gameObject.tag.Equals("Player") && !other.gameObject.tag.Equals("Bullet"))
            {
                Destroy(gameObject);
            }
        }
    }

    public IEnumerator RebreMal(enemigo enemy)
    {
        enemy.comprovar = false;
        enemy.anim.Play("hurt", -1, 0f);
        yield return new WaitForSeconds(2.5f);
        if (!enemy.dead)
        {
            enemy.comprovar = true;
        }
        Destroy(gameObject);
    }
}
