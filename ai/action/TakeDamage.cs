using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace RPG
{
    public class TakeDamage : IState
    {
        float fleeSpeed;
        float fleeDistance;

        float cachedAgentSpeed;

        Animator animator;
        NavMeshAgent navMeshAgent;
        Transform transform;
        TargetDetector targetDetector;

        public TakeDamage(GameObject owner)
        {
            this.transform = owner.transform;
            this.animator = owner.GetComponent<Animator>();
            this.navMeshAgent = owner.GetComponent<NavMeshAgent>();
        }

        public void OnEnter()
        {
            navMeshAgent.enabled = false;
            cachedAgentSpeed = navMeshAgent.speed;
            navMeshAgent.speed = 0f;
        }

        public void Tick()
        {
        }

        public void OnExit()
        {
            navMeshAgent.enabled = false;
            navMeshAgent.speed = cachedAgentSpeed;
        }
    }

}

