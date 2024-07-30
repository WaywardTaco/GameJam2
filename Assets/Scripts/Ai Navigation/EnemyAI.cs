using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float attackDis = 2;
    private NavMeshAgent agent;
    private Animator animator;
    private ParticleSystem particle;
    private GameObject rig;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDis;
        animator = GetComponent<Animator>();
        particle = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        rig = gameObject.transform.GetChild(1).gameObject;
    }

    bool attack = false;
    float time = 0;
    int action = 0;

    public LayerMask layer;

    private void Update()
    {
        animator.SetInteger("State", action);

        switch (action)
        {
            case 0: Idle();
                break;
            case 1: Run();
                break;
            case 2: Attack();
                break;
            case 3: Teleport();
                break;
        }

        time += Time.deltaTime;
        if (time < 15) return;
        time = 0;
        action = 3;
    }

    void Idle()
    {
        agent.isStopped = true;
        
        RaycastHit hit;
        Physics.Raycast(transform.position, player.position - this.transform.position, out hit, Vector3.Distance(this.transform.position, player.position), layer);
        if (hit.collider.gameObject.name == player.gameObject.name)
        {
            action = 1;
        }
    }

    void Run()
    {
        agent.isStopped = false;
        agent.destination = player.position;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            action = 2;
        }
    }

    void Attack()
    {
        this.transform.LookAt(player);
        agent.isStopped = true;
        if(!attack) Invoke("Attacked", 1);

        attack = true;
    }

    void Attacked()
    {
        action = 1;
        attack = false;

        if(Vector3.Distance(this.transform.position, player.position) <= attackDis)
        {
            EventBroadcaster.Instance.PostEvent("GAME_LOSE");
        }
    }

    void Teleport()
    {
        Vector3 pos;
        if (RandomPoint(40, out pos))
        {
            agent.isStopped = true;
            rig.SetActive(false);
            particle.transform.position = new Vector3(pos.x, pos.y + 1f, pos.z);
            particle.gameObject.SetActive(true);

            Invoke("Teleported", 5);
            action = 4;
        }
    }

    void Teleported()
    {
        agent.isStopped = false;
        rig.SetActive(true);
        particle.gameObject.SetActive(false);

        Vector3 pos = particle.transform.position;
        agent.Warp(new Vector3(pos.x, pos.y - 1f, pos.z));
        particle.transform.position = this.transform.position;

        action = 0;
    }

    bool RandomPoint(float range, out Vector3 pos)
    {
        Vector3 randomPoint;
        do
        {
            randomPoint = player.position + Random.insideUnitSphere * range;
        } while (Vector3.Distance(randomPoint, player.position) > 15);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 30f, NavMesh.AllAreas))
        {
            pos = hit.position;
            return true;
        }

        pos = this.transform.position;
        return false;
    }
}
