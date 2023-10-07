using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI bagStock;
    public TextMeshProUGUI factoryStock;
    public void UpdateBagStock(int currentStock)
    {
        bagStock.text = "Bag Stock : " + currentStock + " / 10";
    }
   
    public void UpdateFactoryStock()
    {
        factoryStock.text = "Factory Stock : " + GameManager.instance._breadSpawner.totalFactoryStock 
            + " / " + GameManager.instance._breadSpawner._conveyorsTotalStock; 
    }

}
