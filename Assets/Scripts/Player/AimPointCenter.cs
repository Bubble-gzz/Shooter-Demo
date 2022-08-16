using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPointCenter : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius;
    float radiusFree;
    float radiusLocked;
    float rotateSpeed;
    float rotateSpeedFree;
    float rotateSpeedLocked;
    Camera mainCamera;
    bool concentrate;
    float concentrateSpeed;
    void Start()
    {
        rotateSpeedFree = 120;
        radiusFree = 0.3f;
        rotateSpeedLocked = 30;
        radiusLocked = 0.2f;
        radius = radiusFree;
        concentrateSpeed = 2f;

        mainCamera = GamePlay.mainCamera.GetComponent<Camera>();
        concentrate = false;
    }
 
    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        transform.rotation *= Quaternion.Euler(0, 0, rotateSpeed * Time.deltaTime);
        transform.position = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        concentrate = Input.GetMouseButton(0);
        if (concentrate)
        {
            rotateSpeed = rotateSpeedLocked;
            float pace = concentrateSpeed * Time.deltaTime;
            if (radius - radiusLocked < pace) radius = radiusLocked;
            else radius -= pace;
        }
        else {
            rotateSpeed = rotateSpeedFree;
            float pace = concentrateSpeed * Time.deltaTime;
            if (radiusFree - radius < pace) radius = radiusFree;
            else radius += pace;
        }
    }
}
