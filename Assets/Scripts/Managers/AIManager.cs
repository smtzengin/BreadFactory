using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [Header("AI Spawn Points")]
    public Transform[] spawnPoints;
    public Transform[] targetPoints;

    [Header("AI List")]
    public GameObject[] AIPrefabs;
    public List<AIController> controllerList;

    [Header("SpawnVariables")]
    public float spawnInterval = 10.0f;
    public Transform spawnParent;
    private float timer = 0.0f;
    public Transform AIExit;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            var rnd = Random.Range(0, 8);
            GameObject newAI = Instantiate(AIPrefabs[0], spawnPoints[rnd].position, Quaternion.identity, spawnParent);
            controllerList.Add(newAI.GetComponent<AIController>());          
            timer = 0.0f;
        }
    }
}
