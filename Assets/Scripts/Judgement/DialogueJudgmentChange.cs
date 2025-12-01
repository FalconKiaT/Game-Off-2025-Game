using UnityEngine;

public class DialogueJudgmentChange : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private JudgementSystem judgementSystem;
    [SerializeField] private DialogueManager dialogueManager;
    
    [Header("Dialogue Judgment")]
    [SerializeField, TextArea] private string waveDialogue;
    [SerializeField, TextArea] private string susDialogue;

    public void ChangeDialogue()
    {
        string grade = judgementSystem.GetJudgement();

        if (grade is "A+" or "A" or "A-" or "B+" or "B" or "B-")
            dialogueManager.dialogueMessage = waveDialogue;
        else
        {
            dialogueManager.dialogueMessage = susDialogue;
        }
    }
}
