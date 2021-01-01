using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace RPG
{
    public class Flee : IState
    {
        float fleeSpeed;
        float fleeDistance;

        float cachedAgentSpeed;

        Animator animator;
        Movement movement;
        NavMeshAgent navMeshAgent;
        Transform transform;
        TargetDetector targetDetector;


        public Flee(GameObject owner, TargetDetector targetDetector, float fleeSpeed, float fleeDistance)
        {
            this.transform = owner.transform;
            this.animator = owner.GetComponent<Animator>();
            this.navMeshAgent = owner.GetComponent<NavMeshAgent>();
            this.targetDetector = targetDetector;
            this.fleeSpeed = fleeSpeed;
            this.fleeDistance = fleeDistance;
        }

        public void OnEnter()
        {
            navMeshAgent.enabled = true;
            cachedAgentSpeed = navMeshAgent.speed;
            navMeshAgent.speed = fleeSpeed;
        }

        public void Tick()
        {
            animator.SetFloat(HumanoidAnimatorConstants.VERTICAL, navMeshAgent.speed);
            
            if (navMeshAgent.remainingDistance < 1f)
            {
                var away = GetRandomPoint();
                navMeshAgent.SetDestination(away);
            }
        }

        private Vector3 GetRandomPoint()
        {
            var directionFromTarget = transform.position - targetDetector.GetNearestTargetPosition();
            directionFromTarget.Normalize();

            var endPoint = transform.position + (directionFromTarget * fleeDistance);
            if (NavMesh.SamplePosition(endPoint, out var hit, 10f, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return transform.position;
        }

        public void OnExit()
        {
            navMeshAgent.enabled = false;
            navMeshAgent.speed = cachedAgentSpeed;
        }
    }

}
