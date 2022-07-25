using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform target;
    float smoothSpeed = 5f;
    Camera thisCamera;
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        target = GameObject.Find("player").transform;
        thisCamera.orthographicSize = 5.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + new Vector3(0,0,-5);
        Vector3 newPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = newPostion;
    }
}
