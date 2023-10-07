using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{
    public GameObject breadPrefab;
    [SerializeField] private int saleFee;
    public bool isCollectible;
    private BreadSpawner breadSpawner;

    private void Awake()
    {
        breadSpawner = GetComponentInParent<BreadSpawner>();
    }

    public void Collect(Transform bagPositions)
    {        
        Transform[] playerBag = bagPositions.GetComponent<PlayerMovement>().GetBagTransforms();
        int index = FindNextAvailableSlot(playerBag);
        if (index != -1)
        {
            transform.parent = playerBag[index];
            transform.position = playerBag[index].position;
            transform.rotation = playerBag[index].rotation;
            GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
        }
        breadSpawner.DeCreaseCurrentStock(1);
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

    private void OnTriggerEnter(Collider other)
    {
        if(!isCollectible && other.tag == "BreadStock")
        {
            StartCoroutine(WaitForCollect());
        }
    }

    IEnumerator WaitForCollect()
    {
        yield return new WaitForSeconds(2f);
        isCollectible = true;
        
    }

}
