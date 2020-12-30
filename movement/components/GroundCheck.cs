using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class GroundCheck
    {
        const float GROUND_RAYCAST_OFFSET = 0.5f;
        float minimumDistanceToGround;
        public LayerMask groundLayers;

        Animator animator;
        Transform transform;

        float longFallThreshold;

        public bool isGrounded = false;
        private Vector3 lastGroundedPosition;


        public GroundCheck(GameObject target, float minimumDistanceToGround, float longFallThreshold, LayerMask groundLayers)
        {
            this.transform = target.transform;
            this.animator = target.GetComponent<Animator>();
            this.minimumDistanceToGround = minimumDistanceToGround;
            this.longFallThreshold = longFallThreshold;
            this.groundLayers = groundLayers;
        }

        public void Execute()
        {
            bool nextGrounded = IsGrounded();

            if (isGrounded != nextGrounded)
            {
                isGrounded = nextGrounded;

                if (isGrounded == false)
                {
                    // Record last position where player was grounded
                    lastGroundedPosition = transform.position;
                }
                else
                {
                    float fallDistance = Vector3.Distance(transform.position, lastGroundedPosition);
                    bool isLongFall = fallDistance > longFallThreshold;

                    if (isLongFall)
                    {
                        animator.CrossFade(HumanoidAnimatorConstants.LANDING_CLIP_NAME, .1f);
                    }
                }
            }

            animator.SetBool(HumanoidAnimatorConstants.IS_FALLING, !isGrounded);
        }

        bool IsGrounded()
        {
            Vector3 origin = this.transform.position;
            origin.y += GROUND_RAYCAST_OFFSET;
            Debug.DrawRay(origin, Vector3.down, Color.cyan);
            return Physics.Raycast(origin, Vector3.down, minimumDistanceToGround + GROUND_RAYCAST_OFFSET + 0.01f, this.groundLayers);
        }
    }

}
