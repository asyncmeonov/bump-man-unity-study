using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 movDirection;

    private bool isTweaking;
    void Start()
    {
        isTweaking = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame - good for gathering information, but never for updating sprites
    void Update()
    {
        movDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movDirection.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movDirection.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }

    }

    void FixedUpdate()
    {
        rb.velocity = movDirection * movSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.StartsWith("Mob"))
        {
            if (isTweaking)
            {
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetIstweaking(bool value)
    {
        isTweaking = value;
    }
    public bool GetIsTweaking() => isTweaking;
}
