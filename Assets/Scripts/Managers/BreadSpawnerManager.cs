using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadSpawnerManager : MonoBehaviour
{
    [SerializeField] private BreadSpawner[] _conveyors;
    public int totalFactoryStock;
    public int _conveyorsTotalStock;

    private void Awake()
    {
        for(int i = 0 ; i < _conveyors.Length; i++)
        {
            _conveyorsTotalStock += _conveyors[i].numberOfStocks;
        }
    }



}
