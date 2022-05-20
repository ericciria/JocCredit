using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWalls : MonoBehaviour
{
    public bool ascend;

    public Transform[] murs;
    public MeshRenderer illa;

    private void Start()
    {
        murs = GetComponentsInChildren<Transform>();
        illa = GetComponentInParent<MeshRenderer>();
        Debug.LogWarning(murs);
        Debug.LogWarning(illa);
    }


    // Update is called once per frame
    void Update()
    {
        /*if (ascend)
        {
            Ascend();
        }
        else
        {
            Descend();
        }*/
    }
    void Ascend()
    {
        if (transform.position.y < 2.5)
        {
            transform.position += new Vector3(0, Time.deltaTime*2, 0);
        }
    }
    void Descend()
    {
        if (transform.position.y > -2.5)
        {
            transform.position += new Vector3(0, -Time.deltaTime*4, 0);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
