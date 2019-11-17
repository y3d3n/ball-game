using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public static CameraBehaviour _camera;
    private Transform target;
    public float distanceFromTarget = 6.0f;

    public Vector3 offset = new Vector3(2,0,4);
    Vector3 playerLastPosition;
    private void Awake()
    {
        _camera = this;
    }

    public void SetNewTarget(GameObject newTarget)
    {
        target = newTarget.transform;
    }
    private void LateUpdate()
    {
        if (target != null)
        {
            playerLastPosition = target.position;
            playerLastPosition.x = playerLastPosition.x + distanceFromTarget;
            playerLastPosition.y = target.position.y + distanceFromTarget / 2;

            transform.position = playerLastPosition;
        }
    }

}
