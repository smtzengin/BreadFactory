
using UnityEngine;
using DG.Tweening;

public class Table : MonoBehaviour
{    
    [SerializeField] private Transform slotsParent;
    [SerializeField] private Transform[] slotsTransform;
    [SerializeField] private GameObject placementArea;

    private void Start()
    {
        slotsTransform = new Transform[slotsParent.childCount];

        for (int i = 0; i < slotsParent.childCount; i++)
        {
            slotsTransform[i] = slotsParent.GetChild(i).transform;
        }
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
            }
        }

        return placedBreadCount; 
    }


}
