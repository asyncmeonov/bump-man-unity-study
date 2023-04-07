using UnityEngine;

//Monitors walking off edges and adjusts camera. There definitely is a better way to do this
public class EdgeBehavior : MonoBehaviour
{
    private BoxCollider2D col;
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (transform.name){
            case "RightEdge": transform.parent.SendMessage("TeleportToLeft", other); break;
            case "LeftEdge": transform.parent.SendMessage("TeleportToRight", other); break;
        }

        if(other.gameObject.tag == "Player" && !PlayerController.Instance.IsTweaking)
        {
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = other.transform.position;
        } 
        
    }
}
