using UnityEngine;

public class SpriteShadowSync : MonoBehaviour
{
    public SpriteRenderer shadowRenderer;
    private SpriteRenderer parentRenderer;

    void Start()
    {
        parentRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (shadowRenderer != null && parentRenderer != null)
        {
            shadowRenderer.sprite = parentRenderer.sprite;
            shadowRenderer.flipX = parentRenderer.flipX;
            shadowRenderer.flipY = parentRenderer.flipY;
        }
    }
}
