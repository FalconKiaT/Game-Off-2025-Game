using UnityEngine;

public class SpriteChangeJudge : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private JudgementSystem judgementSystem;

    [SerializeField] private SpriteRenderer faceRenderer;
    [SerializeField] private Sprite waveSprite;
    [SerializeField] private Sprite suspiciousSprite;

    public void ChangeJudge()
    {
        string grade = judgementSystem.GetJudgement();

        if (grade is "A+" or "A" or "A-" or "B+" or "B" or "B-")
            faceRenderer.sprite = waveSprite;
        else
        {
            faceRenderer.sprite = suspiciousSprite;
        }
    }
}
