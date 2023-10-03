using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class BreadSpawner : MonoBehaviour
{
    [SerializeField] private Transform _breadSpawnPoint;
    [SerializeField] private Transform _breadStockPoint;

    [SerializeField] private float spawnInterval = 5.0f; // Spawn aralığı
    [SerializeField] private int numberOfStocks;
    [SerializeField] private int currentStock;
    [SerializeField] private int totalSpawnItems;

    [SerializeField] private Transform pathParent;
    [SerializeField] private PathType pathType;
    [SerializeField] private Vector3[] pathArray;

    [SerializeField] private Bread bread;
    [SerializeField] private ButtonClick startButton;

    private bool isProductionActive;

    public static BreadSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {               
        isProductionActive = false;
        
    }


    void SpawnBread()
    {
        GameObject newBread = Instantiate(bread.breadPrefab, _breadSpawnPoint.position, Quaternion.identity);

        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = pathParent.GetChild(i).position;
        }

        Tween t = newBread.transform.DOPath(pathArray, 3f, pathType).SetLookAt(0.001f);
        t.ForceInit();
        currentStock++;
    }

    IEnumerator SpawnPeriodically()
    {
        while (currentStock < numberOfStocks)
        {
            if (isProductionActive && currentStock < numberOfStocks)
            {
                SpawnBread();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void StartProduction()
    {
        isProductionActive = true;
        StartCoroutine(SpawnPeriodically());
    }
    public void StopProduction()
    {
        isProductionActive = false;
        StopCoroutine(SpawnPeriodically());
    }

}
