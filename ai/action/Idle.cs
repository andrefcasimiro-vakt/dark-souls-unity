using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace RPG
{
    public class Idle : IState
    {
        float cachedAgentSpeed;

        Animator animator;
        Transform transform;
        NavMeshAgent navMeshAgent;

        public Idle(GameObject owner)
        {
            this.animator = owner.GetComponent<Animator>();
            this.transform = owner.transform;
            this.navMeshAgent = owner.GetComponent<NavMeshAgent>();
        }

        public void OnEnter()
        {
            navMeshAgent.enabled = true;
            cachedAgentSpeed = navMeshAgent.speed;
            navMeshAgent.speed = 0f;
            navMeshAgent.SetDestination(transform.position);
        }

        public void Tick()
        {
            animator.SetFloat(HumanoidAnimatorConstants.VERTICAL, navMeshAgent.speed);
        }

        public void OnExit()
        {
            navMeshAgent.enabled = false;
            navMeshAgent.speed = cachedAgentSpeed;
        }
    }
}

