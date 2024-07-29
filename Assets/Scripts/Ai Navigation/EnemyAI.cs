using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float distance = 10;
    private NavMeshAgent agent;
    private Animator animator;
    private ParticleSystem particle;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    bool Lost = false;
    float time = 0;

    private void Update()
    {
        agent.destination = player.position;
        if (agent.remainingDistance <= distance)
        {
            agent.isStopped = false;
            if (Lost)
            {
                animator.SetTrigger("Found");
                Lost = false;
            }
        }
        else
        {
            agent.isStopped = true;
            Lost = true;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("Attack", true);
        }
        else animator.SetBool("Attack", false);

        if (!Lost)
        {
            time = 0;
            return;
        }

        time += Time.deltaTime;
        if (time < 5) return;
        time = 0;
        
        Vector3 result;
        if(RandomPoint(5, out result))
        {
            particle.transform.position = result;
            Invoke("Teleport", 3);
        }
    }
    bool RandomPoint(float range, out Vector3 pos)
    {

        Vector3 randomPoint = this.transform.position + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 5.0f, NavMesh.AllAreas))
        {
            pos = hit.position;
            return true;
        }

        pos = this.transform.position;
        return false;
    }

    void Teleport()
    {
        this.transform.position = particle.transform.position;
        particle.transform.position = this.transform.position;
    }
}
