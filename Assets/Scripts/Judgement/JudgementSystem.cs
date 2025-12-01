using UnityEngine;

public class JudgementSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SpriteRenderer characterBicep;
    [SerializeField] SpriteRenderer characterForearm;
    [SerializeField] SpriteRenderer characterHand;
    
    [SerializeField] SpriteRenderer targetBicep;
    [SerializeField] SpriteRenderer targetForearm;
    [SerializeField] SpriteRenderer targetHand;
    
    [Header("Settings")]
    [SerializeField] private float tolerance = 0.25f; 
    // Distance threshold where position counts as perfect

    [SerializeField] private float maxFailDistance = 2f; 
    // When distance is >= this → zero score for that segment

    /// <summary>
    /// Calculates a judgment grade based on how closely
    /// the player's arm positions match the target.
    /// </summary>
    public string GetJudgement()
    {
        float totalScore = 0f;

        // Score each body part independently
        totalScore += ScorePart(characterBicep.transform.position, targetBicep.transform.position);
        totalScore += ScorePart(characterForearm.transform.position, targetForearm.transform.position);
        totalScore += ScorePart(characterHand.transform.position, targetHand.transform.position);

        // Average score for final pose score (0–1 range)
        float averageScore = totalScore / 3f;

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

        // Inside tolerance → full score
        if (dist <= tolerance)
            return 1f;

        // Score falls off as distance increases
        float normalized = 1f - ((dist - tolerance) / (maxFailDistance - tolerance));

        // Clamp between 0 and 1
        return Mathf.Clamp01(normalized);
    }

    /// <summary>
    /// Converts a score percentage into a letter grade using your scale.
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
