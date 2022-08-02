using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour
{
    public static UnityEvent Event_Fire;
    float _fireLoadTime;
    float fireLoadTime{
        get{
            return _fireLoadTime / core.myTimeScale;
        }
        set{
            _fireLoadTime = value;
        }
    }
    float _bulletSpeed;
    float bulletSpeed{
        get{
            return _bulletSpeed * core.myTimeScale;
        }
        set{
            _bulletSpeed = value;
        }
    }
    bool fireFreeze;
    [SerializeField]
    GameObject bulletPrefab;
    Player core;
    public static float recoilForce;
    public static float recoilRecoverSpeed;
    void Awake()
    {
        Event_Fire = new UnityEvent();
        recoilForce = 1.0f;
        recoilRecoverSpeed = 3f;
    }
    void Start()
    {
        fireLoadTime = 0.3f;
        bulletSpeed = 100;
        core = GameObject.Find("Player").GetComponent<Player>();
        fireFreeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        FireCheck();
    }
    void FireCheck()
    {
        if (Input.GetMouseButton(0))
        {
            if (!fireFreeze)
            {
                Event_Fire.Invoke();
                StartCoroutine(Fire());
            }
        }
    }

    IEnumerator Fire()
    {
    //    Debug.Log("Fire");
        if (bulletPrefab == null) yield break;
        fireFreeze = true;
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        yield return new WaitForSeconds(fireLoadTime);
        fireFreeze = false;
    }

}
