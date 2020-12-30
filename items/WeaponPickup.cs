using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [RequireComponent(typeof(SphereCollider))]
    public class WeaponPickup : MonoBehaviour
    {
        public Weapon weapon;
        public string[] tags;

        private void OnTriggerEnter(Collider other)
        {
            foreach(string tag in tags)
            {
                if (tag == other.gameObject.tag)
                {
                    Pickup(other.gameObject);
                    break;
                }
            }
        }

        public void Pickup(GameObject target)
        {
            WeaponManager targetWeaponManager = target.GetComponent<WeaponManager>();
            if (targetWeaponManager == null)
            {
                Debug.Log("Target does not have a Weapon Manager attached. Weapon {" + weapon.name + "} could not be picked up by gameObject {" + target.gameObject.name +"}");
                return;
            }

            targetWeaponManager.RightWeapon = weapon;

            Destroy(this.gameObject);
        }

    }

}
