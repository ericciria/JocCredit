using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallInWater : MonoBehaviour
{
    PlayerController player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            player = other.GetComponent<PlayerController>();
            player.transform.position = player.lastPosition;
        }
    }
}
