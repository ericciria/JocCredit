using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public Item info;
    private Inventory player;
    private PlayerController playerC;

    private void Update()
    {
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
            Destroy(gameObject);
        }
    }
}
