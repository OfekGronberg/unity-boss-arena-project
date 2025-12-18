using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [Header("Optional UI")]
    [SerializeField] private GameObject gameOverUI; // optional

    private bool dead;

    void OnDeath()
    {
        if (dead) return; // 🔒 prevent double-trigger
        dead = true;

        Debug.Log("Player died!");

        // Disable all gameplay scripts except Health & PlayerDeath
        var scripts = GetComponents<MonoBehaviour>();
        foreach (var s in scripts)
        {
            if (s is Health) continue;
            if (s is PlayerDeath) continue;
            s.enabled = false;
        }

        // Freeze physics (feels better than disabling RB completely)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Show Game Over UI (if assigned)
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        // Unlock cursor (useful later)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (!dead) return;

        // Press R to restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
