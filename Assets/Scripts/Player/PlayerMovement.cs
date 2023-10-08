
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Camera mainCamera;
    public float speed = 5.0f;
    public float distanceFromPlayer = 2.0f;
    public float rotationSpeed = 5f;
    public Animator anim;
    public Transform _bagParent;
    public Transform[] _bag;
    [SerializeField] private int _bagCapacity = 10;

    [SerializeField] private int currentBagStock = 0;
    public int GetCurrentBagStock() { return currentBagStock; }
    

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;      
        anim = GetComponent<Animator>();
        SetBagSlots();      
    }

    private void Update()
    {        
        DrawRaycastAndCollectBread();        
    }

    private void DrawRaycastAndCollectBread()
    {
        if (currentBagStock < _bagCapacity)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.CompareTag("Bread") && Input.GetMouseButtonDown(0))
                {                    
                    Bread bread = hit.collider.GetComponent<Bread>();
                    if (bread != null && bread.isCollectible)
                    {                       
                       bread.Collect(transform);
                       currentBagStock++;
                       GameManager.instance._uiManager.UpdateBagStock(currentBagStock);                                               
                       hit.rigidbody.freezeRotation = true;
                                             
                    }
                }
            }
        }        
    }

    
    public Transform[] GetBagTransforms()
    {
        return _bag;
    }

    private void SetBagSlots()
    {
        _bag = new Transform[_bagCapacity];
        for (int i = 0; i < _bagCapacity; i++)
        {
            GameObject emptyGameObject = new GameObject("EmptySlot" + i);
            emptyGameObject.transform.parent = _bagParent;
            emptyGameObject.transform.localPosition = new Vector3(0f, 0.1f * i, 0f);
            _bag[i] = emptyGameObject.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("placementArea"))
        {
            print("şu an placementArea'dayım" + other.gameObject.name);
            Table table = other.GetComponentInParent<Table>();
            if (table != null)
            {           
                print("masa bulundu" + table.name);
                int placedBreadCount = table.PlaceBreadsFromCharacter(_bag);

                currentBagStock -= placedBreadCount;
                GameManager.instance._uiManager.UpdateBagStock(currentBagStock);
            }
        }
    }

    public void Move(float horizontal, float vertical)
    {
        // Kameranın bakış yönünü al
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize(); 

        // Hareketi kameranın bakış açısına göre ayarla
        Vector3 moveDirection = cameraForward * vertical + mainCamera.transform.right * horizontal;

        // Karakterin rotasyonunu kameraya göre ayarla.
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }        
        controller.SimpleMove(moveDirection * speed);
    }
}
