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

    Vector3 cameraInitialPos;
    private void Awake()
    {
        _camera = this;
    }

    public void SetNewTarget(GameObject newTarget)
    {
        target = newTarget.transform;
        cameraInitialPos = target.transform.position;
    }
    private void LateUpdate()
    {
        if (target != null)
        {
            cameraInitialPos.x = target.transform.position.x;
            playerLastPosition = cameraInitialPos + offset;
            transform.position = playerLastPosition;
        }
    }

}
