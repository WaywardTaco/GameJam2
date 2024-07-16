using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] public Transform player;
    private NavMeshAgent enemy;

    private void Start()
    {       
        enemy = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (enemy != null) enemy.destination = player.position;
    }
}
