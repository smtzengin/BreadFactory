
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Table : MonoBehaviour
{    
    [SerializeField] private Transform slotsParent;
    [SerializeField] private Transform[] slotsTransform;
    [SerializeField] private GameObject placementArea;
    private int breadCount = 0;
    

    private void Start()
    {
        slotsTransform = new Transform[slotsParent.childCount];

        for (int i = 0; i < slotsParent.childCount; i++)
        {
            slotsTransform[i] = slotsParent.GetChild(i).transform;
        }
    }

    public int GetBreadCount()
    {
        return breadCount;
    }

    public int PlaceBreadsFromCharacter(Transform[] characterBag)
    {
        int placedBreadCount = 0;

        for (int i = 0; i < slotsTransform.Length; i++)
        {
            
            if (i < characterBag.Length && characterBag[i].childCount > 0 && slotsTransform[i].childCount == 0)
            {
                Transform breadToPlace = characterBag[i].GetChild(0); 
                Transform slotTransform = slotsTransform[i];                 
                breadToPlace.DOMove(slotTransform.position, 1f); 
                breadToPlace.DORotateQuaternion(slotTransform.rotation, 1f); 
                breadToPlace.SetParent(slotTransform); 

                placedBreadCount++;

                breadCount++;
            }
        }

        return placedBreadCount; 
    }


    public int GiveTheBreadsFromTable(int breadCount)
    {
        for(int i = 0; i < breadCount; i++)
        {
            Destroy(slotsTransform[i].GetChild(0).gameObject);
            breadCount--;
        }
        GameManager.instance._uiManager.DeCreaseFactoryStock(breadCount);       

        ReplaceBreads();

        print(breadCount + " kadat ekmek alındı.");        

        return breadCount;
    }

    private void ReplaceBreads()
    {
        List<Transform> emptySlots = new List<Transform>();
        List<Transform> filledSlots = new List<Transform>();
        
        for (int i = 0; i < slotsTransform.Length; i++)
        {
            if (slotsTransform[i].childCount == 0)
            {
                emptySlots.Add(slotsTransform[i]);
            }
            else
            {
                filledSlots.Add(slotsTransform[i]);
            }
        }

        int emptySlotIndex = 0;
        int filledSlotIndex = 0;
        
        while (emptySlotIndex < emptySlots.Count && filledSlotIndex < filledSlots.Count)
        {
            Transform emptySlot = emptySlots[emptySlotIndex];
            Transform filledSlot = filledSlots[filledSlotIndex];

            Transform breadToMove = filledSlot.GetChild(0);

            breadToMove.DOMove(emptySlot.position, 1f);
            breadToMove.DORotateQuaternion(emptySlot.rotation, 1f);
            breadToMove.SetParent(emptySlot);

            emptySlotIndex++;
            filledSlotIndex++;
        }
    }



}
