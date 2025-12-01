using UnityEngine;

public class ProgressData : MonoBehaviour
{
    public static ProgressData Instance;

    [SerializeField] private int totalLevels = 3;

    private const string HighestLevelKey = "HighestUnlockedLevel";
    public int HighestUnlockedLevel { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        HighestUnlockedLevel = PlayerPrefs.GetInt(HighestLevelKey, 1);
    }

    public bool IsLevelUnlocked(int level)
    {
        return level <= HighestUnlockedLevel;
    }

    public void UnlockNextLevel(int currentLevel)
    {
        int nextLevel = currentLevel + 1;

        if (nextLevel > totalLevels)
            return;

        if (nextLevel > HighestUnlockedLevel)
        {
            HighestUnlockedLevel = nextLevel;
            PlayerPrefs.SetInt(HighestLevelKey, HighestUnlockedLevel);
            PlayerPrefs.Save();
        }
    }

    public void ResetProgress()
    {
        HighestUnlockedLevel = 1;
        PlayerPrefs.SetInt(HighestLevelKey, HighestUnlockedLevel);
        PlayerPrefs.Save();
    }
}
