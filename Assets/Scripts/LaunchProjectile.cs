using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
//[RequireComponent(typeof(Rigidbody))]
public class LaunchProjectile : MonoBehaviour
{
    public GameObject Projectile;
    public float Force;
    public Transform FirePoint;

//    private Rigidbody Rigidbody;

    public void Start()
    {
//        if (Rigidbody == null)
//            Rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Fire();
    }

    public void fire() => Fire();

    public GameObject Fire()
    {
        GameObject obj = Instantiate(Projectile);
        Rigidbody rigidbody = obj.GetComponent<Rigidbody>();

        rigidbody.MovePosition(FirePoint.position);
        rigidbody.MoveRotation(FirePoint.rotation);
//        rigidbody.velocity += Rigidbody.velocity;

        rigidbody.AddForce(FirePoint.forward * Force);

        return obj;
    }
}
