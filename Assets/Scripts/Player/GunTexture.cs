using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTexture : MonoBehaviour
{
    // Start is called before the first frame update
    bool isRecoiling;
    float recoilFactor = 0.2f;
    void Start()
    {
        AttackController.Event_Fire?.AddListener(Recoil);
        isRecoiling = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Recoil()
    {
        if (isRecoiling) return;
        StartCoroutine(_Recoil());
    }
    IEnumerator _Recoil()
    {
        isRecoiling = true;
        float dist = AttackController.recoilForce * recoilFactor, speed = AttackController.recoilRecoverSpeed;
        Vector3 localPosition0 = transform.localPosition;
        transform.localPosition += new Vector3(-dist,0,0);
        float totalDistance = 0;
        while (totalDistance < dist - speed * Time.deltaTime)
        {
            totalDistance += speed * Time.deltaTime;
            transform.localPosition += new Vector3(speed * Time.deltaTime,0,0);
            yield return null;
        }
        transform.localPosition = localPosition0;
        isRecoiling = false;
    }
}
