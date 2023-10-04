using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour
{
    public Animator anim;
    public bool isButtonClick;

    private BreadSpawner breadSpawner;
    

    private void Start()
    {
        anim = GetComponent<Animator>(); 
        breadSpawner = GetComponentInParent<BreadSpawner>();
    }

    private void OnMouseDown()
    {        
        isButtonClick = !isButtonClick;
        anim.SetBool("isButtonClick", isButtonClick);
        
        if (isButtonClick)
        {
            breadSpawner.StartProduction();
        }
        else
        {
            breadSpawner.StopProduction(); 
        }
    }

}
