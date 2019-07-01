using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ChangeLayerOnVelocity : MonoBehaviour
{
    public float Comparison = 5;
    public Layers LayerOnMoreThan;
    public Layers LayerOnLessThan;
    private Rigidbody Rigidbody;

    public void Start()
    {
        if (Rigidbody == null)
            Rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        Vector3 Velocity = Rigidbody.velocity;
        if (Velocity.x > Comparison || Velocity.y > Comparison || Velocity.z > Comparison)
        {
            gameObject.layer = (int)LayerOnMoreThan;
            return;
        }
        gameObject.layer = (int)LayerOnLessThan;
    }
}
