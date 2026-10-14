using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChase : MonoBehaviour
{
    public Transform target;
    public float chaseSpeed = 3.5f;      // kucing walk 5, jadi anjing lebih pelan
    public float stopDistance = 2f;      // berhenti kalau sedekat ini
    public float resumeDistance = 3.5f;  // lanjut ngejar kalau kucing menjauh segini
    public Animator animator;

    NavMeshAgent agent;
    bool chasing = true;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;
        agent.stoppingDistance = 0f;
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (animator) animator.applyRootMotion = false;
    }

    void Start()
    {
        if (target == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) target = p.transform;
        }

        if (NavMesh.SamplePosition(transform.position, out var hit, 2f, NavMesh.AllAreas))
            agent.Warp(hit.position);
        else
            Debug.LogWarning("EnemyChase: posisi anjing di luar NavMesh, bake ulang arena!");
    }

    void Update()
    {
        if (target == null || !agent.isOnNavMesh) return;

        float d = Vector3.Distance(transform.position, target.position);
        if (chasing && d <= stopDistance) chasing = false;
        else if (!chasing && d >= resumeDistance) chasing = true;

        if (chasing) agent.SetDestination(target.position);
        else agent.ResetPath();

        
    }
}