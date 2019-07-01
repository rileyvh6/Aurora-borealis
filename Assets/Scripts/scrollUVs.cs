using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class scrollUVs : MonoBehaviour
{
//    [Flags]
//    public enum SheetToAnimate
//    {
//        Albedo = 1 << 0,

////        Everything = Albedo
    //}

    public Vector2 Speed;
    public Material Material;
    //public SheetToAnimate Animate = SheetToAnimate.Albedo;

    //private readonly Dictionary<SheetToAnimate, string> TextureNamesAssociations = new Dictionary<SheetToAnimate, string>
    //{
        //{SheetToAnimate.Albedo,"_MainTex"}
        //};
    private Vector2 offset;
    public void Start()
    {
//        AssignValues();
    }

    public void LateUpdate()
    {
        MoveUVs();
    }

    private void Reset()
    {
//        AssignValues();
    }

    private void AssignValues()
    {
        if (Material == null)
            Material = GetComponentsInChildren<Renderer>().Select(o => o.sharedMaterial).First();
    }

    private void MoveUVs()
    {
        offset += Speed * Time.deltaTime;
        //        if (Material.HasProperty("_MainTex")) //Animate.HasFlag(SheetToAnimate.Albedo) &&
//        Vector2 CurrentOffset = Material.GetTextureOffset("_MainTex");
            Material.SetTextureOffset("_MainTex",  offset);
    }
}
