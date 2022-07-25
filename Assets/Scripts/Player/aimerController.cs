using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimerController : MonoBehaviour
{
    float facingAngle;
    float turnSpeed = 520;
    // Start is called before the first frame update
    void Awake()
    {
        GamePlay.playerLauncher = transform.Find("aimerTexture").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Turn();

    }
    void Turn()
    {
        Vector3 aimVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.eulerAngles = new Vector3(0, 0, Utility.Vec2Angle(aimVector));
    }

    void Turn1()
    {
        if (Input.GetKey(KeyCode.A)) {
            facingAngle += turnSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            facingAngle -= turnSpeed * Time.deltaTime;
        }
        transform.eulerAngles = new Vector3(0, 0, facingAngle);
    }
}
