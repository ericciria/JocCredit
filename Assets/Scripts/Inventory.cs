using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    PlayerController player;
    public int maxHealth;
    public float shootSpeed, shootRate, speed, attack;

    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            other.gameObject.GetComponent<ItemInfo>(){

            }
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
