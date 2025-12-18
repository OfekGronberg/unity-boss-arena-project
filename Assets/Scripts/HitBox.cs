using UnityEngine;

public class Hitbox : MonoBehaviour
{
    // Tunable knockback strength
    public float knockbackForce = 6f;

    // Reference to player (to compute push direction)
    public Transform player;
    public int damage = 1;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        // 1) Damage (only if enemy has Health)
        Health h = other.GetComponent<Health>();
        if (h != null)
        {
            h.TakeDamage(damage);
        }
        // If the enemy has a Rigidbody, push it away from the player
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            // Direction from player -> enemy (ignore Y so we only push on ground)
            Vector3 dir = (other.transform.position - player.position);
            dir.y = 0f;
            dir = dir.normalized;

            rb.AddForce(dir * knockbackForce, ForceMode.Impulse);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;

        BoxCollider box = GetComponent<BoxCollider>();
        if (box != null)
        {
            Gizmos.DrawWireCube(box.center, box.size);
        }
    }
}
