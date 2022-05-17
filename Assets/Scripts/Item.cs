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
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;

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
    public float Damage
    {
        get
        {
            return damage;
        }
    }


}
