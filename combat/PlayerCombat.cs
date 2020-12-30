using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{

    [RequireComponent(typeof(WeaponManager))]
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Combat Status")]
        public bool canAttack = true;

        public WeaponManager weaponManager;
        public Animator animator;

        // Constants
        [Header("Animator Clip Names")]
        public string ANIMATOR_ATTACK_CLIP_NAME = "Attack";

        private void Start()
        {
            weaponManager.OnWeaponEquipped += OnWeaponEquipped;
        }

        public void Update()
        {
            canAttack = (
                animator.GetBool(HumanoidAnimatorConstants.IS_FALLING) == false
            );

            if (!canAttack) return;

            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }
        }

        void Attack()
        {
            // Mirror animations
            if (weaponManager.IsLeftHanding() && weaponManager.LeftWeapon.usesRightHandAnimations)
            {
                animator.SetBool(HumanoidAnimatorConstants.MIRROR, true);
            }
            else
            {
                animator.SetBool(HumanoidAnimatorConstants.MIRROR, false);
            }

            animator.CrossFade(ANIMATOR_ATTACK_CLIP_NAME, .1f);
        }

        void OnWeaponEquipped(Weapon equippedWeapon)
        {
            // Debug.Log("Weapon manager is aware that a weapon was equipped");
            // Debug.Log("Equipped Weapon: " + equippedWeapon.name);
        }

    }

}
