using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace RPG
{
    public class GoTo : IState
    {
        float cachedAgentSpeed;

        Animator animator;
        Transform transform;
        Transform target;
        NavMeshAgent navMeshAgent;

        float moveSpeed;

        public GoTo(GameObject owner, Transform target, float moveSpeed)
        {
            this.animator = owner.GetComponent<Animator>();
            this.transform = owner.transform;
            this.navMeshAgent = owner.GetComponent<NavMeshAgent>();
            this.target = target;
            this.moveSpeed = moveSpeed;
        }

        public void OnEnter()
        {
            navMeshAgent.enabled = true;
            cachedAgentSpeed = navMeshAgent.speed;
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.SetDestination(target.position);
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

