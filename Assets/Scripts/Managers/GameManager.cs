using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Water;
    public bool WaterActive;
    public bool FastRestart;
    public bool SafeTraining;

    private readonly List<float> CumulativeRewards = new List<float>();
    private readonly List<long> CollisionTimesteps = new List<long>();
    private readonly List<int> SuccessRate = new List<int>();

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }

    public void AddCumulativeReward(float reward)
    {
        CumulativeRewards.Add(reward);
    }
    public void AddCollisions(long collisions)
    {
        CollisionTimesteps.Add(collisions);
    }
    public void AddSuccess(int success)
    {
        SuccessRate.Add(success);
    }

    public void PrintResults()
    {
        Debug.Log("Number of Episodes: " + CumulativeRewards.Count);
        float meanCumulative = 0;
        float meanCollisions = 0;
        float successRate = 0;
        for (int index = 0; index < CumulativeRewards.Count; index++)
        {
            meanCumulative += (CumulativeRewards[index] / CumulativeRewards.Count);
            //meanCollisions += ((float)(CollisionTimesteps[index]) / CollisionTimesteps.Count);
            meanCollisions += (float)(CollisionTimesteps[index]);
            successRate += SuccessRate[index];
        }
        Debug.Log("CumulativeRewards: " + meanCumulative);
        Debug.Log("Collisions: " + meanCollisions);
        Debug.Log("Success Rate: " + successRate);
    }


}
