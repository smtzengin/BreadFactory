using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{
    public GameObject breadPrefab;
    [SerializeField] private int saleFee;
    public bool isCollectible;
    
    public void Collect(Transform bagPositions)
    {      
        if (isCollectible)
        {
            isCollectible = false;
            Transform[] playerBag = bagPositions.GetComponent<PlayerMovement>().GetBagTransforms();
            int index = FindNextAvailableSlot(playerBag);
            if (index != -1)
            {
                transform.parent = playerBag[index];
                transform.localPosition = Vector3.zero;
            }
        }
    }

    private int FindNextAvailableSlot(Transform[] bagTransforms)
    {
        for (int i = 0; i < bagTransforms.Length; i++)
        {
            if (bagTransforms[i].childCount == 0)
            {
                return i;
            }
        }
        return -1;
    }

  

}
