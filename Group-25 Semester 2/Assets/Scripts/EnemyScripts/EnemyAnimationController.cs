using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyAnimationController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Animation Settings")]
    [Tooltip("Multiplier to scale the speed parameter sent to the animator if needed.")]
    public float speedMultiplier = 1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (agent == null || animator == null)
            return;

        // Calculate the actual current movement speed of the NavMeshAgent
        float currentSpeed = agent.velocity.magnitude * speedMultiplier;

        // Feed the speed value into the Animator parameter
        animator.SetFloat("Speed", currentSpeed);
    }
}