using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    [SerializeField] private float movSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 movDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movDirection = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rb.velocity = movDirection * movSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Janky-ass turn logic using waypoints
        string turn_type = other.transform.parent.name;
        Debug.Log(turn_type);
        switch (turn_type)
        {
            case "LeftTIntersection":
                movDirection = randomizeDirection(new List<Vector2> { Vector2.up, Vector2.down, Vector2.left});
                break;
            case "RightTIntersection":
                movDirection = randomizeDirection(new List<Vector2> { Vector2.up, Vector2.down, Vector2.right});
                break;
            default: break;
        }
    }


    // //Simple movement script
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     Debug.Log("COP COLLIDED with " + other.gameObject.name);
    //     if (other.gameObject.name == "Tilemap_Layer_Buildings_Col")
    //     {
    //         movDirection = randomizeDirection(movDirection, new List<Vector2> { Vector2.up, Vector2.down, Vector2.right, Vector2.left });
    //     }
    // }

    private Vector2 randomizeDirection(List<Vector2> available_directions) =>
     available_directions[Random.Range(0, available_directions.Count)];

}
