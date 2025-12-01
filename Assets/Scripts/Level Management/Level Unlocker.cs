using UnityEngine;
using UnityEngine.Serialization;

public class LevelUnlocker : MonoBehaviour
{
    [Header("Level To Unlock")]
    public int target = 1;

    public void Unlock()
    {
        if (ProgressData.Instance != null)
        {
            ProgressData.Instance.UnlockNextLevel(target);
        }
    }
}
