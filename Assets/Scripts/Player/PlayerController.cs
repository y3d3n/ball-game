using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    private bool canMove=false;

    public bool ForceFullyStopPlayer = false;

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
        maxClampRight = currentPosX + 0.9f;
        minClampLeft = currentPosX - 0.6f;
        playerNewVelocity = Vector3.zero;
       
    }
    Vector3 clampedPos;
    private void FixedUpdate()
    {
        if (canMove && !ForceFullyStopPlayer)
        {


            //Deactive UI
            UIManager.Instance.IsPlaying();

            rb.MovePosition(transform.position - transform.right * forwardMoveSpeed*Time.deltaTime);
            playerNewVelocity.x = rb.velocity.x;
            rb.velocity = playerNewVelocity;
           

        }
            playerNewVelocity.z = 0;

    }



    void OnInputReceived(InputValues values)
    {
        canMove = true;
        if (values.isSwiping)
        {
            float swipeX = 0;
            swipeX = Mathf.Clamp(values.swipeX, -1f, 1f);
            playerNewVelocity.z = swipeX * horizontalMoveSpeed * Time.fixedDeltaTime;
        }

    }

    public void ChangeMoveState()
    {
       ForceFullyStopPlayer = !ForceFullyStopPlayer;
        rb.velocity = Vector3.zero;
    }


    

}
