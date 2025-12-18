using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Hitbox")]
    [SerializeField] private GameObject attackHitbox; // drag EnemyAttackHitBox here

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 1.8f; // only for gizmo/tuning (damage comes from hitbox)

    [Header("Timing")]
    [SerializeField] private float windup = 0.35f;
    [SerializeField] private float activeTime = 0.10f;
    [SerializeField] private float cooldown = 0.80f;

    [Header("Debug")]
    [SerializeField] private bool drawGizmos = true;

    private bool isAttacking;
    private float nextAttackTime;

    void Awake()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(false);
    }

    // Called by EnemyAI
    public bool TryStartAttack()
    {
        if (isAttacking) return false;
        if (Time.time < nextAttackTime) return false;

        StartCoroutine(AttackRoutine());
        return true;
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // WINDUP
        yield return new WaitForSeconds(windup);

        // ACTIVE (hitbox ON)
        if (attackHitbox != null)
            attackHitbox.SetActive(true);

        yield return new WaitForSeconds(activeTime);

        // Hitbox OFF
        if (attackHitbox != null)
            attackHitbox.SetActive(false);

        // COOLDOWN
        nextAttackTime = Time.time + cooldown;
        isAttacking = false;
    }

    // Used by EnemyAI to freeze movement during windup/active/cooldown
    public bool IsAttacking() => isAttacking || Time.time < nextAttackTime;

    public float AttackRange => attackRange; // optional (not required)

    void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
