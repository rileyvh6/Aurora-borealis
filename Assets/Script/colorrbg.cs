using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorrbg : MonoBehaviour
{

    public float Timer = 0.0f;
    private Color newColor;

    public Renderer Renderer;

    public bool changeColour = true;

    Color lerpedColor = Color.white;

    private void Start()
    {
        if (Renderer == null)
            Renderer = GetComponent<Renderer>();
        StartCoroutine(ColorTmr());

    }

    protected void FixedUpdate()
    {

    }


    IEnumerator ColorChangeTmr()
    {
        while(true)
        {
            newColor = ValueManager.valueManager.RGBarr[Random.Range(0,ValueManager.valueManager.RGBarr.Length)];
            yield return new WaitForSeconds(3f);
        }
    }
    IEnumerator ColorTmr()
    {
        StartCoroutine(ColorChangeTmr());
        while (true)
        {
            Renderer.material.color = Color.Lerp(Renderer.material.color, newColor, Time.deltaTime * 2);
            if(Renderer.material.color == newColor)
                yield return new WaitForSeconds(Timer);
            else
                yield return null;
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            changeColour = false;
        }
    }
}