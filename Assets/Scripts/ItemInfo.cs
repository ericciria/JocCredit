using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public Item info;
    private Inventory player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject.GetComponent<Inventory>();

            player.UpdateStats(info.ShootSpeed, info.FireRate, info.Speed, info.Damage, info.Health);

            Destroy(gameObject);
        }
    }
}
