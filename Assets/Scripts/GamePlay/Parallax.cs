using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float ratio = 0.5f;
    [SerializeField]
    Vector2 anchor = new Vector2(0,0);
    GameObject mainCam;
    void Start()
    {
        mainCam = GamePlay.mainCamera.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = anchor + (Vector2)mainCam.transform.position * ratio;
    }
}
