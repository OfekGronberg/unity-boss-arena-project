using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private bool hitThisSwing;

    private void OnEnable()
    {
        hitThisSwing = false;
    }

    private void OnTriggerEnter(Collider other) => TryHit(other);
    private void OnTriggerStay(Collider other) => TryHit(other);

    private void TryHit(Collider other)
    {
        if (hitThisSwing) return;
        if (!other.CompareTag("Player")) return;

        Health h = other.GetComponent<Health>();
        if (h == null) return;

        hitThisSwing = true;
        h.TakeDamage(damage);
        Debug.Log("Enemy hit player for " + damage);
    }
}
