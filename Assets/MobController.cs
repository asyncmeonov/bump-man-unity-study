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
        Debug.Log(Vector2.up + " - " + Vector2.down + " - " + Vector2.right + " - " + Vector2.left);
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

    private void OnTriggerStay2D(Collider2D other)
    {
        //Janky-ass turn logic using waypoints
        //TODO - make it approximate
        string turn_type = other.transform.parent.name;
        if (other.transform.position == gameObject.transform.position) //important, these are Vector3!
        {
            switch (turn_type)
            {
                case "LeftTIntersection":
                    movDirection = randomizeDirection(new List<Vector2> { Vector2.up, Vector2.down, Vector2.left });
                    break;
                case "RightTIntersection":
                    movDirection = randomizeDirection(new List<Vector2> { Vector2.up, Vector2.down, Vector2.right });
                    break;
                case "UpTIntersection":
                    movDirection = randomizeDirection(new List<Vector2> { Vector2.up, Vector2.left, Vector2.right });
                    break;
                case "DownTIntersection":
                    movDirection = randomizeDirection(new List<Vector2> { Vector2.left, Vector2.down, Vector2.right });
                    break;
                case "DownLeftTurn":
                    movDirection = randomizeDirection(new List<Vector2> { Vector2.down, Vector2.left });
                    break;
                case "DownRightTurn":
                    movDirection = randomizeDirection(new List<Vector2> { Vector2.down, Vector2.right });
                    break;
                case "UpLeftTurn":
                    movDirection = randomizeDirection(new List<Vector2> { Vector2.up, Vector2.left });
                    break;
                case "UpRightTurn":
                    movDirection = randomizeDirection(new List<Vector2> { Vector2.up, Vector2.right });
                    break;
                case "CrossRoads":
                    movDirection = randomizeDirection(new List<Vector2> { Vector2.down, Vector2.right, Vector2.left, Vector2.up });
                    break;
                default: break; //do nothing if unspecified collision is encountered
            }
        }
    }

    private Vector2 randomizeDirection(List<Vector2> availableDirections)
    {
        //get current
        Vector2 cameFrom = new Vector2(rb.velocity.normalized.x * -1, rb.velocity.normalized.y * -1);
        availableDirections.Remove(cameFrom);
        return availableDirections[Random.Range(0, availableDirections.Count)];
    }

    private void printDebug(Vector2 test)
    {
        if(test == Vector2.down) Debug.Log("CAME FROM DOWN");
        if(test == Vector2.up) Debug.Log("CAME FROM UP");
        if(test == Vector2.right) Debug.Log("CAME FROM RIGHT");
        if(test == Vector2.left) Debug.Log("CAME FROM LEFT");
    }
}
