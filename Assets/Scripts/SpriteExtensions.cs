using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteExtensions
{
    public static Texture2D ToTexture(this Sprite sprite)
    {

        if ((int)sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        return sprite.texture;
    }
}
