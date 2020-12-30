using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{

    [RequireComponent(typeof (Animator))]
    public class WeaponManager : MonoBehaviour
    {
        [Header("Transforms")]
        public Transform leftHand;
        public Transform rightHand;

        [Header("Starting Weapons")]
        public Weapon startingLeftWeapon;
        public Weapon startingRightWeapon;

        [Header("Current Weapons")]
        [SerializeField]
        private Weapon leftWeapon;
        [SerializeField]
        private Weapon rightWeapon;

        // The instatiated prefabs of each weapon
        private GameObject leftWeaponInstance;
        private GameObject rightWeaponInstance;

        public Weapon RightWeapon
        {
            get
            {
                return rightWeapon;
            }

            set
            {
                rightWeapon = value;

                HandleWeaponChange(HandEnum.RIGHT_HAND, value);
                OnWeaponEquipped?.Invoke(value);
            }
        }

        public Weapon LeftWeapon
        {
            get
            {
                return leftWeapon;
            }

            set
            {
                leftWeapon = value;

                HandleWeaponChange(HandEnum.LEFT_HAND, value);
                OnWeaponEquipped?.Invoke(value);
            }
        }

        [Header("Animators")]
        public RuntimeAnimatorController dualWieldingAnimatorRuntimeController;
        RuntimeAnimatorController defaultAnimatorRuntimeController;


        private void Awake()
        {
            // Cache the default animator because we will override this component with the weapon's override controllers
            defaultAnimatorRuntimeController = GetComponent<Animator>().runtimeAnimatorController;
        }

        private void Start()
        {
            if (startingLeftWeapon)
            {
                LeftWeapon = startingLeftWeapon;
            }

            if (startingRightWeapon)
            {
                RightWeapon = startingRightWeapon;
            }
        }


        void HandleWeaponChange(HandEnum targetHandEnum, Weapon weapon)
        {
            InstantiateWeapon(targetHandEnum, weapon);

            HandleAnimator();
        }

        void InstantiateWeapon(HandEnum targetHandEnum, Weapon weapon)
        {
            bool isLeftHand = targetHandEnum == HandEnum.LEFT_HAND;
            Transform targetHand = isLeftHand ? leftHand : rightHand;

            if (isLeftHand && leftWeaponInstance != null)
            {
                Destroy(leftWeaponInstance.gameObject);
                leftWeaponInstance = null;
            }
            else if (rightWeaponInstance != null)
            {
                Destroy(rightWeaponInstance.gameObject);
                rightWeaponInstance = null;
            }

            GameObject weaponInstanceTemp = Instantiate(weapon.prefab, targetHand);
            
            if (isLeftHand)
            {
                leftWeaponInstance = weaponInstanceTemp;
            }
            else
            {
                rightWeaponInstance = weaponInstanceTemp;
            }
        }


        void HandleAnimator()
        {
            if (leftWeapon != null && rightWeapon != null) // Dual Wielding Controller
            {
                GetComponent<Animator>().runtimeAnimatorController = dualWieldingAnimatorRuntimeController;
            }
            else if (leftWeapon != null && rightWeapon == null) // Left Hand Controller
            {
                GetComponent<Animator>().runtimeAnimatorController = leftWeapon.animator;
            }
            else if (leftWeapon == null && rightWeapon != null) // Right Hand Controller
            {
                GetComponent<Animator>().runtimeAnimatorController = rightWeapon.animator;
            }
            else // Unarmed / Default Controller
            {
                GetComponent<Animator>().runtimeAnimatorController = defaultAnimatorRuntimeController;
            }
        }

        public bool IsDualWielding()
        {
            return leftWeapon != null && rightWeapon != null;
        }

        public bool IsLeftHanding()
        {
            return leftWeapon != null && rightWeapon == null;
        }

        public bool IsRightHanding()
        {
            return rightWeapon != null && leftWeapon == null;
        }

        public delegate void OnWeaponEquippedDelegate(Weapon equippedWeapon);
        public event OnWeaponEquippedDelegate OnWeaponEquipped;
    }

}
