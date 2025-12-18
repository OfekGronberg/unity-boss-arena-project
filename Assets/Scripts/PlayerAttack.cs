using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Hitbox")]
    [SerializeField] private GameObject attackHitbox; // child object (AttackHitBox)

    [Header("Timing")]
    [SerializeField] private float windup = 0.1f;     // delay before hit becomes active
    [SerializeField] private float activeTime = 0.25f; // how long hitbox stays on
    [SerializeField] private float cooldown = 0.4f;   // time before next attack allowed

    private bool isAttacking;
    private float nextAttackTime;

    void Awake()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(false);
    }

    void Update()
    {
        if (Time.time < nextAttackTime) return;

        // Left mouse OR J key (change if you want)
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("ATTACK pressed!");
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        if (isAttacking) yield break;
        isAttacking = true;

        // Windup
        yield return new WaitForSeconds(windup);

        // Active frames (hitbox ON)
        if (attackHitbox != null)
            attackHitbox.SetActive(true);

        yield return new WaitForSeconds(activeTime);

        // Hitbox OFF
        if (attackHitbox != null)
            attackHitbox.SetActive(false);

        // Cooldown
        nextAttackTime = Time.time + cooldown;
        isAttacking = false;
    }
}
