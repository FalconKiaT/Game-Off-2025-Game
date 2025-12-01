using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private string soundName;
    [SerializeField] private SoundType soundType;
    
    public void PlaySound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play(soundName, soundType);
        }
    }
}
