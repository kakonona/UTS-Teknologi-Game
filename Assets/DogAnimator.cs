using UnityEngine;
using UnityEngine.AI;

public class DogAnimator : MonoBehaviour
{
    public Animator animator;
    public float moveThreshold = 0.1f;   // di bawah ini dianggap diam
    public string moveParam = "IsMoving";

    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (animator) animator.applyRootMotion = false;
    }

    void Update()
    {
        if (animator == null || agent == null) return;
        bool moving = agent.velocity.magnitude > moveThreshold;
        animator.SetBool(moveParam, moving);
    }
}