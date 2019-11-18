using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ChangeMoveState();
            Debug.Log("FUCK Player is here");
        }
    }
}
