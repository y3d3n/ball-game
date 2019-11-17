using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool canMove=false;

    private Rigidbody rb;
    public float speed;

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, screenPoint.y, screenPoint.z));
        canMove = true;
    }
    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, screenPoint.y, screenPoint.z);

        Vector3 curPoint = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPoint;
    }

    private void OnMouseUp()
    {
        canMove = false;
    }

    private void Update()
    {
        if (canMove)
        {
           // player movement code
        }
    }

}
