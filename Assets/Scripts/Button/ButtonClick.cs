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
        // Tıklama durumunu tersine çevir
        isButtonClick = !isButtonClick;
        anim.SetBool("isButtonClick", isButtonClick);

        // Eğer tıklama durumu değiştiyse, bir event tetikleyebilirsiniz (bu event üretim kontrolünü sağlayacak başka bir sınıfa yönlendirilebilir)
        // Örnek olarak:
        if (isButtonClick)
        {
            breadSpawner.StartProduction(); // Üretimi başlatmak için GameManager sınıfındaki bir metodu çağırabilirsiniz.
        }
        else
        {
            breadSpawner.StopProduction(); // Üretimi durdurmak için GameManager sınıfındaki bir metodu çağırabilirsiniz.
        }
    }

}
