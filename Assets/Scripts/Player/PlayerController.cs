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

    Vector3 playerNewVelocity;

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
        playerNewVelocity = Vector3.zero;
       
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            Debug.Log("should move");
            //rb.AddForce(-transform.right * forwardMoveSpeed * rb.mass*Time.deltaTime);
            playerNewVelocity.x = -forwardMoveSpeed * Time.deltaTime;
            playerNewVelocity.y = rb.velocity.y;
            

      
            rb.velocity = playerNewVelocity;
            playerNewVelocity.z = 0;
          
        }
    }


    void OnInputReceived(InputValues values)
    {
        canMove = true;


        // Vector3 newMovePos = transform.position;
        //  newMovePos.z += values.swipeX * horizontalMoveSpeed * Time.fixedDeltaTime;
        //  newMovePos.z = Mathf.Clamp(newMovePos.z, minClampLeft, maxClampRight);
        // transform.position = newMovePos;

        if (values.isSwiping)
        {
            float swipeX = 0;
            swipeX = Mathf.Clamp(values.swipeX, -1f, 1f);
            playerNewVelocity.z = swipeX * horizontalMoveSpeed * Time.fixedDeltaTime;
     

        }

        //   float velocityZ = Mathf.Clamp(playerNewVelocity.z, -horizontal - MoveSpeed, horizontalMoveSpeed);
        // playerNewVelocity.z = velocityZ;
       
        

        




    }

    

}
