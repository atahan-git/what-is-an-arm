using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    [SerializeField] private Transform enemyMoveTransform;

    private NavMeshAgent agent;
    public bool isAlive;

    private void Awake()
    {   
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(PlayerController.s.transform.position, transform.position) < 1.25) {
            GameMaster.s.EndGame(false);
        }
                
        agent.destination = enemyMoveTransform.position;
    }
}
