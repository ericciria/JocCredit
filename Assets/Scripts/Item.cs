using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item Data", order = 51)]

public class Item : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private string effect;
    [SerializeField] private Sprite icon;
    [SerializeField] private float speed;
    [SerializeField] private float shootSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private int damage;
    [SerializeField] private int health;
    [SerializeField] private Vector3 tamanyBala;

    public string Name
    {
        get
        {
            return name;
        }
    }
    public string Description
    {
        get
        {
            return description;
        }
    }
    public string Effect
    {
        get
        {
            return effect;
        }
    }
    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }
    public float ShootSpeed
    {
        get
        {
            return shootSpeed;
        }
    }
    public float Speed
    {
        get
        {
            return speed;
        }
    }
    public float FireRate
    {
        get
        {
            return fireRate;
        }
    }
    public int Damage
    {
        get
        {
            return damage;
        }
    }
    public int Health
    {
        get
        {
            return health;
        }
    }
    public Vector3 TamanyBala
    {
        get
        {
            return tamanyBala;
        }
    }


}
