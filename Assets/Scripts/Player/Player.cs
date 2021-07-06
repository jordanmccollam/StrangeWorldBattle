using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public GameObject hand;
    public bool pickedUp = false;

    [Header("Stats")]
    public float speed = 3f;
    public float jumpForce;

    private Rigidbody2D rb;
    private Vector2 moveAmount;
    private bool facingRight = true;
    private bool isGrounded;
    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine) {
            HandleMove();
            HandleJump();
            HandleAction();
        }
    }

    private void HandleMove() {
        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        if (input > 0 && !facingRight) {
            Flip();
        } else if (input < 0 && facingRight) {
            Flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // TODO: Add move sound?
        // TODO: Add dust
    }

    private void Flip() {
        if (facingRight) {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180f, transform.localRotation.z);
        } else {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
        }
        
        facingRight = !facingRight;
    }

    private void HandleJump() {
        if ((Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("space")) && isGrounded) {
            rb.velocity = Vector2.up * jumpForce;
            // TODO: Add jump animation
            // TODO: Add jump sound
        }
    }

    private void HandleAction() {
        if (Input.GetKeyDown("joystick button 0")) {
            if (pickedUp) {
                // TODO: code shooting action
                pickedUp = false;
                GameObject orb = GameObject.FindGameObjectWithTag("Orb");
                orb.GetComponent<Orb>().Activate();

                // TODO: Shooting sounds / animation / anything game feel
            } else {
                // TODO: code some kind of misfire game feel (player isn't holding an item)
            }
        }
    }
}
