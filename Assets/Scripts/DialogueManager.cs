using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DialogueManager : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private Transform dialoguePanel;
    [SerializeField] private Vector2 startPosition;
    
    [Header("Dialogue Options")]
    [SerializeField] private float fontSize = 25f; // 25 = 0.25 scale
    [SerializeField] private float spacing = 19f; // width each character should be placed apart
    [SerializeField] private float maxLineWidth = 220f; // max horizontal width of the line
    [Space]
    [SerializeField] private AudioClip dialogueSound;
    [SerializeField] private float timeBetweenCharacters = 0.2f;
    
    [Space]
    [TextArea]
    public string dialogueMessage = "text";
    
    // Resources
    private Sprite[] _spriteSheet;
    private GameObject _letterPrefab;
    
    private AudioSource _source;
    private Vector2 _currentPosition;
    private int _charInLine = 0;

    private void Start()
    {
        // Load resources
        _spriteSheet = Resources.LoadAll<Sprite>("Dialogue/letters");
        _letterPrefab = Resources.Load<GameObject>("Dialogue/letter_pfb");
        _source = GetComponent<AudioSource>();

        StartCoroutine(WriteText(dialogueMessage));
    }

    private IEnumerator WriteText(string text)
    {
        _currentPosition = startPosition;

        // Write message to textbox
        for (int i = 0; i < text.Length; i++)
        {
            char currentChar = text[i];
            
            // Check ahead for word wrapping
            if (currentChar != ' ' && currentChar != '\n')
            {
                int wordLength = GetWordLength(text, i);
                float projectedWidth = (wordLength * spacing);

                bool exceedsLine = (_currentPosition.x - startPosition.x) + projectedWidth > maxLineWidth;
                if (exceedsLine) NextLine();
            }
            
            // Instantiate letter and set position/scale
            GameObject letter = Instantiate(_letterPrefab, dialoguePanel);
            RectTransform letterRect = letter.GetComponent<RectTransform>();
            
            letterRect.anchoredPosition = _currentPosition;
            letterRect.localScale *= (fontSize / 100f);
                
            // set sprite to letter
            Image letterImage = letter.GetComponent<Image>();
            int index = GetIndex(text[i]);
            letterImage.sprite = _spriteSheet[index];
            letter.name = "letter_" + index;
            
            // play sound
            PlaySpeechSound();
            
            // Adjust position of next letter
            if (currentChar == ' ')
                _currentPosition.x += spacing * 0.6f;
            else if(text[i] == 'l' || (i+1 != text.Length && text[i+1] == 'l') 
                                   || text[i] == '!' || (i+1 != text.Length && text[i+1] == '!'))
                _currentPosition.x += spacing * 0.7f;
            else
                _currentPosition.x += spacing;

            _charInLine++;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }

        yield break;
    }
    
    // Returns how many characters until space/punctuation
    private int GetWordLength(string text, int startIndex)
    {
        int length = 0;
        for (int i = startIndex; i < text.Length; i++)
        {
            char c = text[i];

            // End of word characters
            if (c == ' ' || c == ',' || c == '.' || c == '!' || c == '?' || c == '\n')
                break;

            length++;
        }

        return length;
    }
    
    // Adjust spacing for next line in text
    private void NextLine()
    {
        _currentPosition.x = startPosition.x;
        _currentPosition.y -= spacing * 1.25f;
        _charInLine = 0;
    }

    // Get the image index of a character in the spritesheet
    private int GetIndex(char c)
    {
        c = char.ToUpper(c);
        
        if (c >= 'A' && c <= 'Z')
            return c - 'A';
        
        switch (c)
        {
            case ',': return 25;
            case '.': return 26;
            case '!': return 27;
            case '?': return 28;
            case ' ': return 29;
        }

        return -1;
    }
    
    // play a short soundbyte at random pitch
    private void PlaySpeechSound()
    {
        _source.Stop();
        _source.pitch = Random.Range(0.9f, 1.1f);
        _source.clip = dialogueSound;
        _source.Play();
    }
}
