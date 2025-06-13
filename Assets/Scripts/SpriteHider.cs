using UnityEngine;

public class SpriteHider : MonoBehaviour
{
    public bool hideChildrenSprites = false;

    void Update()
    {
        SetChildrenSpritesVisible(!hideChildrenSprites);
    }

    void SetChildrenSpritesVisible(bool visible)
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var sr in spriteRenderers)
        {
            sr.enabled = visible;
        }
    }
}