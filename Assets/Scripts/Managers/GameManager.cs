using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    public BreadSpawnerManager _breadSpawner;
    public UIManager _uiManager;
    public AIManager _aiManager;
    public static GameManager instance;

    [Header("Game Variables")]
    [SerializeField] private int _gold;

    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;            
            _uiManager.UpdateGoldUI(_gold);
        }
    }


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

    public void GiveGoldToAI(int amount)
    {
        Gold += amount;
    }


}
