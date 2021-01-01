using UnityEngine;
using System;
using System.Collections;

namespace RPG {

    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(TargetDetector))]
    [RequireComponent(typeof(WeaponManager))]
    public class Actor : MonoBehaviour
    {
        [Header("General Information")]
        public string name;

        [Header("States")]
        public string currentState;
        [HideInInspector]
        public StateMachine stateMachine;

        [Header("Movement Settings")]
        public float moveSpeed = 6f;

        [Header("Flee Settings")]
        public bool enableFlee = true;
        public float fleeSpeed = 6f;
        public float fleeDistance = 5f;
        public string[] fleeTags;

        Animator animator => GetComponent<Animator>();
        Rigidbody rigidBody => GetComponent<Rigidbody>();

        Health health => GetComponent<Health>();
        TargetDetector targetDetector => GetComponent<TargetDetector>();
        WeaponManager weaponManager => GetComponent<WeaponManager>();

        GoTo goToWeapon;
        Idle idle;
        Flee flee;
        TakeDamage takeDamage;

        private void Awake()
        {
            rigidBody.freezeRotation = true;

            stateMachine = new StateMachine();

            goToWeapon = new GoTo(this.gameObject, FindNearestWeapon(), moveSpeed);
            idle = new Idle(this.gameObject);
            flee = new Flee(this.gameObject, targetDetector, fleeSpeed, fleeDistance);
            takeDamage = new TakeDamage(this.gameObject);

            // Highest priority first

            // Die
            stateMachine.AddAnyTransition(takeDamage, () => ShouldDie() == true);

            // Take Damage
            stateMachine.AddTransition(takeDamage, idle, () => ShouldTakeDamage() == false); // If took damage return to Idle
            stateMachine.AddAnyTransition(takeDamage, () => ShouldTakeDamage() == true);

            // Search for weapon if has none
            stateMachine.AddAnyTransition(goToWeapon, () => ShouldFindWeapon() == true);
            stateMachine.AddTransition(goToWeapon, idle, () => ShouldFindWeapon() == false); // If finds weapon, go to Idle

            // When target is lost, go to idle
            stateMachine.AddAnyTransition(idle, () => targetDetector.TargetInRange == false);

            // Should flee
            stateMachine.AddAnyTransition(flee, () => ShouldFlee() == true);
        }

        private void Update()
        {
            currentState = stateMachine?.currentState?.GetType().Name;
            stateMachine.Tick();
        }

        private bool ShouldDie()
        {
            return animator.GetBool(HumanoidAnimatorConstants.IS_DEAD) == true;
        }

        private bool ShouldTakeDamage()
        {
            return animator.GetBool(HumanoidAnimatorConstants.IS_TAKING_DAMAGE) == true;
        }

        private Transform FindNearestWeapon()
        {
            GameObject nearestWeapon = FindNearestObject<WeaponPickup>.Find(transform);

            return nearestWeapon?.transform;
        }

        private bool ShouldFindWeapon()
        {
            if (FindNearestWeapon() == null)
            {
                return false;
            }

            if (weaponManager.RightWeapon != null)
            {
                return false;
            }

            return true;
        }

        private bool ShouldFlee()
        {
            if (enableFlee == false)
            {
                return false;
            }

            bool isLowHealth = health.currentAmount <= ((health.maxAmount * 30) / 100);
            bool targetInRange = targetDetector.TargetInRange == true;

            if (isLowHealth && targetInRange)
            {
                return true;
            }

            return false;
        }
    }

}
