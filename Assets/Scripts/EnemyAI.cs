using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State { Idle, Chase, Attack }
    [SerializeField] private State state = State.Idle; // start idle

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private EnemyAttack enemyAttack;

    [Header("Ranges")]
    [SerializeField] private float engageRange = 10f;     // start chasing when player is within this
    [SerializeField] private float disengageRange = 12f;  // stop chasing when player goes beyond this (prevents jitter)

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stopDistance = 1.5f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (enemyAttack == null)
            enemyAttack = GetComponent<EnemyAttack>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        switch (state)
        {
            case State.Idle:
                // ✅ Do NOT rotate when idle.
                if (dist <= engageRange)
                    state = State.Chase;
                break;

            case State.Chase:
                // If player escaped far enough, go idle
                if (dist > disengageRange)
                {
                    state = State.Idle;
                    break;
                }

                FacePlayer();

                if (dist <= stopDistance)
                {
                    state = State.Attack;
                    break;
                }

                MoveTowardPlayer();
                break;

            case State.Attack:
                // If player escaped far enough, go back to chase/idle
                if (dist > disengageRange)
                {
                    state = State.Idle;
                    break;
                }

                if (dist > stopDistance + 0.2f) // small buffer prevents flicker
                {
                    state = State.Chase;
                    break;
                }

                FacePlayer();

                // Start attack if possible (EnemyAttack handles cooldown)
                if (enemyAttack != null)
                    enemyAttack.TryStartAttack();

                // Freeze movement while attacking/cooldown
                // (EnemyAttack.IsAttacking() returns true during cooldown too)
                break;
        }
    }

    private void MoveTowardPlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        if (toPlayer.sqrMagnitude < 0.001f) return;

        Vector3 dir = toPlayer.normalized;
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    private void FacePlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        if (toPlayer.sqrMagnitude < 0.001f) return;

        Quaternion targetRot = Quaternion.LookRotation(toPlayer, Vector3.up);
        rb.MoveRotation(targetRot);
    }
}
