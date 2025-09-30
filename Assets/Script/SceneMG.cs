using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMG : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
                     Application.Quit();
#endif
    }
}
