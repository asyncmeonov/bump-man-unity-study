using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{

    
    public MobAssetDefinition mobAD;
    [SerializeField] private float movSpeed = 2f;
    private Rigidbody2D _rb;
    private Vector2 _movDirection;
    private SpriteRenderer _sr;

    void Start()
    {
        GameObject soundSource = mobAD.walkingSfx.Play(null);
        soundSource.transform.position = gameObject.transform.position;
        soundSource.transform.parent = gameObject.transform;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = mobAD.horizontalSprite;
        _movDirection = new Vector2[] { Vector2.right, Vector2.left }[Random.Range(0, 2)];
    }

    void FixedUpdate()
    {
        _rb.velocity = _movDirection * movSpeed;

        switch (_movDirection.x)
        {
            case < 0:
                _sr.sprite = mobAD.horizontalSprite;
                _sr.flipX = true;

                break;
            case > 0:
                _sr.sprite = mobAD.horizontalSprite;
                _sr.flipX = false;
                break;
        }

        switch (_movDirection.y)
        {
            case > 0:
                _sr.sprite = mobAD.upSprite;
                break;
            case < 0:
                _sr.sprite = mobAD.downSprite;
                break;
        }

        if(GameController.Instance.IsGameRunning && PlayerController.Instance.IsTweaking)
        {
            _sr.sprite = mobAD.afraidSprite;
        }
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
                    _movDirection = RandomizeDirection(new List<Vector2> { Vector2.up, Vector2.down, Vector2.left });
                    break;
                case "RightTIntersection":
                    _movDirection = RandomizeDirection(new List<Vector2> { Vector2.up, Vector2.down, Vector2.right });
                    break;
                case "UpTIntersection":
                    _movDirection = RandomizeDirection(new List<Vector2> { Vector2.up, Vector2.left, Vector2.right });
                    break;
                case "DownTIntersection":
                    _movDirection = RandomizeDirection(new List<Vector2> { Vector2.left, Vector2.down, Vector2.right });
                    break;
                case "DownLeftTurn":
                    _movDirection = RandomizeDirection(new List<Vector2> { Vector2.down, Vector2.left });
                    break;
                case "DownRightTurn":
                    _movDirection = RandomizeDirection(new List<Vector2> { Vector2.down, Vector2.right });
                    break;
                case "UpLeftTurn":
                    _movDirection = RandomizeDirection(new List<Vector2> { Vector2.up, Vector2.left });
                    break;
                case "UpRightTurn":
                    _movDirection = RandomizeDirection(new List<Vector2> { Vector2.up, Vector2.right });
                    break;
                case "CrossRoads":
                    _movDirection = RandomizeDirection(new List<Vector2> { Vector2.down, Vector2.right, Vector2.left, Vector2.up });
                    break;
                default: break; //do nothing if unspecified collision is encountered
            }
        }
    }

    private Vector2 RandomizeDirection(List<Vector2> availableDirections)
    {
        Vector2 cameFrom = new Vector2(_rb.velocity.normalized.x * -1, _rb.velocity.normalized.y * -1);
        availableDirections.Remove(cameFrom);
        return availableDirections[Random.Range(0, availableDirections.Count)];
    }

}
