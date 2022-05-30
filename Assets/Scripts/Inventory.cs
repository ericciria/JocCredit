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

    private void Awake()
    {
        
    }
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

        if (maxHealth1 > 0)
        {
            player.health += maxHealth1;
            player.sliderhealth.fillAmount = (float)player.health / player.maxHealth;
        }

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

    [System.Serializable]
    struct InventoryData
    {
        public int maxHealth, attack;
        public float shootSpeed, shootRate, speed;
        public float[] tamanyBala;
        public float[] playerPos;
    }

    public object CaptureState()
    {
        InventoryData data;
        data.maxHealth = maxHealth;
        data.attack = attack;
        data.shootSpeed = shootSpeed;
        data.shootRate = shootRate;
        data.speed = speed;
        data.tamanyBala = new float[3];
        data.tamanyBala[0] = tamanyBala.x;
        data.tamanyBala[1] = tamanyBala.y;
        data.tamanyBala[2] = tamanyBala.z;
        data.playerPos = new float[3];
        data.playerPos[0] = player.transform.position.x;
        data.playerPos[1] = player.transform.position.y;
        data.playerPos[2] = player.transform.position.z;

        return data;
    }

    public void RestoreState(object data)
    {
        player = gameObject.GetComponent<PlayerController>();

        InventoryData dataLoaded = (InventoryData)data;
        maxHealth = dataLoaded.maxHealth;
        attack = dataLoaded.attack;
        shootSpeed = dataLoaded.shootSpeed;
        shootRate = dataLoaded.shootRate;
        speed = dataLoaded.speed;
        player.transform.position = new Vector3(dataLoaded.playerPos[0], dataLoaded.playerPos[1], dataLoaded.playerPos[2]);
        UpdatePlayer(0, 0, 0, 0, 0);
    }
}
