using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }
    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player recibió " + dmg + " daño. HP = " + currentHP);
        if (currentHP <= 0) Die();
    }

    void Die()
    {
        Debug.Log("muerto");
    }
}
