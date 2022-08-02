using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float ZERO = 0.001f;
    float _maxMoveSpeed;
    float maxMoveSpeed{
        get{
            return _maxMoveSpeed * core.myTimeScale;
        }
        set{
            _maxMoveSpeed = value;
        }
    }

    float _dashForce;
    float dashForce{
        get{
            return _dashForce * core.myTimeScale;
        }
        set{
            _dashForce = value;
        }
    }
    [SerializeField]
    int dashCount;
    int maxDashCount;

    float _damping;
    float damping{
        get{
            if (isDashing) return _damping * 2;
            return _damping;
        }
        set{
            _damping = value;
        }
    }
    float pushForce;
    float facingAngle;
    Rigidbody2D body;
    Vector3 facingVector;
    [SerializeField]
    Vector2 velocity;
    Player core;
    [SerializeField]
    bool isDashing;
    float steerFactorOfDash;
    float dashChargeCountdown;
    bool isDashChargeCountdown;
    Vector2 lastDirection;

    int debugCounter;
    float recoilFactor;
    void Awake()
    {
        GamePlay.player = transform.parent.gameObject;
        maxMoveSpeed = 8;
        maxDashCount = 1;
        damping = 30;
        pushForce = 80;
        dashCount = maxDashCount;
        dashForce = 22;
        dashChargeCountdown = 2f;
        isDashChargeCountdown = false;
        lastDirection = new Vector2(1,0);
        steerFactorOfDash = 0.1f;
        recoilFactor = 3.5f;
    }
    void Start()
    {
        core = GameObject.Find("Player").GetComponent<Player>();
        GameObject player = transform.parent.gameObject;
        body = player.GetComponent<Rigidbody2D>();
        velocity = new Vector2(0,0);
        debugCounter = 0;
        AttackController.Event_Fire?.AddListener(Recoil);
    }

    void Update()
    {
        DashChargeCheck();
        Move();
        //Debug.Log(core.myTimeScale);
    }
    void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Dash Start" + debugCounter);
            //debugCounter++;
            if (!isDashing && dashCount > 0)
            {
                isDashing = true;
                dashCount--;
                velocity += dashForce * lastDirection;
            }
        }

        Vector2 aimVector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        if (!isDashing) velocity += aimVector * pushForce * Time.deltaTime * core.myTimeScale;
        else velocity = velocity.magnitude * (velocity + aimVector * steerFactorOfDash).normalized;

        float moveSpeed = velocity.magnitude;
        moveSpeed = Mathf.Max(moveSpeed - damping * Time.deltaTime * core.myTimeScale, ZERO);
        if (!isDashing) {
            if (moveSpeed > maxMoveSpeed) moveSpeed = maxMoveSpeed;
        }
        velocity = velocity.normalized * moveSpeed;

        if (moveSpeed > ZERO) body.velocity = velocity;
        else body.velocity = Vector3.zero;

        facingAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, facingAngle);

        if (isDashing && moveSpeed < maxMoveSpeed) isDashing = false;
        if (velocity.magnitude > ZERO) lastDirection = velocity.normalized;
    }
    void DashChargeCheck()
    {
        if (isDashChargeCountdown || dashCount == maxDashCount) return;
        StartCoroutine(DashChargeCountdown());
    }
    IEnumerator DashChargeCountdown()
    {
        isDashChargeCountdown = true;
        yield return new WaitForSeconds(dashChargeCountdown);
        dashCount++;
        isDashChargeCountdown = false;
    }
    void Recoil()
    {
        velocity -= AttackController.recoilForce * recoilFactor * AimerController.aimVector;
    }
}
