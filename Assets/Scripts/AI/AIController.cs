using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    private Animator animator;
    [SerializeField] private Transform AIExit;

    private bool isWaiting = false;
    private bool isQueueing = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        var rand = Random.Range(0, 9);
        target = GameManager.instance._aiManager.targetPoints[rand];

        AIExit = GameObject.FindGameObjectWithTag("AIExit").transform;
       
    }

    private void Update()
    {
        if (isWaiting)
        {           
            agent.velocity = Vector3.zero;
            animator.SetBool("isWalking", false);
        }
        else if (isQueueing)
        {            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f))
            {                
                isWaiting = true;
            }
            else
            {                
                isWaiting = false;
                agent.SetDestination(target.position);
                animator.SetBool("isWalking", true);
            }
        }
        else
        {            
            if (target != null)
                agent.SetDestination(target.position);

            if (agent.velocity.magnitude > 0.1f)
                animator.SetBool("isWalking", true);
            else animator.SetBool("isWalking", false);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AIArea"))
        {
            var reachedTable = other.GetComponentInParent<Table>();
            if (reachedTable != null)
            {
                StartCoroutine(WaitForBreads(reachedTable));
            }
        }
    }

    IEnumerator WaitForBreads(Table table)
    {
        isQueueing = true;
        var rnd = Random.Range(1, 5);
        while (table.GetBreadCount() < rnd)
        {           
            yield return null;
        }
        isQueueing = false;        
        table.GiveTheBreadsFromTable(rnd);
        GameManager.instance.GiveGoldToAI(rnd * 10);
        target = AIExit;
    }
}
