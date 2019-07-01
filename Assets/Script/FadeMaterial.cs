using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMaterial : MonoBehaviour
{
    Renderer rend;
    [SerializeField] float threshold;
    [SerializeField] string colorName;
    [SerializeField] float speed;
    bool toggleFade = true;
    Color matColor, matColorBackup;
    Color fadeColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        matColor = rend.material.GetColor(colorName);
        matColorBackup = matColor;
        fadeColor = matColor;
        fadeColor.a = 0f;
    }


    void Update()
    {
        matColor = Color.Lerp(matColor, !toggleFade ? matColorBackup : fadeColor, Time.deltaTime * speed);
       
        if (matColor.a <= threshold)
        {
            toggleFade = false;
            Debug.Log("Toggle");
        }
        else if (matColor.a >= 0.98f && !toggleFade)
        {
            toggleFade = true;
        }
        try
        {
            rend.material.SetColor(colorName, matColor);
        }
        catch { Debug.Log("Error"); }

    }

}
