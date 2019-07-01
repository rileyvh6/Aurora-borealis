using UnityEngine;
using System.Linq;
[DisallowMultipleComponent,RequireComponent(typeof(Collider))]
public class FloatObjects : MonoBehaviour
{
    //    public Collider Collider;
    public float bounceDamp = 0.1f;
    public float DragRotation = 0.97f;
    /// <summary>
    /// Whether the water level will be found via the nearest water mesh (more expensive but if object this script is attatched to isn't at the same level as the water, then this will be correct) or via the currently attatched GameObject.
    /// </summary>
    public bool UseWaterMeshPositionForWaterLevel;
    public float MinRandomBounce = 0.01f;
    public float MaxRandomBounce = 0.1f;

    public void OnTriggerStay(Collider other)
    {
        Rigidbody rigid = other.GetComponent<Rigidbody>();
        if (rigid == null || rigid.gameObject.isStatic)
            return;
        Vector3[] boyancyPoints;
        Boyancy boyancy = rigid.GetComponent<Boyancy>();
        switch (boyancy)
        {
            case null:
                boyancyPoints = new Vector3[] { rigid.worldCenterOfMass };
                break;
            default:
                boyancyPoints = boyancy.BoyancyPoints;
                break;
        }
        float waterLevel = transform.position.y;
        switch (UseWaterMeshPositionForWaterLevel)
        {
            case true:
                Vector3 actionPoint = boyancyPoints[0];
                Collider[] nearbyColliders = new Collider[4];
                Physics.OverlapSphereNonAlloc(actionPoint, 2, nearbyColliders, LayerMask.NameToLayer("Water"), QueryTriggerInteraction.Collide);
                Transform nearestWaterCollider = null;
                float nearestColliderDistSqr = Mathf.Infinity;
                for (int e = 0; e < nearbyColliders.Length; e++)
                {
                    Transform target = nearbyColliders[e].transform;
                    Vector3 directionToTarget = target.position - actionPoint;
                    float dSqrToTarget = directionToTarget.sqrMagnitude;
                    if (dSqrToTarget < nearestColliderDistSqr)
                    {
                        nearestColliderDistSqr = dSqrToTarget;
                        nearestWaterCollider = target;
                    }
                }
                waterLevel = nearestWaterCollider.position.y;
                break;
        }

        float BobForce = Random.Range(MinRandomBounce, MaxRandomBounce);
        for (int i = 0, boyancyPointsLength = boyancyPoints.Length; i < boyancyPointsLength; i++)
        {
            Vector3 actionPoint = boyancyPoints[i];
            float forceFactor = 1f - (actionPoint.y - waterLevel);
            //            Debug.Log(forceFactor);
            forceFactor += BobForce;
            Debug.Log(forceFactor);
            if (forceFactor > 0)
            {
                float upliftMultiplier = forceFactor - (rigid.velocity.y * bounceDamp);
                Vector3 uplift = -Physics.gravity * upliftMultiplier;
                rigid.AddForceAtPosition(uplift, actionPoint);
                rigid.angularVelocity *= DragRotation;
            }
        }
    }
}
