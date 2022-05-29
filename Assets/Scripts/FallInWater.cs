using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallInWater : MonoBehaviour
{
    PlayerController player;
    private bool antibug;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            
            if (!antibug)
            {
                player = other.GetComponent<PlayerController>();
                player.activarSang(1);
                player.transform.position = player.lastPosition;
                StartCoroutine(ReturnAntiBug());
            }
            else
            {
                player = other.GetComponent<PlayerController>();
                player.transform.position = player.lastPosition2;
            }
        }
    }

    IEnumerator ReturnAntiBug()
    {
        antibug = true;
        yield return new WaitForSeconds(1);
        antibug = false;
    }
}
