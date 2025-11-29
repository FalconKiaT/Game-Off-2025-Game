using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Script that loads a specific scene
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private string sceneToLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
