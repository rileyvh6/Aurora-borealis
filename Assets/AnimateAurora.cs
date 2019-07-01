using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System.Linq;

public class AnimateAurora : MonoBehaviour
{
    public float FPS = 60;
    public SpriteAtlas Atlas;
    public Material Material;
    public string SpriteName;


    private float currentCooldown;
    private int currentFrame = -1; //Because AdvanceFrame adds 1 to currentFrame
    private Texture2D[] spriteTextures;

    private float Cooldown => 1 / FPS;

    private void Awake()
    {
        if (Material == null)
            Material = GetComponentInChildren<Renderer>().material;
        Sprite[] sprites = new Sprite[Atlas.spriteCount];
//        Atlas.GetSprites(sprites);
        for (int i = 0;i < Atlas.spriteCount;i++)
        {
            string Position = string.Empty;
            /*            if (i < 1000)
                            Position += "0";
                        if (i < 100)
                            Position += "0";
                        if (i < 10)
                            Position += "0";*/
            Position += i.ToString();
            string Name = SpriteName + Position;
//            Debug.Log("Accessing " + Name,this);
            sprites[i] = Atlas.GetSprite(Name);
        }
        spriteTextures = sprites.Select(o => o.ToTexture()).ToArray();
    }

    private void LateUpdate()
    {
        if (currentCooldown < Cooldown)
        {
            currentCooldown += Time.deltaTime;
            return;
        }
        currentCooldown = 0;
        AdvanceFrame();
    }

    private void AdvanceFrame()
    {
        currentFrame++;
        currentFrame = currentFrame % spriteTextures.Length;

        Texture2D TargetFrame = spriteTextures[currentFrame];

        Material.mainTexture = TargetFrame;
        Material.SetTexture("_EmissionMap", TargetFrame);

    }
}
