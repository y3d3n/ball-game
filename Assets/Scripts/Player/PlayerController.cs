using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    private bool canMove=false;

    private Rigidbody rb;
    public float horizontalMoveSpeed =5f;
    public float forwardMoveSpeed = 10f;


    private float maxClampRight;
    private float minClampLeft;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        EventSystem.Instance.RegisterEvent<InputValues>(OnInputReceived);
       
        float currentPosX = transform.position.z;
        maxClampRight = currentPosX + 1.1f;
        minClampLeft = currentPosX - 0.8f;
       
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            Debug.Log("should move");
            //rb.AddForce(-transform.right * forwardMoveSpeed * rb.mass*Time.deltaTime);
            rb.velocity = -transform.right * forwardMoveSpeed * Time.deltaTime;
        }
    }


    void OnInputReceived(InputValues values)
    {
        canMove = true;
        if (values.isSwiping)
        {
            
            Vector3 newMovePos = transform.position;
            newMovePos.z += values.swipeX * horizontalMoveSpeed * Time.fixedDeltaTime;
            newMovePos.z = Mathf.Clamp(newMovePos.z, minClampLeft, maxClampRight);
            transform.position = newMovePos;

        }
       
    }

    

}
