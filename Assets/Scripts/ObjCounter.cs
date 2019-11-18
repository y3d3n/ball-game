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
            //ScoreManager.Instance.CheckCount();

            StartCoroutine(waitForDeactive(other.gameObject));

            //Destroy(other.gameObject);
        }
    }

    public int GetCount()
    {
        return collection;
    }
    
    IEnumerator waitForDeactive( GameObject other)
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(other.gameObject);
    }
}
