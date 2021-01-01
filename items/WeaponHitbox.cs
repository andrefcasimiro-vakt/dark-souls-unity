using UnityEngine;
using System.Collections;

namespace RPG
{

    public class WeaponHitbox : MonoBehaviour
    {
        public Weapon weapon;

        private void Start()
        {
            Disable();
        }

        public void Enable()
        {
            GetComponent<Collider>().enabled = true;
        }

        public void Disable()
        {
            GetComponent<Collider>().enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Hitbox") return;

            Health otherHealth = other.gameObject.GetComponent<Hitbox>().actorHealth;
            otherHealth.TakeDamage(weapon.physicalDamage);
        }

    }

}
