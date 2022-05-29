using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour, IsSaveable
{
    PlayerController player;
    public int maxHealth, attack;
    public float shootSpeed, shootRate, speed;
    public Vector3 tamanyBala;
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        shootSpeed = 0;
        shootRate = 0;
        speed = 0;
        attack = 0;
        maxHealth = 0;
        tamanyBala = new Vector3(0,0,0);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("play"))
        {
            EmptyInventory();
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

    public void IncreaseStats(float shootSpeed1, float shootRate1, float speed1, int attack1, int maxHealth1, Vector3 tamanyBala1)
    {
        shootSpeed += shootSpeed1;
        shootRate += shootRate1;
        speed += speed1;
        attack += attack1;
        maxHealth += maxHealth1;
        tamanyBala += tamanyBala1;

        UpdatePlayer(shootSpeed, shootRate, speed, attack, maxHealth);

        if (player.shootRate < 0.4)
        {
            player.shootRate = 0.4f;
        }
        else if (player.shootRate > 10)
        {
            player.shootRate = 10;
        }
        if (player.speed < 0.4)
        {
            player.speed = 0.4f;
        }
        else if (player.speed > 10)
        {
            player.speed = 10;
        }
        if (player.shootSpeed < player.speed *2)
        {
            player.shootSpeed = player.speed *2;
        }
        if (player.shootSpeed < 4)
        {
            player.shootSpeed = 4;
        }
        else if (player.shootSpeed > 50)
        {
            player.shootSpeed = 50;
        }
    }

    public void UpdatePlayer(float shootSpeed, float shootRate, float speed, int attack, int maxHealth)
    {
        player.shootSpeed = player.baseShootSpeed + shootSpeed;
        player.shootRate = player.baseShootRate + shootRate;
        player.speed = player.baseSpeed + speed;
        player.attack = player.baseAttack + attack;
        player.maxHealth = player.baseMaxHealth + maxHealth;
        player.gun.tamanyBala = new Vector3(0.3f,0.3f,0.3f) + tamanyBala;
    }

    public void EmptyInventory()
    {
        shootSpeed = 0;
        shootRate = 0;
        speed = 0;
        attack = 0;
        maxHealth = 0;
        tamanyBala = new Vector3(0, 0, 0);
    }

    public object CaptureState()
    {
        throw new System.NotImplementedException();
    }

    public void RestoreState(object data)
    {
        throw new System.NotImplementedException();
    }
}
