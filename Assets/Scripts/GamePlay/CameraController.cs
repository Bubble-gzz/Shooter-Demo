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
    int number;
    [SerializeField]
    float subLeftRate = 0.2f, subRightRate = 0.8f, subTopRate = 0.2f, subBottomRate = 0.8f;
    float subLeft, subRight, subTop, subBottom;
    Camera mainCam;
    void Awake()
    {
        GamePlay.mainCamera = GetComponent<CameraController>();
        mainCam = GetComponent<Camera>();
    }
    void Start()
    {
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
        subLeft = Screen.width * subLeftRate;
        subRight = Screen.width * subRightRate;
        subTop = Screen.height * subTopRate;
        subBottom = Screen.height * subBottomRate;

        Vector3 desiredPosition = target.position + new Vector3(0,0,-5);
        float left = mainCam.ScreenToWorldPoint(new Vector3(subLeft,0,0)).x;
        float right = mainCam.ScreenToWorldPoint(new Vector3(subRight,0,0)).x;
        float top = mainCam.ScreenToWorldPoint(new Vector3(0,subTop,0)).y;
        float bottom = mainCam.ScreenToWorldPoint(new Vector3(0,subBottom,0)).y;
        //Debug.Log("cam leftBorder: " + left);
        Vector3 newPosition = transform.position;
        Vector3 delta = new Vector3(0,0,0);
        if (target.position.x < left) delta.x = target.position.x - left;
        if (target.position.x > right) delta.x = target.position.x - right;
        if (target.position.y < bottom) delta.y = target.position.y - bottom;
        if (target.position.y > top) delta.y = target.position.y - top;
        newPosition = Vector3.Lerp(newPosition, newPosition + delta, smoothSpeed * Time.deltaTime);
       // if (OutofBoundary(mainCam.WorldToScreenPoint(target.position)))
      //  {
            //Vector3 newPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = newPosition;
       // }
    }
    bool OutofBoundary(Vector2 pos)
    {
        if (pos.x < subLeft || pos.x > subRight) return true;
        if (pos.y < subBottom || pos.y > subTop) return true;
        return false;
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
