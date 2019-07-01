using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SplashOnTrigger : MonoBehaviour
{
    /// <summary>
    /// On collision, if the other object has one of these tags, it will not create a splash. Useful for particle effects or something of its ilk.
    /// </summary>
    public string[] IgnoreTags;
    public ParticleSystem SplashSystem;
    public float MinCooldown;

    private Vector3 SplashSystemDefaultScale;
    /// <summary>
    /// The last position and cooldown of each object that is currently beleived to be in the trigger (Because I'm not actively checking, this may not be accurate if something teleports out of the trigger, but this is fine) The first Three values are the position, the W value is the time since it has last started a splash because of the object.
    /// </summary>
    private Dictionary<Transform, Vector4> LastPositionAndCooldown = new Dictionary<Transform, Vector4>();
    // Start is called before the first frame update
    void Start()
    {
        SplashSystemDefaultScale = SplashSystem.transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!LastPositionAndCooldown.ContainsKey(other.transform))
            LastPositionAndCooldown.Add(other.transform, other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (LastPositionAndCooldown.ContainsKey(other.transform))
            LastPositionAndCooldown.Remove(other.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        StartCoroutine(SplashEffect(other.transform));

        Vector4 PosAndCool = LastPositionAndCooldown[other.transform];
        float newtime = PosAndCool.w + Time.deltaTime;
        PosAndCool = other.transform.position;
        PosAndCool.w = newtime;
        LastPositionAndCooldown[other.transform] = PosAndCool;
    }

    IEnumerator SplashEffect(Transform other)
    {
        Transform oTrans = other.transform;
        if (other.gameObject.isStatic || IgnoreTags.Contains(other.gameObject.tag) || LastPositionAndCooldown[oTrans].w < MinCooldown)
            yield break;

        Vector4 LastPosAndCool = LastPositionAndCooldown[oTrans];

//        if ()

        SplashSystem.transform.position = oTrans.position;
        SplashSystem.transform.localScale += oTrans.lossyScale / 2;
        Rigidbody oRigid = other.GetComponent<Rigidbody>();
        if (oRigid)
            SplashSystem.transform.localScale += oRigid.velocity;
        SplashSystem.Play();
        yield return null;
        SplashSystem.transform.localScale = SplashSystemDefaultScale;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
