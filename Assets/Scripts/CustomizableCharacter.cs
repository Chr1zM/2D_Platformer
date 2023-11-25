using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableCharacter : MonoBehaviour {
    [SerializeField] private int skinNr;
    [SerializeField] private Skins[] skins;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < skins.Length; i++) {
            if (skins[i].skinName == PlayerPrefs.GetString("SelectedPlayerSkin", "VirtualGuy")) {
                skinNr = i;
            }
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (skinNr >= skins.Length || skinNr < 0) {
            skinNr = Math.Abs(skinNr % skins.Length);
        }
    }

    private void LateUpdate() {
        SkinChoice();
    }

    void SkinChoice() {
        string spriteName = spriteRenderer.sprite.name;
        int spriteNr;

        switch (spriteName) {
            case string s when s.Contains("Idle (32x32)_"):
                spriteNr = int.Parse(s.Replace("Idle (32x32)_", ""));
                spriteRenderer.sprite = skins[skinNr].spritesIdle[spriteNr];
                break;
            case string s when s.Contains("Run (32x32)_"):
                spriteNr = int.Parse(s.Replace("Run (32x32)_", ""));
                spriteRenderer.sprite = skins[skinNr].spritesRun[spriteNr];
                break;
            case string s when s.Contains("Wall Jump (32x32)"):
                spriteNr = int.Parse(s.Replace("Wall Jump (32x32)_", ""));
                spriteRenderer.sprite = skins[skinNr].spriteWallslide[spriteNr];
                break;
            case string s when s.Equals("Jump (32x32)"):
                spriteRenderer.sprite = skins[skinNr].spriteJump;
                break;
            case string s when s.Equals("Fall (32x32)"):
                spriteRenderer.sprite = skins[skinNr].spriteFall;
                break;
            default:
                break;
        }
    }

}

[System.Serializable]
public struct Skins {
    public string skinName;
    public Sprite[] spritesIdle;
    public Sprite[] spritesRun;
    public Sprite[] spriteWallslide;
    public Sprite spriteJump;
    public Sprite spriteFall;

}