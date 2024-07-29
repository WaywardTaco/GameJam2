using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

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
        particle = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    bool Lost = true;
    bool attack = false;
    float time = 0;

    public LayerMask layer;

    private void Update()
    {
        agent.destination = player.position;

        RaycastHit hit;
        Physics.Raycast(transform.position, player.position - this.transform.position, out hit, Vector3.Distance(this.transform.position, player.position), layer);
        if (hit.collider.gameObject.name == player.gameObject.name)
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

        if (!Lost && Vector3.Distance(this.transform.position, player.position) <= agent.stoppingDistance)
        {
            if (!attack)
            {
                animator.SetTrigger("Attack");
                attack = true;
            }
        }
        else
        {
            if (attack)
            {
                attack = false;
            }
        }

        if (!Lost)
        {
            time = 0;
            return;
        }

        time += Time.deltaTime;
        if (time < 6) return;
        time = 0;

        Vector3 pos;
        if (RandomPoint(30, out pos))
        {
            particle.transform.position = new Vector3(pos.x, pos.y + 1f, pos.z);
            particle.gameObject.SetActive(true);
            Invoke("Teleport", 4);
        }
    }
    bool RandomPoint(float range, out Vector3 pos)
    {
        Vector3 randomPoint;
        do
        {
            randomPoint = player.position + Random.insideUnitSphere * range;
        } while (Vector3.Distance(randomPoint, player.position) > 5);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 30f, NavMesh.AllAreas))
        {
            pos = hit.position;
            return true;
        }

        pos = this.transform.position;
        return false;
    }

    void Teleport()
    {
        Vector3 pos = particle.transform.position;
        agent.Warp(new Vector3(pos.x, pos.y - 1f, pos.z));
        particle.gameObject.SetActive(false);
    }
}
