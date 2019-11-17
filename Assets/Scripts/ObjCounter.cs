using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCounter : MonoBehaviour
{
    private int collection=0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ball"))
        {
            collection++;
            Destroy(other.gameObject);
        }
    }

    public int GetCount()
    {
        return collection;
    }
}
