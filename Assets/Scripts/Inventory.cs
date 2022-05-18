using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    PlayerController player;
    public int maxHealth;
    public float shootSpeed, shootRate, speed, attack;
    public Vector3 tamanyBala;
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        shootSpeed = 0;
        shootRate = 0;
        speed = 0;
        attack = 0;
        tamanyBala = new Vector3(0,0,0);
    }


    public void HealPlayer(int health)
    {
        player.health += health;
        if (player.health > player.maxHealth)
        {
            player.health = player.maxHealth;
        }
    }

    public void UpdateStats(float shootSpeed1, float shootRate1, float speed1, float attack1, int maxHealth1, Vector3 tamanyBala1)
    {
        shootSpeed += shootSpeed1;
        shootRate += shootRate1;
        speed += speed1;
        attack += attack1;
        maxHealth += maxHealth1;
        tamanyBala += tamanyBala1;
        
        if (shootRate < 0.4)
        {
            shootRate = 0.4f;
        }
        else if (shootRate > 10)
        {
            shootRate = 10;
        }
        if (speed < 0.4)
        {
            speed = 0.4f;
        }
        else if (speed > 10)
        {
            speed = 10;
        }
        if (shootSpeed < speed*2)
        {
            shootSpeed = speed*2;
        }
        if (shootSpeed < 4)
        {
            shootSpeed = 4;
        }
        else if (shootSpeed > 50)
        {
            shootSpeed = 50;
        }
        UpdatePlayer(shootSpeed, shootRate, speed, attack, maxHealth);
    }

    public void UpdatePlayer(float shootSpeed, float shootRate, float speed, float attack, int maxHealth)
    {
        player.shootSpeed = player.baseShootSpeed + shootSpeed;
        player.shootRate = player.baseShootRate + shootRate;
        player.speed = player.baseSpeed + speed;
        player.attack = player.baseAttack + attack;
        player.maxHealth = player.baseMaxHealth + maxHealth;
        player.gun.tamanyBala = new Vector3(0.3f,0.3f,0.3f) + tamanyBala;
    }
}
