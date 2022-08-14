using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float ZERO = 0.001f;
    Player player;
    float maxMoveSpeed{
        get{
            return player.steerinfo.maxMoveSpeed * player.myTimeScale;
        }
    }
    float dashForce{
        get{
            return player.steerinfo.dashForce * player.myTimeScale;
        }
    }
    int dashCount{
        get{
            return player.steerinfo.dashCount;
        }
        set{
            player.steerinfo.dashCount = value;
        }
    }
    int maxDashCount{
        get{
            return player.steerinfo.maxDashCount;
        }
    }

    float damping{
        get{
            if (isDashing) return player.steerinfo.damping * 2;
            return player.steerinfo.damping;
        }
    }
    float pushForce{
        get{
            return player.steerinfo.pushForce;
        }
    }
    float recoilFactor{
        get{
            return player.steerinfo.recoilFactor;
        }
    }
    float dashChargeCountdown{
        get{
            return player.steerinfo.dashChargeCountdown;
        }
    }

    float facingAngle;
    Rigidbody2D body;
    Vector3 facingVector;
    [SerializeField]
    Vector2 velocity;
    [SerializeField]
    bool isDashing;
    float steerFactorOfDash;
    bool isDashChargeCountdown;
    Vector2 lastDirection;

    int debugCounter;
    void Awake()
    {

    }
    void Start()
    {
        player = GamePlay.player;
        body = transform.parent.gameObject.GetComponent<Rigidbody2D>();
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
                dashCount = dashCount - 1;
                velocity += dashForce * lastDirection;
            }
        }

        Vector2 aimVector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        if (!isDashing) velocity += aimVector * pushForce * Time.deltaTime * player.myTimeScale;
        else velocity = velocity.magnitude * (velocity + aimVector * steerFactorOfDash).normalized;

        float moveSpeed = velocity.magnitude;
        moveSpeed = Mathf.Max(moveSpeed - damping * Time.deltaTime * player.myTimeScale, ZERO);
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
        if (isDashing) return;
        velocity -= player.attackinfo.recoilForce * recoilFactor * AimerController.aimVector;
    }
}
