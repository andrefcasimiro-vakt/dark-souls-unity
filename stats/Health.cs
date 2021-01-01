using UnityEngine;
using System.Collections;

namespace RPG
{

    [RequireComponent(typeof(Actor))]
    public class Health : MonoBehaviour
    {
        Animator animator => GetComponent<Animator>();
        Actor actor => GetComponent<Actor>();

        public float currentAmount;
        public float maxAmount;

        public void TakeDamage(float damageAmount)
        {
            float newHealthAmount = currentAmount - damageAmount;
            currentAmount = newHealthAmount <= 0 ? 0 : newHealthAmount;

            if (currentAmount == 0)
            {
                animator.CrossFade(HumanoidAnimatorConstants.DYING_CLIP_NAME, .1f);
            }
            else
            {
                animator.CrossFade(HumanoidAnimatorConstants.TAKING_DAMAGE_CLIP_NAME, .1f);
            }
        }
    }

}