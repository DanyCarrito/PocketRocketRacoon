using NUnit.Framework.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RacMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputAction clickAction;
    private InputAction dashAction;
    private bool isDashing;
    private bool isFirstTime = true;
    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    [SerializeField] float speedDash;
    [SerializeField] float limitMax;
    [SerializeField] float limitMin;
    [SerializeField] float timerDash;
    [SerializeField] float maxVelocity = 0f;


    void Start()
    {
        clickAction = InputSystem.actions.FindAction("pushito");
        dashAction = InputSystem.actions.FindAction("DashPush");
        rb = GetComponent<Rigidbody2D>();
        rb.AddForceX(speed);
    }

    void Update()
    {
        if(isFirstTime)
        {
            maxVelocity = rb.linearVelocityX;
            isFirstTime = false;
            Debug.Log(maxVelocity);
            return;
        }

        if(!isDashing)
        {
            clickAction.performed += context => { rb.AddForceY(jumpForce); };
            clickAction.canceled += context => { rb.AddForceY(-20.0f); };
            if (transform.position.y < limitMin) { rb.AddForceY(20.0f); }
            if (transform.position.y > limitMax) { rb.AddForceY(-20.0f); }
            if (rb.linearVelocityY > 12.0f) { rb.linearVelocityY -= 3.0f; }

            dashAction.performed += context =>
            {
                Dash();
            };
        }


        if(isDashing)
        {
            timerDash += Time.deltaTime;
            if(timerDash > 0.5f)
            {
                isDashing = false;
                timerDash = 0.0f;
                rb.linearVelocityX = maxVelocity;
                //rb.AddForceX(-speedDash);
                rb.gravityScale = 1.0f;
            }
        }
    }

    private void Dash()
    {
        rb.AddForceX(speedDash);
        rb.linearVelocityY = 0.0f;
        rb.linearVelocityX = maxVelocity;
        isDashing = true;
        rb.gravityScale = 0.0f;
        timerDash = 0.0f;
    }

}
