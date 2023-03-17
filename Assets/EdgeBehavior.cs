using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D col;
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D other)
    {
        switch (transform.name){
            case "RightEdge": transform.parent.SendMessage("TeleportToLeft", other); break;
            case "LeftEdge": transform.parent.SendMessage("TeleportToRight", other); break;
        }
        
    }
}
