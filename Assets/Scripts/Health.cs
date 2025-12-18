using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP = 10;
    int hp;
    bool dead;

    void Awake()
    {
        hp = maxHP;
        dead = false;
    }

    public void TakeDamage(int amount)
    {
        if (dead) return;              // ✅ ignore damage after death
        if (amount <= 0) return;

        hp -= amount;
        hp = Mathf.Max(hp, 0);         // ✅ never go below 0

        Debug.Log(name + " HP = " + hp);

        if (hp <= 0)
        {
            dead = true;
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " died");

        // Notify other components on this GameObject
        SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
    }

    public float GetHP01()
    {
        return Mathf.Clamp01((float)hp / maxHP);
    }

    public bool IsDead()
    {
        return dead;
    }
}
