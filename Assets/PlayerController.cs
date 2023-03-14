using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 movDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame - good for gathering information, but never for updating sprites
    void Update()
    {
        movDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        System.Console.WriteLine(movDirection);

    }

    void FixedUpdate()
    {
        rb.velocity = movDirection * movSpeed;
    }
}
