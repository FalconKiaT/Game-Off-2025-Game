using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Class used to add a random sprite to the sprite renderer attached
/// </summary>
public class RandomSpritePicker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image ImageRenderer;
    
    [Header("Sprite List")]
    [SerializeField] private List<Sprite> sprites;

    void Start()
    {
        if (sprites.Count > 0)
        {
            int randomIndex = Random.Range(0, sprites.Count);
            ImageRenderer.sprite = sprites[randomIndex];
        }
    }

    public void PickRandomSprite()
    {
        if (sprites.Count > 0)
        {
            int randomIndex = Random.Range(0, sprites.Count);
            ImageRenderer.sprite = sprites[randomIndex];
        }
    }
}
