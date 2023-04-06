using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{

    
    [SerializeField] private float movSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 movDirection;

    [Header("SFXs")]
    [SerializeField] private AudioEvent _walkingSfx;
    [SerializeField] private AudioEvent _alertSfx;

    void Start()
    {
        GameObject soundSource = _walkingSfx.Play(null);
        soundSource.transform.position = gameObject.transform.position;
        soundSource.transform.parent = gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
        movDirection = new Vector2[] { Vector2.right, Vector2.left }[Random.Range(0, 2)];
    }

    void FixedUpdate()
    {
        rb.velocity = movDirection * movSpeed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Janky-ass turn logic using waypoints
        string turnType = other.transform.parent.name;
        if (VectorUtil.IsWithinRange(other.transform.position, gameObject.transform.position, 0.02f))
        {
            switch (turnType)
            {
                case "LeftTIntersection":
                    movDirection = RandomizeDirection(new List<Vector2> { Vector2.up, Vector2.down, Vector2.left });
                    break;
                case "RightTIntersection":
                    movDirection = RandomizeDirection(new List<Vector2> { Vector2.up, Vector2.down, Vector2.right });
                    break;
                case "UpTIntersection":
                    movDirection = RandomizeDirection(new List<Vector2> { Vector2.up, Vector2.left, Vector2.right });
                    break;
                case "DownTIntersection":
                    movDirection = RandomizeDirection(new List<Vector2> { Vector2.left, Vector2.down, Vector2.right });
                    break;
                case "DownLeftTurn":
                    movDirection = RandomizeDirection(new List<Vector2> { Vector2.down, Vector2.left });
                    break;
                case "DownRightTurn":
                    movDirection = RandomizeDirection(new List<Vector2> { Vector2.down, Vector2.right });
                    break;
                case "UpLeftTurn":
                    movDirection = RandomizeDirection(new List<Vector2> { Vector2.up, Vector2.left });
                    break;
                case "UpRightTurn":
                    movDirection = RandomizeDirection(new List<Vector2> { Vector2.up, Vector2.right });
                    break;
                case "CrossRoads":
                    movDirection = RandomizeDirection(new List<Vector2> { Vector2.down, Vector2.right, Vector2.left, Vector2.up });
                    break;
                default: break; //do nothing if unspecified collision is encountered
            }
        }
    }

    private Vector2 RandomizeDirection(List<Vector2> availableDirections)
    {
        Vector2 cameFrom = new Vector2(rb.velocity.normalized.x * -1, rb.velocity.normalized.y * -1);
        availableDirections.Remove(cameFrom);
        return availableDirections[Random.Range(0, availableDirections.Count)];
    }

}
