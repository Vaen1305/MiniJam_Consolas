using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    [SerializeField] private int damageFromPogoable = 1;
    [SerializeField] private int damageFromBreakable = 1;
    [SerializeField] private string gameOverSceneName = "GameOver";
    [SerializeField] private string winSceneName = "Win";
    [SerializeField] private float delayBeforeSceneLoad = 1f;
    [SerializeField] private float pogoInvulnerabilityTime = 0.5f;
    
    private int currentHP;
    private PlayerAttack playerAttack;
    private float lastPogoTime = -999f;
    private bool isDead = false;
    private bool hasWon = false;

    void Start()
    {
        currentHP = maxHP;
        playerAttack = GetComponent<PlayerAttack>();
        
        if (playerAttack == null)
        {
            Debug.LogError("PlayerAttack no encontrado en el jugador!");
        }
    }
    
    public void TakeDamage(int dmg)
    {
        if (isDead || hasWon) return;
        currentHP -= dmg;
        Debug.Log("Player recibió " + dmg + " daño. HP = " + currentHP);
        if (currentHP <= 0) Die();
    }

    void Die()
    {
        if (isDead || hasWon) return;
        
        isDead = true;
        Debug.Log("Player muerto - Cargando escena de Game Over en " + delayBeforeSceneLoad + " segundos");
        
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;
        
        if (playerAttack != null) playerAttack.enabled = false;
        
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null) renderer.enabled = false;
        
        Invoke(nameof(LoadGameOverScene), delayBeforeSceneLoad);
    }
    
    void LoadGameOverScene()
    {
        Debug.Log("Cargando escena: " + gameOverSceneName);
        SceneManager.LoadScene(gameOverSceneName);
    }
    
    void Win()
    {
        if (isDead || hasWon) return;
        
        hasWon = true;
        Debug.Log("¡Victoria! - Cargando escena de victoria en " + delayBeforeSceneLoad + " segundos");
        
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;
        
        if (playerAttack != null) playerAttack.enabled = false;
        
        Invoke(nameof(LoadWinScene), delayBeforeSceneLoad);
    }
    
    void LoadWinScene()
    {
        Debug.Log("Cargando escena: " + winSceneName);
        SceneManager.LoadScene(winSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead || hasWon) return;
        
        if (other.CompareTag("Win"))
        {
            Debug.Log("¡Player alcanzó el objetivo de victoria!");
            Win();
            return;
        }
        
        if (other.CompareTag("Ocean"))
        {
            currentHP = 0;
            Die();
            return;
        }
        
        if (other.CompareTag("Pogoable"))
        {
            if (playerAttack != null && playerAttack.IsDownSlashing())
            {
                return;
            }
            
            if (Time.time - lastPogoTime < pogoInvulnerabilityTime)
            {
                return;
            }
            
            TakeDamage(damageFromPogoable);
        }
        
        if (other.CompareTag("Breakable"))
        {
            TakeDamage(damageFromBreakable);
        }
    }
    
    public void OnPogoPerformed()
    {
        lastPogoTime = Time.time;
    }
}