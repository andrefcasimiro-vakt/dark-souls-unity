using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG { 

    /// <summary>
    /// A movement handler for locomotion actions such as walking, running and sprinting.
    /// </summary>
    public class Movement
    {

        Animator animator;
        Rigidbody rigidbody;
        Transform transform;
        GroundCheck groundCheck;

        float frontRayOffset = .5f;
        float adaptSpeed = 10f;
        float speed;

        public float moveAmount = 0f;

        public Movement(GameObject target, GroundCheck groundCheck, float speed)
        {
            this.transform = target.transform;
            this.rigidbody = target.GetComponent<Rigidbody>();
            this.animator = target.GetComponent<Animator>();
            this.groundCheck = groundCheck;
            this.speed = speed;
        }

        public void Execute(float horizontal, float vertical)
        {
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            animator.SetFloat("vertical", moveAmount);

            float frontY = 0f;
            RaycastHit hit;

            Vector3 origin = transform.position + transform.forward * frontRayOffset;
            origin.y += .5f;

            Debug.DrawRay(origin, -Vector3.up, Color.red, .01f, false);
            if (Physics.Raycast(origin, -Vector3.up, out hit, 1, groundCheck.groundLayers))
            {
                float y = hit.point.y;
                frontY = y - transform.position.y;
            }

            Vector3 currentVelocity = rigidbody.velocity;
            Vector3 targetVelocity = transform.forward * moveAmount * speed;

            
            if (groundCheck.isGrounded)
            {
                float abs = Mathf.Abs(frontY);

                if (moveAmount > 0.1f) // Is Moving
                {
                    rigidbody.isKinematic = false;
                    rigidbody.drag = 0;

                    if (abs > 0.02f)
                    {
                        targetVelocity.y = ((frontY > 0) ? frontY + 0.2f : frontY - 0.2f) * speed;
                    }
                }
            }
            else // Falling
            {
                rigidbody.isKinematic = false;
                rigidbody.drag = 0;
                targetVelocity.y = currentVelocity.y;
            }

            Debug.DrawRay((transform.position + Vector3.up * .2f), targetVelocity, Color.green, 0.01f, false);
            rigidbody.velocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.fixedDeltaTime * adaptSpeed);

        }

    }

}
