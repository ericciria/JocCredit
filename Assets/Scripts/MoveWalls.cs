using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWalls : MonoBehaviour
{
    public bool ascend;

    public Transform[] murs;
    public MeshRenderer illa;
    float illaX;

    private void Start()
    {
        murs = GetComponentsInChildren<Transform>();
        illa = GetComponentInParent<MeshRenderer>();
        illaX = illa.bounds.size.x;
        murs[0] = murs[murs.Length - 1];
        System.Array.Resize(ref murs, murs.Length - 1);

        Debug.LogWarning(murs.Length);
        
        if (murs.Length == 8)
        {
            ColocarMurs8();
        }
        
    }

    


    // Update is called once per frame
    void Update()
    {
        if (ascend)
        {
            Ascend();
        }
        else
        {
            Descend();
        }
        /*if (Input.GetKeyDown(KeyCode.T))
        {
            ColocarMurs8();
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
        /*else
        {
            gameObject.SetActive(false);
        }*/
    }

    private void ColocarMurs8()
    {
        int i = 0;
        foreach (Transform mur in murs)
        {
            Debug.LogWarning(illa.transform.localScale.x );
            mur.localScale = new Vector3(1/illa.transform.localScale.x, 1 / illa.transform.localScale.y, 1 / illa.transform.localScale.z);
            if (i==0)
            {
                mur.transform.position = new Vector3(illa.transform.position.x, illa.transform.position.y, illa.transform.position.z);
                mur.localScale = new Vector3(mur.localScale.x*illa.transform.localScale.x/12,
                    mur.localScale.y * illa.transform.localScale.y,
                    mur.localScale.z * illa.transform.localScale.z/6);
            }
            else if (i == 1)
            {
                mur.transform.position = new Vector3(illa.transform.position.x - illa.bounds.size.x / 3.5f, illa.transform.position.y, illa.transform.position.z + illa.bounds.size.z / 12f);
                mur.localScale = new Vector3(mur.localScale.x * illa.transform.localScale.x / 12,
                    mur.localScale.y * illa.transform.localScale.y/1.2f,
                    mur.localScale.z * illa.transform.localScale.z / 6);
                mur.rotation = new Quaternion(-0.2f,0.6f,0.6f,0.2f);
            }
            // Izquierda
            else if (i == 2)
            {
                mur.transform.position = new Vector3(illa.transform.position.x - illa.bounds.size.x / 2.5f, illa.transform.position.y, illa.transform.position.z + illa.bounds.size.z / 2);
                mur.localScale = new Vector3(0.1f,
                    mur.localScale.y * illa.transform.localScale.y*2,
                    mur.localScale.z * illa.transform.localScale.z / 6);
                Debug.LogWarning(mur.rotation);
                mur.rotation = new Quaternion(0f, 0.6f, 0.6f, 0f);
            }
            // Arriba izquierda
            else if (i == 3)
            {
                mur.transform.position = new Vector3(illa.transform.position.x - illa.bounds.size.x / 4f, illa.transform.position.y, illa.transform.position.z + illa.bounds.size.z * 0.8f);
                mur.localScale = new Vector3(mur.localScale.x * illa.transform.localScale.x / 12,
                    mur.localScale.y * illa.transform.localScale.y/1.2f,
                    mur.localScale.z * illa.transform.localScale.z / 6);
                mur.rotation = new Quaternion(-0.6f, 0.25f, 0.25f, 0.6f);
            }
            // Arriba
            else if (i == 4)
            {
                mur.transform.position = new Vector3(illa.transform.position.x, illa.transform.position.y, illa.transform.position.z + illa.bounds.size.z*0.8f);
                mur.localScale = new Vector3(mur.localScale.x * illa.transform.localScale.x / 12,
                    mur.localScale.y * illa.transform.localScale.y,
                    mur.localScale.z * illa.transform.localScale.z / 6);
            }
            // Arriba derecha
            else if (i == 5)
            {
                mur.transform.position = new Vector3(illa.transform.position.x + illa.bounds.size.x / 3.5f, illa.transform.position.y, illa.transform.position.z + illa.bounds.size.z *0.8f);
                mur.localScale = new Vector3(mur.localScale.x * illa.transform.localScale.x / 12,
                    mur.localScale.y * illa.transform.localScale.y/1.2f,
                    mur.localScale.z * illa.transform.localScale.z / 6);
                mur.rotation = new Quaternion(-0.2f, 0.6f, 0.6f, 0.2f);
            }
            // Derecha
            else if (i == 6)
            {
                mur.transform.position = new Vector3(illa.transform.position.x + illa.bounds.size.x/2.5f, illa.transform.position.y, illa.transform.position.z + illa.bounds.size.z/2);
                mur.localScale = new Vector3(0.1f,
                    mur.localScale.y * illa.transform.localScale.y*2,
                    mur.localScale.z * illa.transform.localScale.z / 6);
                mur.rotation = new Quaternion(0f, 0.6f, 0.6f, 0f);
            }
            else
            {
                mur.transform.position = new Vector3(illa.transform.position.x + illa.bounds.size.x / 3.5f, illa.transform.position.y, illa.transform.position.z + illa.bounds.size.z / 18f);
                mur.localScale = new Vector3(mur.localScale.x * illa.transform.localScale.x / 12,
                    mur.localScale.y * illa.transform.localScale.y/1.2f,
                    mur.localScale.z * illa.transform.localScale.z / 6);
                mur.rotation = new Quaternion(-0.6f, 0.25f, 0.25f, 0.6f);
            }
            i++;

        }
        gameObject.SetActive(false);
    }
}
