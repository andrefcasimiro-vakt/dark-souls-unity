using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Components")]
        public Animator animator;
        public Camera playerCamera;
        public Rigidbody rigidBody;

        [Header("Movement Settings")]
        public float moveSpeed = 7f;
        public float moveRotationSpeed = 20f;

        [Header("Movement Rules")]
        public bool canMove;


        [Header("Ground Settings")]
        public float minimumGroundDistance = .3f;
        public LayerMask groundLayers;

        [Header("Fall Settings")]
        [Tooltip("The distance of a fall before applying the landing animation")]
        public float longFallThreshold = 5f;

        GroundCheck groundCheck;
        Movement movement;
        RotateWithCamera movementRotation;

        private void Start()
        {
            animator.applyRootMotion = false;
            rigidBody.constraints = RigidbodyConstraints.FreezeRotation;

            groundCheck = new GroundCheck(this.gameObject, minimumGroundDistance, longFallThreshold, groundLayers);

            movement = new Movement(this.gameObject, groundCheck, moveSpeed);
            movementRotation = new RotateWithCamera(this.transform, this.movement, this.playerCamera, moveRotationSpeed);
        }

        private void Update()
        {
            canMove = (
                animator.GetBool(HumanoidAnimatorConstants.IS_ATTACKING) == false
            );


            groundCheck.Execute();
        }

        private void FixedUpdate()
        {
            if (!canMove) return;

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            movement.Execute(horizontalInput, verticalInput);
            movementRotation.HandleRotation(horizontalInput, verticalInput);
        }

    }

}
