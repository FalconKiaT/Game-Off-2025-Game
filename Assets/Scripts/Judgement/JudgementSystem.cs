using UnityEngine;

public class JudgementSystem : MonoBehaviour
{
    [Header("References - Right / Primary Arm")]
    [SerializeField] SpriteRenderer characterBicepA;
    [SerializeField] SpriteRenderer characterForearmA;
    [SerializeField] SpriteRenderer characterHandA;

    [SerializeField] SpriteRenderer targetBicepA;
    [SerializeField] SpriteRenderer targetForearmA;
    [SerializeField] SpriteRenderer targetHandA;

    [Header("References - Left / Secondary Arm ")]
    [SerializeField] SpriteRenderer characterBicepB;
    [SerializeField] SpriteRenderer characterForearmB;
    [SerializeField] SpriteRenderer characterHandB;

    [SerializeField] SpriteRenderer targetBicepB;
    [SerializeField] SpriteRenderer targetForearmB;
    [SerializeField] SpriteRenderer targetHandB;

    [Header("Settings")]
    [SerializeField] private float tolerance = 0.25f;
    [SerializeField] private float maxFailDistance = 2f;

    [Header("Mode")]
    [SerializeField] private bool useTwoArms = false; 


    public void ToggleTwoArms()
    {
        useTwoArms = !useTwoArms;
    }

    /// <summary>
    /// Calculates a judgment grade based on how closely
    /// the player's arm positions match the target.
    /// Supports one or two arms depending on useTwoArms.
    /// </summary>
    public string GetJudgement()
    {
        float totalScore = 0f;
        int segmentCount = 0;

        // Right / primary arm
        totalScore += ScorePart(characterBicepA.transform.position, targetBicepA.transform.position);
        totalScore += ScorePart(characterForearmA.transform.position, targetForearmA.transform.position);
        totalScore += ScorePart(characterHandA.transform.position, targetHandA.transform.position);
        segmentCount += 3;

        // Left / secondary arm (only if enabled)
        if (useTwoArms)
        {
            totalScore += ScorePart(characterBicepB.transform.position, targetBicepB.transform.position);
            totalScore += ScorePart(characterForearmB.transform.position, targetForearmB.transform.position);
            totalScore += ScorePart(characterHandB.transform.position, targetHandB.transform.position);
            segmentCount += 3;
        }

        // Average score across active segments (0–1 range)
        float averageScore = (segmentCount > 0) ? (totalScore / segmentCount) : 0f;

        // Convert to percentage 0–100
        float percentage = Mathf.Clamp01(averageScore) * 100f;

        return Grade(percentage);
    }

    /// <summary>
    /// Returns a normalized score (0–1) for a single limb segment.
    /// Perfect if within tolerance, then linearly decreasing.
    /// </summary>
    private float ScorePart(Vector2 playerPos, Vector2 targetPos)
    {
        float dist = Vector2.Distance(playerPos, targetPos);

        if (dist <= tolerance)
            return 1f;

        float normalized = 1f - ((dist - tolerance) / (maxFailDistance - tolerance));
        return Mathf.Clamp01(normalized);
    }

    /// <summary>
    /// Converts a score percentage into a letter grade.
    /// </summary>
    private string Grade(float score)
    {
        if (score >= 97f) return "A+";
        if (score >= 93f) return "A";
        if (score >= 90f) return "A-";
        if (score >= 87f) return "B+";
        if (score >= 83f) return "B";
        if (score >= 80f) return "B-";
        if (score >= 77f) return "C+";
        if (score >= 70f) return "C";
        if (score >= 60f) return "D";
        return "F";
    }
}