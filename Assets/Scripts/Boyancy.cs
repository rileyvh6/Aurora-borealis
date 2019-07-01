using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(Rigidbody))]
public class Boyancy : MonoBehaviour
{
    /// <summary>
    /// The boyancy points. If this is empty will default to returning the Center Of Mass
    /// </summary>
    [SerializeField]
    private Transform[] boyancyPoints = new Transform[0];
    private Rigidbody Rigidbody;

    public Vector3[] BoyancyPoints => (boyancyPoints.Length == 0) ? new Vector3[] { Rigidbody.worldCenterOfMass } : boyancyPoints.Select(o => o.position).ToArray();



    public void Awake()
    {
        AssignValues();
    }

    private void AssignValues()
    {
        if (Rigidbody == null)
            Rigidbody = GetComponent<Rigidbody>();
    }

    public void OnDrawGizmosSelected()
    {
        AssignValues();
        Gizmos.color = Color.cyan;
        for (int i = 0; i < BoyancyPoints.Length; i++)
            Gizmos.DrawSphere(BoyancyPoints[i], 0.05f);
    }
}
