using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public Transform platform;
    public GameObject[] obj;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetSpawnPoint(platform));
    }

    public IEnumerator GetSpawnPoint(Transform p)
    {
        foreach (Transform g in p.transform.GetComponentsInChildren<Transform>())
        {
            if (p != g)
            {
                GameObject c = Instantiate(obj[0], g.position, Quaternion.identity) as GameObject;

                //parrent it to its spawn point
                c.transform.parent = g.transform;
            }
        }
        yield return true;
    }
}
