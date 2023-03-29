using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _wideCamSize;
    [SerializeField] private float _narrowCamSize;

    private Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        if (PlayerController.Instance.IsTweaking)
        {
            //_cam.transform.position = new Vector3(playerPos.x, playerPos.y, -3);
            _cam.orthographicSize = Mathf.MoveTowards(_cam.orthographicSize, _wideCamSize, 0.2f);
            float tempXZoomOut = Mathf.MoveTowards(playerPos.x, 0f, 0.2f);
            float tempYZoomOut = Mathf.Clamp(_cam.transform.position.y, -12, 10);
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
}
