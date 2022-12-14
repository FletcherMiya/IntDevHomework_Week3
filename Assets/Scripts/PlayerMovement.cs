using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontalMove;
    public float force;

    bool grounded = false;
    public float castDist = 0.2f;

    public float jumpHeight;
    public float speedLimit;

    bool jump = false;
    bool doublejump = true;

    Vector3 startpos = new Vector3(-11.2f, 0.81f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump"))
        {
            if(doublejump || grounded)
            {
                jump = true;
            }
        }
        
    }

    private void FixedUpdate()
    {
        HorizontalMove(horizontalMove);

        if (jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight));
            jump = false;
            if (!grounded && doublejump)
            {
                doublejump = false;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        if(hit.collider != null)
        {
            grounded = true;
            doublejump = true;
        }
        else
        {
            grounded = false;
        }

        if(transform.position.y < -10)
        {
            transform.position = startpos;
        }

    }

    void HorizontalMove(float toMove)
    {
        float moveX = toMove * Time.fixedDeltaTime * force;
        rb.velocity = new Vector3(moveX, rb.velocity.y);
        /*if(Mathf.Abs(rb.velocity.x) < speedLimit)
        {
            rb.AddForce(transform.right * moveX, ForceMode2D.Impulse);
        }*/
    }
}
