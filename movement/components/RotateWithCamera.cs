using UnityEngine;
using System.Collections;

namespace RPG { 
    public class RotateWithCamera
    {
        Transform transform;
        Movement movement;
        Camera camera;

        public float rotationSpeed;

        public RotateWithCamera(Transform transform, Movement movement, Camera camera, float rotationSpeed)
        {
            this.transform = transform;
            this.movement = movement;
            this.camera = camera;
            this.rotationSpeed = rotationSpeed;
        }

        public void HandleRotation(float horizontalMovement, float verticalMovement)
        {
            Vector3 targetDirection = camera.transform.forward * verticalMovement;
            targetDirection += camera.transform.right * horizontalMovement;
            targetDirection.Normalize();

            targetDirection.y = 0;
            
            if (targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion slerpedRotation = Quaternion.Slerp(
                transform.rotation, targetRotation, Time.fixedDeltaTime * movement.moveAmount * rotationSpeed);

            transform.rotation = slerpedRotation;
        }
            
    }
}
