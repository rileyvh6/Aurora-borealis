using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
public class RippleInstance : MonoBehaviour
{
    public float power = 1;
    private Renderer Renderer;

    private const float DefaultSpeed = 0.1f;
    private const float DefaultStrength = 1;
    private const float DefaultNoiseStrength = 10;
    private const float DefaultTransparency = 0;

    // Start is called before the first frame update
    void Start()
    {
        AssignValues();
    }

    void AssignValues()
    {
        if (Renderer == null)
            Renderer = GetComponent<Renderer>();
    }

    private void Reset()
    {
        AssignValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
