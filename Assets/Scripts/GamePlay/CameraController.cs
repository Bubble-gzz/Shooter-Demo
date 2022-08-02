using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform target;
    float smoothSpeed;
    Camera thisCamera;
    public float attentionSize;
    float zoomoutSpeed;
    float zoominSpeed;
    int poolSize;
    int number;
    void Awake()
    {
        GamePlay.mainCamera = GetComponent<CameraController>();
    }
    void Start()
    {
        poolSize = 1000;
        smoothSpeed = 5f;
        thisCamera = GetComponent<Camera>();
        target = GameObject.Find("Player").transform;
        attentionSize = 5f;
        zoomoutSpeed = 2;
        zoominSpeed = 1;
        thisCamera.orthographicSize = attentionSize;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) attentionSize = 7f;
        else attentionSize = 5f;
        if (thisCamera.orthographicSize < attentionSize)
        {
            float gap = attentionSize - thisCamera.orthographicSize;
            float pace = zoomoutSpeed * Time.deltaTime;
            if (pace > gap) thisCamera.orthographicSize = attentionSize;
            else thisCamera.orthographicSize += pace;
        }
        else {
            float gap = thisCamera.orthographicSize - attentionSize;
            float pace = zoominSpeed * Time.deltaTime;
            if (pace > gap) thisCamera.orthographicSize = attentionSize;
            else thisCamera.orthographicSize -= pace;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + new Vector3(0,0,-5);
        Vector3 newPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = newPostion;
    }
    public IEnumerator Shake(float force, float time)
    {
        float timer = time;
        while (true)
        {
            timer -= Time.deltaTime;
            if (timer < 0) break;
            transform.position += new Vector3(Random.Range(-1.0f, 1.0f) * force, Random.Range(-1.0f, 1.0f) * force, 0);
            yield return null;
        }
    }
}
