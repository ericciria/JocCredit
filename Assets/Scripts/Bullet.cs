using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    private bool impacte;
    private MeshRenderer mr;
    public int extraAttack;

    public Transform camTransform1, camTransform3;
    public movment player;
    

    void Awake()
    {
        Destroy(gameObject, life);
        mr = GetComponent<MeshRenderer>();

        player = GameObject.Find("/Player").GetComponent<movment>();
        camTransform3 = player.cam3.transform;
        camTransform1 = player.cam1.transform;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (!impacte)
            {

                impacte = true;
                if (other.gameObject.tag.Equals("Enemy"))
                {
                    mr.enabled = false;
                    enemigo enemy = other.GetComponentInParent<enemigo>();
                    if (enemy != null)
                    {
                        if (!enemy.dead)
                        {
                            enemy.sang.transform.LookAt(player.cam1.transform);
                            enemy.sang.transform.position=transform.position;

                            enemy.sang.Play();
                            enemy.vida -= (1+extraAttack);
                            enemy.sliderhealth.fillAmount = (float)enemy.vida / enemy.maxVida;
                            if (enemy.vida <= enemy.vida/2 && enemy.vida>0 && !enemy.isHurt)
                            {
                                StartCoroutine(RebreMal(enemy));
                            }
                            else if (enemy.vida <= 0 && !enemy.dead)
                            {
                                enemy.comprovar = false;
                                enemy.dead = true;
                                enemy.so.Stop();
                                enemy.anim.Play("dead", -1, 0f);
                                other.GetComponent<BoxCollider>().enabled = false;
                            }
                        }
                    }
                    else
                    {
                        enemyshoot enemyShoot = other.GetComponent<enemyshoot>();
                        Debug.LogWarning(enemyShoot);
                        if (!enemyShoot.dead)
                        {
                            enemyShoot.sang.transform.LookAt(player.cam1.transform);
                            enemyShoot.sang.transform.position = transform.position;

                            enemyShoot.sang.Play();
                            enemyShoot.vida -= (1 + extraAttack);
                            enemyShoot.sliderhealth.fillAmount = (float)enemyShoot.vida / enemyShoot.maxVida;
                            if (enemyShoot.vida <= 0 && !enemyShoot.dead)
                            {
                                enemyShoot.comprovar = false;
                                enemyShoot.dead = true;
                                enemyShoot.anim.Play("dead", -1, 0f);
                                other.GetComponent<BoxCollider>().enabled = false;
                            }
                        }
                    }
                    

                }
                else if (!other.gameObject.tag.Equals("Player") && !other.gameObject.tag.Equals("Bullet"))
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (other.gameObject.tag.Equals("EnemyPre"))
            {
                other.GetComponentInChildren<MeshCollisionHelper>().UpdateCollisionMesh();
            }
        }
    }

    public IEnumerator RebreMal(enemigo enemy)
    {
        enemy.comprovar = false;
        enemy.isHurt = true;
        enemy.anim.Play("hurt", -1, 0f);
        yield return new WaitForSeconds(2.5f);
        if (!enemy.dead)
        {
            enemy.comprovar = true;
        }
        Destroy(gameObject);
    }
}
