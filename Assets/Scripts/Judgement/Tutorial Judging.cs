using TMPro;
using UnityEngine;

public class TutorialJudging : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private JudgementSystem judgementSystem;

    [SerializeField] private SpriteRenderer faceRenderer;
    [SerializeField] private Sprite mehSprite;
    [SerializeField] private Sprite coolSprite;
    [SerializeField] private Sprite epicSprite;
    [SerializeField] private Sprite groovySprite;

    [SerializeField] private TextMeshProUGUI gradeText;

    private void Update()
    {
        if (!judgementSystem)
            return;

        string grade = judgementSystem.GetJudgement();

        if (gradeText)
            gradeText.text = grade;

        if (faceRenderer)
            faceRenderer.sprite = GetSpriteForGrade(grade);
    }

    private Sprite GetSpriteForGrade(string grade)
    {
        // Worst → Best : meh, cool, epic, groovy

        if (grade == "A+" || grade == "A" || grade == "A-")
            return groovySprite;      // best

        if (grade == "B+" || grade == "B" || grade == "B-")
            return epicSprite;

        if (grade == "C+" || grade == "C")
            return coolSprite;

        return mehSprite;             // D / F / anything else
    }
}
