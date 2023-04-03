using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _wideCamSize;
    [SerializeField] private float _narrowCamSize;

    private BoundsInt _mapBounds;

    private float _aspect;

    private Camera _cam;

    private Vector2 _floatingCamDir;
    // Start is called before the first frame update
    void Start()
    {
        _cam = GetComponent<Camera>();
        _aspect = Screen.width / Screen.height;
        _mapBounds = new BoundsInt(-12, -20, -5, 12 * 2, 20 * 2, 5); //established in editor and hardcoded in as map doesn't change
        _floatingCamDir = Vector2.down;
    }


    void FixedUpdate()
    {
        if (UIController.Instance.IsCameraAttached) 
        {
            //if are in game, follow the player
            Vector3 playerPos = PlayerController.Instance.transform.position;

            float camVertExtent = _cam.orthographicSize;
            float camHorzExtent = _aspect * camVertExtent;
            float leftBound = _mapBounds.min.x + camHorzExtent;
            float rightBound = _mapBounds.max.x - camHorzExtent;
            float bottomBound = _mapBounds.min.y + camVertExtent;
            float topBound = _mapBounds.max.y - camVertExtent;

            if (PlayerController.Instance.IsTweaking)
            {
                _cam.orthographicSize = Mathf.MoveTowards(_cam.orthographicSize, _wideCamSize, 0.2f);
                float tempXZoomOut = Mathf.MoveTowards(Mathf.Clamp(playerPos.x, leftBound, rightBound), 0f, 0.2f);
                float tempYZoomOut = Mathf.Clamp(playerPos.y, bottomBound, topBound);
                _cam.transform.position = new Vector3(tempXZoomOut, tempYZoomOut, _cam.transform.position.z);
            }
            else
            {

                _cam.orthographicSize = Mathf.MoveTowards(_cam.orthographicSize, _narrowCamSize, 0.1f);
                float tempXZoomIn = Mathf.MoveTowards(_cam.transform.position.x, playerPos.x, 0.2f);
                float tempYZoomIn = Mathf.MoveTowards(_cam.transform.position.y, playerPos.y, 0.2f);
                _cam.transform.position = new Vector3(tempXZoomIn, tempYZoomIn, -3);
            }
        }
        else 
        {
            //otherwise float
            float topY = 10f;
            float bottomY = -8f;
            float floatingCamSize = 9.5f;
            _cam.orthographicSize = Mathf.MoveTowards(_cam.orthographicSize, floatingCamSize, 0.2f);;
            if(_floatingCamDir == Vector2.up)
            {
                _cam.transform.position = new Vector3(0,Mathf.MoveTowards(_cam.transform.position.y,topY,0.01f),-3);
            }
            else
            {
                 _cam.transform.position = new Vector3(0,Mathf.MoveTowards(_cam.transform.position.y,bottomY,0.01f),-3);
            }
            if (_cam.transform.position.y >= topY) 
            {
                _floatingCamDir = Vector2.down;
            }
            if (_cam.transform.position.y <= bottomY)
            {
                _floatingCamDir = Vector2.up;
            }

        }
    }
}
