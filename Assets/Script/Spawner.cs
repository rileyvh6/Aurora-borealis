using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner spawner;
    [SerializeField] private float RandomRangeX, RandomRangeZ;
    [SerializeField] int amountOfPrefab, amountOfGoals;
    public delegate void FunctionMod<T>(T param1);
    public GameObject[] globalGoal { get; set;}
    int lastRecordedIndex;
    [SerializeField] GameObject prefabs;

    private void Start()
    {
        RandomRangeX /= 2;
        RandomRangeZ /= 2;
        spawner = this;
        globalGoal = new GameObject[amountOfGoals];
        for(int i = 0; i < globalGoal.Length; i++)
        {
            globalGoal[i] = new GameObject("globalGoal_" + i);
        }
        StartCoroutine("ChangePosEveryX", Random.Range(3f, 4f));
        for(int i = 0; i < amountOfPrefab; i++)
        {
            MovePosSetup((x) => {
              //  if(x.collider.gameObject.tag == "Ground")
               // {
                    GameObject obj = Instantiate(prefabs, x.point, this.transform.rotation);
                //}
            });
        }
    }

    public void MovePosSetup(FunctionMod<RaycastHit> function)
    {
        RaycastHit hit;
        Vector3 randomPos = new Vector3(Random.Range(RandomRangeX * -1, RandomRangeX), 100f, Random.Range(RandomRangeZ * -1, RandomRangeZ));
        randomPos -= this.transform.position;
        randomPos = new Vector3(randomPos.x * -1f, randomPos.y, randomPos.z * -1f);
        Ray ray = new Ray(randomPos, Vector3.down);
        if (Physics.Raycast(ray, out hit))
            function(hit);
    }

    IEnumerator ChangePosEveryX(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            MovePosSetup((x) => 
            {
                //int randomIndex;
                //do { randomIndex = Random.Range(0, globalGoal.Length); }
                //while (lastRecordedIndex == randomIndex);
                globalGoal[0].transform.position = x.point;

                //lastRecordedIndex = randomIndex;
            });
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(RandomRangeX * 2f, 100f, RandomRangeZ * 2f));
    }


}
