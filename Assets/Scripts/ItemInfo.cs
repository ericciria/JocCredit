using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{


    AudioSource tuto;
    public Item info;
    private Inventory player;
    private PlayerController playerC;


    private void Start()
    {
        //tuto.Pause();
    }
    private void Update()
    {
        tuto = GetComponent<AudioSource>();
        float y = Mathf.PingPong(Time.time, 4)/4;
        transform.position = new Vector3(transform.position.x, y+1, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject.GetComponent<Inventory>();

            
            if (info.Effect.Equals("Blue"))
            {
                playerC = player.gameObject.GetComponent<PlayerController>();
                playerC.gun.color = Color.blue;
            }
            player.UpdateStats(info.ShootSpeed, info.FireRate, info.Speed, info.Damage, info.Health, info.TamanyBala);


            tuto.Play();
            StartCoroutine(Destruir());

            //Destroy(gameObject);
        }
    }
    IEnumerator Destruir()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
