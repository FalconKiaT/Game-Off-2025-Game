using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNextScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private JudgementSystem judgementSystem;
    [SerializeField] private TextMeshProUGUI buttonText;

    [Header("Settings")] 
    [SerializeField] private int nextLevelTarget;
    [SerializeField] private string mainMenuString;

    void onEnable()
    {
        string grade = judgementSystem.GetJudgement();

        if (grade is "A+" or "A" or "A-" or "B+" or "B" or "B-")
            ProgressData.Instance.UnlockNextLevel(nextLevelTarget);
            SceneManager.LoadScene(mainMenuString);
    }
}
