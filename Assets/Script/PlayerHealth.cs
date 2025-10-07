using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    [SerializeField] private int damageFromPogoable = 1;
    [SerializeField] private string gameOverSceneName = "GameOver";
    [SerializeField] private float delayBeforeSceneLoad = 1f;
    [SerializeField] private float pogoInvulnerabilityTime = 0.3f; // Tiempo de invulnerabilidad después de pogo
    
    private int currentHP;
    private PlayerAttack playerAttack;
    private float lastPogoTime = -999f;

    void Start()
    {
        currentHP = maxHP;
        playerAttack = GetComponent<PlayerAttack>();
    }
    
    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player recibió " + dmg + " daño. HP = " + currentHP);
        if (currentHP <= 0) Die();
    }

    void Die()
    {
        Debug.Log("Player muerto - Cargando escena de Game Over");
        Destroy(gameObject);
        Invoke(nameof(LoadGameOverScene), delayBeforeSceneLoad);
    }
    
    void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pogoable"))
        {
            if (Time.time - lastPogoTime < pogoInvulnerabilityTime)
            {
                Debug.Log("Invulnerable por pogo reciente");
                return;
            }
            
            TakeDamage(damageFromPogoable);
        }
    }
    
    public void OnPogoPerformed()
    {
        lastPogoTime = Time.time;
        Debug.Log("Pogo realizado - Invulnerabilidad activada");
    }
}