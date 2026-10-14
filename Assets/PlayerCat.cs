using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerCat : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float turnSpeed = 12f;
    public float gravity = -20f;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float drainPerSec = 25f;
    public float regenPerSec = 15f;
    public float regenDelay = 1f;
    public float Stamina01 => stamina / maxStamina;

    [Header("Animasi (nama state di Animator)")]
    public Animator animator;
    public string idleState = "Idle";
    public string runState = "Run";

    CharacterController cc;
    float yVel, stamina, regenTimer;
    bool exhausted;
    string current;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        stamina = maxStamina;
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        float x = (kb.dKey.isPressed ? 1 : 0) - (kb.aKey.isPressed ? 1 : 0);
        float z = (kb.wKey.isPressed ? 1 : 0) - (kb.sKey.isPressed ? 1 : 0);
        Vector3 dir = new Vector3(x, 0, z).normalized;
        bool moving = dir.sqrMagnitude > 0.01f;

        bool wantSprint = kb.leftShiftKey.isPressed && moving && !exhausted;
        if (wantSprint)
        {
            stamina -= drainPerSec * Time.deltaTime;
            regenTimer = regenDelay;
            if (stamina <= 0) { stamina = 0; exhausted = true; }
        }
        else
        {
            regenTimer -= Time.deltaTime;
            if (regenTimer <= 0) stamina = Mathf.Min(maxStamina, stamina + regenPerSec * Time.deltaTime);
            if (exhausted && stamina > maxStamina * 0.2f) exhausted = false;
        }

        if (moving)
        {
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, turnSpeed * Time.deltaTime);
        }

        float speed = wantSprint ? sprintSpeed : walkSpeed;
        yVel = cc.isGrounded ? -1f : yVel + gravity * Time.deltaTime;
        cc.Move((dir * speed + Vector3.up * yVel) * Time.deltaTime);

        if (animator)
        {
            animator.SetBool("IsMoving", moving);
            animator.speed = wantSprint ? 1.4f : 1f;
        }
    }

    void Play(string state)
    {
        if (animator == null || state == current) return;
        current = state;
        animator.CrossFadeInFixedTime(state, 0.15f);
    }
}