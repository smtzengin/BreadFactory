using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    public BreadSpawnerManager _breadSpawner;
    public UIManager _uiManager;
    public static GameManager instance;

    [Header("Game Variables")]
    [SerializeField] private int _gold;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else DontDestroyOnLoad(instance);        
    }

    private void Start()
    {
        _uiManager.UpdateFactoryStock();
    }
}
