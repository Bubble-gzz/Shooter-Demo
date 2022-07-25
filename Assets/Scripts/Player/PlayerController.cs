using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    float damping = 30;
    [SerializeField]
    float pushForce = 80;
    [SerializeField]
    float maxMoveSpeed = 10;

    [SerializeField]
    float facingAngle;
    
    float moveSpeed;
    Rigidbody2D body;
    Vector3 facingVector;
    [SerializeField]
    Vector2 velocity;
    float fireLoadTime = 0.1f;
    bool fireFreeze;
    [SerializeField]
    GameObject bulletPrefab;
    void Awake()
    {
        GamePlay.player = transform.parent.gameObject;
    }
    void Start()
    {
        fireFreeze = false;
        body = transform.parent.GetComponent<Rigidbody2D>();
        velocity = new Vector2(0,0);
    //    moveSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void FixedUpdate()
    {   
        FireCheck();
    }
    void Move() // wsad
    {
        Vector2 aimVector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        velocity += aimVector * pushForce * Time.deltaTime;
        float moveSpeed = velocity.magnitude;
        moveSpeed = Mathf.Max(moveSpeed - damping * Time.deltaTime, 0.001f);
        if (moveSpeed > maxMoveSpeed) moveSpeed = maxMoveSpeed;
        velocity = velocity.normalized * moveSpeed;
        if (moveSpeed > 0.001f) body.velocity = velocity;
        facingAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, facingAngle);
        //Mathf.Deg2Rad(facingAngle);
        
    }
    void FireCheck()
    {
        if (Input.GetMouseButton(0))
        {
            if (!fireFreeze)
                StartCoroutine(Fire());
        }
    }
    IEnumerator Fire()
    {
        if (bulletPrefab == null) yield break;
        fireFreeze = true;
    //    Debug.Log("Fire");
        GameObject bullet = Instantiate(bulletPrefab);
        yield return new WaitForSeconds(fireLoadTime);
        fireFreeze = false;
    }
    void Movement1() // toward mouse
    {
        Vector3 aimVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.eulerAngles = new Vector3(0, 0, Utility.Vec2Angle(aimVector));

        if (Input.GetKey(KeyCode.W)) {
            moveSpeed += pushForce * Time.deltaTime;
            if (moveSpeed > maxMoveSpeed) moveSpeed = maxMoveSpeed;
        }
        if (Input.GetKey(KeyCode.S)) {
            moveSpeed -= pushForce * Time.deltaTime;
            if (moveSpeed < -maxMoveSpeed) moveSpeed = -maxMoveSpeed;
        }
        if (moveSpeed > 0) moveSpeed = Mathf.Max(moveSpeed - damping * Time.deltaTime, 0.001f);
        if (moveSpeed < 0) moveSpeed = Mathf.Min(moveSpeed + damping * Time.deltaTime, 0.001f);
        //Mathf.Deg2Rad(facingAngle);
        body.velocity = getDirVec(facingAngle) * moveSpeed;
        while (facingAngle > 360) facingAngle -= 360;
        while (facingAngle < 0) facingAngle += 360;
    }
    Vector3 getDirVec(float angleDeg)
    {
        float angle = angleDeg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0).normalized;
    }
}
