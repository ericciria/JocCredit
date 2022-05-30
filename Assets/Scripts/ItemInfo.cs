using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour, IsSaveable
{


    AudioSource tuto;
    public Item info;
    private Inventory player;
    private PlayerController playerC;
    private bool hasBeenPicked, hasBeenDestroyed;

    private void Start()
    {
        tuto = GetComponentInChildren<AudioSource>();
    }


    private void Update()
    {
        if (hasBeenDestroyed)
        {
            return;
        }
        
        float y = Mathf.PingPong(Time.time, 4)/4;
        transform.position = new Vector3(transform.position.x, y+1, transform.position.z);

        if (hasBeenPicked)
        {
            StartCoroutine(Destruir());
            hasBeenDestroyed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenDestroyed)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject.GetComponent<Inventory>();

            
            if (info.Effect.Equals("Blue"))
            {
                playerC = player.gameObject.GetComponent<PlayerController>();
                playerC.gun.color = Color.blue;
            }
            player.IncreaseStats(info.ShootSpeed, info.FireRate, info.Speed, info.Damage, info.Health, info.TamanyBala);


            tuto.Play();
            hasBeenPicked = true;
        }
    }
    IEnumerator Destruir()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject child = transform.GetChild(0).gameObject;
        child.SetActive(false);
    }

    [System.Serializable]
    struct ItemSave
    {
        public bool hasBeenPicked;
    }

    public object CaptureState()
    {
        ItemSave data;
        data.hasBeenPicked = hasBeenPicked;

        return data;
    }

    public void RestoreState(object data)
    {
        ItemSave dataLoaded = (ItemSave)data;
        hasBeenPicked = dataLoaded.hasBeenPicked;
    }
}
