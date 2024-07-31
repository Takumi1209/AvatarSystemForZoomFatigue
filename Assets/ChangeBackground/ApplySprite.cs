using UnityEngine;

public class ApplySprite : MonoBehaviour
{
    public SpriteRenderer targetSpriteRenderer;  // Set this to the SpriteRenderer of your target GameObject

    private void Start()
    {
        if (SpriteManager.Instance != null && SpriteManager.Instance.SelectedSprite != null)
        {
            targetSpriteRenderer.sprite = SpriteManager.Instance.SelectedSprite;
        }
        else
        {
           // Debug.LogError("No sprite found to apply.");
        }
    }
}
