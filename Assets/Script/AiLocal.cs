using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocal : MonoBehaviour
{
    NavMeshAgent nvAgent;
    int allocatedGoalIndex, lastRecorded;
    int AllocatedGoalIndex 
    {
        get { return allocatedGoalIndex; }
        set 
        {
            allocatedGoalIndex = value;
        }
    }

    private void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
        AllocatedGoalIndex = Random.Range(0, Spawner.spawner.globalGoal.Length);
    }

    private void Update()
    {
        nvAgent.SetDestination(Spawner.spawner.globalGoal[AllocatedGoalIndex].transform.position);
        if (nvAgent.remainingDistance <= 1f)
            Spawner.spawner.MovePosSetup((x) => { Spawner.spawner.globalGoal[AllocatedGoalIndex].transform.position = x.point; });
        if(Random.Range(0, 10000) < 10)
        {
            lastRecorded = allocatedGoalIndex;
            do { AllocatedGoalIndex = Random.Range(0, Spawner.spawner.globalGoal.Length); }
            while (AllocatedGoalIndex == lastRecorded);
        }
    }
}
