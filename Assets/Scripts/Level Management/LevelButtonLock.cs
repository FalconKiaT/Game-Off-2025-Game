using UnityEngine;
using UnityEngine.UI;

public class LevelButtonLock : MonoBehaviour
{
    [Header("Settings")]
    public int levelIndex = 1;
    public Button button;

    private void Start()
    {
        UpdateButtonState();
    }

    public void UpdateButtonState()
    {
        if (ProgressData.Instance != null)
        {
            button.interactable = ProgressData.Instance.IsLevelUnlocked(levelIndex);
        }
        else
        {
            button.interactable = false;
        }
    }
}
