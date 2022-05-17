using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    PlayerController player;
    public int maxHealth;
    public float shootSpeed, shootRate, speed, attack;
    ItemInfo itemInfo;

    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        shootSpeed = 0;
        shootRate = 0;
        speed = 0;
        attack = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            itemInfo = other.gameObject.GetComponent<ItemInfo>();

            shootSpeed += itemInfo.info.ShootSpeed;
        }
    }

    public void HealPlayer(int health)
    {
        player.health += health;
        if (player.health > player.maxHealth)
        {
            player.health = player.maxHealth;
        }
    }

    public void UpdateStats()
    {
        player.shootSpeed = player.baseShootSpeed + shootSpeed;
        player.shootRate = player.baseShootRate + shootRate;
        player.speed = player.baseSpeed + speed;
        player.attack = player.baseAttack + attack;
        player.maxHealth = player.baseMaxHealth + maxHealth;
    }
}
