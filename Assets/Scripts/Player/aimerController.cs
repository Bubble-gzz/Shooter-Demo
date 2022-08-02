using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public static Vector2 aimVector;
    Text debug_aimVector;
    void Awake()
    {
        GamePlay.playerLauncher = transform.Find("Texture").gameObject;
        aimVector = new Vector2(0,0);
        debug_aimVector = GameObject.Find("Debug_AimVector")?.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Turn();

    }
    void Turn()
    {
        aimVector = ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
        if (debug_aimVector) debug_aimVector.text = "AimVector = " + aimVector.ToString("F3");
        transform.eulerAngles = new Vector3(0, 0, Utility.Vec2Angle(aimVector));
    }

}
