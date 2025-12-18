using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    void OnDeath()
    {
        Debug.Log("Enemy defeated!");
        gameObject.SetActive(false);
    }
}
