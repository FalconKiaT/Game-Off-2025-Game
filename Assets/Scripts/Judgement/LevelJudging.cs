using UnityEngine;

public class LevelJudging : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private JudgementSystem judgementSystem;

    [SerializeField] private SpriteRenderer faceRenderer;
    [SerializeField] private Sprite mehSprite;
    [SerializeField] private Sprite coolSprite;
    [SerializeField] private Sprite epicSprite;
    [SerializeField] private Sprite groovySprite;

    private void Update()
    {
        if (!judgementSystem)
            return;

        string grade = judgementSystem.GetJudgement();

        if (faceRenderer)
            faceRenderer.sprite = GetSpriteForGrade(grade);
    }

    private Sprite GetSpriteForGrade(string grade)
    {
        // Worst -> Best : meh, cool, epic, groovy

        if (grade is "A+" or "A" or "A-")
            return groovySprite;      // best

        if (grade is "B+" or "B" or "B-")
            return epicSprite;

        if (grade is "C+" or "C")
            return coolSprite;

        return mehSprite;             // D / F / anything else
    }
}
