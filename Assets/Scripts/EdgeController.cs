using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject rightEdge;
    [SerializeField] private GameObject leftEdge;
    void Start()
    {
        
    }

    //TODO smooth out transition
    private void TeleportToRight(Collider2D other)
    {
        other.transform.position = rightEdge.transform.position + new Vector3(-1f,0f);// + Vector3.left;
    }

    private void TeleportToLeft(Collider2D other)
    {
        other.transform.position = leftEdge.transform.position + new Vector3(1f,0f);// + Vector3.right;
    }

    
}
