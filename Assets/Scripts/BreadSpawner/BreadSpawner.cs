using System.Collections;
using UnityEngine;
using DG.Tweening;


public class BreadSpawner : MonoBehaviour
{
    [SerializeField] private Transform _breadSpawnPoint;
    [SerializeField] private Transform _breadStockPoint;

    [SerializeField] private float spawnInterval = 5.0f; // Spawn aralığı
    [SerializeField] public int numberOfStocks;
    [SerializeField] private int currentStock;

    [SerializeField] private Transform pathParent;
    [SerializeField] private PathType pathType;
    [SerializeField] private Vector3[] pathArray;

    [SerializeField] private Bread bread;
    [SerializeField] private Transform breadStock;
    [SerializeField] private ButtonClick startButton;

    private bool isProductionActive;

    public int SetCurrentStock(int value)
    {
        currentStock = value;
        return currentStock;
    }
    public int GetCurrentStock() { return currentStock; }

    public static BreadSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {               
        isProductionActive = false;        
    }
    private void Update()
    {
        if (currentStock >= numberOfStocks) StopProduction();
    }

    void SpawnBread()
    {        
        GameObject newBread = Instantiate(bread.breadPrefab, _breadSpawnPoint.position, Quaternion.identity,breadStock);
        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = pathParent.GetChild(i).position;
        }

        Tween t = newBread.transform.DOPath(pathArray, 3f, pathType).SetLookAt(0.001f);
        t.ForceInit();
        UpdateCurrentStock();
        GameManager.instance._breadSpawner.totalFactoryStock++;
        GameManager.instance._uiManager.UpdateFactoryStock();
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

    public void UpdateCurrentStock()
    {
        SetCurrentStock(breadStock.transform.childCount);
    }

    public void DeCreaseCurrentStock(int value)
    {
        int newCurrentStock = currentStock - value;
        SetCurrentStock(newCurrentStock);
    }

    public void StartProduction()
    {
        isProductionActive = true;
        UpdateCurrentStock();
        StartCoroutine(SpawnPeriodically());
        

    }
    public void StopProduction()
    {
        isProductionActive = false;
        UpdateCurrentStock();
        StopCoroutine(SpawnPeriodically());
    }

}
