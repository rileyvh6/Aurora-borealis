using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRoation : MonoBehaviour
{
 
    public float speed;
    private float speedincrese;
private Transform Transform;
    void Start()
    {
        //StartCoroutine();
        Transform = transform;
    }

    void Update()
    {
        Transform.Rotate(0, speed * Time.deltaTime,0); 
    }


}
