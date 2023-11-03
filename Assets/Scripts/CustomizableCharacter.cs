using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableCharacter : MonoBehaviour
{
    public int skinNr;
    public Skins[] skins;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (skinNr >= skins.Length || skinNr < 0)
        {
            skinNr = Math.Abs(skinNr % skins.Length);
        }
    }

    private void LateUpdate()
    {
        SkinChoice();
    }

    void SkinChoice()
    {
        if (spriteRenderer.sprite.name.Contains("Idle (32x32)"))
        {
            string spriteName = spriteRenderer.sprite.name;
            spriteName = spriteName.Replace("Idle (32x32)_", "");
            int spriteNr = int.Parse(spriteName);

            spriteRenderer.sprite = skins[skinNr].sprites[spriteNr];
        }
        else if (spriteRenderer.sprite.name.Contains("Jump (32x32)"))
        {
            int spriteNr = 11;
            spriteRenderer.sprite = skins[skinNr].sprites[spriteNr];
        }
        else if (spriteRenderer.sprite.name.Contains("Fall (32x32)"))
        {
            int spriteNr = 12;
            spriteRenderer.sprite = skins[skinNr].sprites[spriteNr];
        }
    }
}

[System.Serializable]
public struct Skins
{
    public Sprite[] sprites;
}