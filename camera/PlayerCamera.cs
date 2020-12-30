using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{

    public class PlayerCamera : MonoBehaviour
    {
        public Transform target;

        [Header("Camera Settings")]
        public float followSpeed = 0.1f;
        public float rotateSpeed = 0.5f;
        public float pivotSpeed = 0.5f;
        public float minPivot = -35;
        public float maxPivot = 35;
        public Transform pivot;

        [Header("Combat Settings")]
        public Transform lockTarget;

        float lookAngle;
        float pivotAngle;

        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            HandleRotation(mouseX, mouseY);
        }

        private void FixedUpdate()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            FollowTarget(mouseX, mouseY);
        }

        public void FollowTarget(float mouseX, float mouseY)
        {
            Vector3 targetPosition = Vector3.Lerp(transform.position, target.transform.position, Time.fixedDeltaTime / followSpeed);
            transform.position = targetPosition;
        }

        public void HandleRotation(float mouseX, float mouseY)
        {
            Quaternion targetRotation, pivotRotation;
            

            if (lockTarget != null)
            {
                // Rotation
                Vector3 direction = lockTarget.position - transform.position;
                direction.Normalize();
                direction.y = 0;

                targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;

                // Pivot
                Vector3 pivotDirection = lockTarget.position - pivot.position;
                pivotDirection.Normalize();

                pivotRotation = Quaternion.LookRotation(pivotDirection);
                Vector3 e = pivotRotation.eulerAngles;
                e.y = 0;

                pivot.localEulerAngles = e;
                
                pivotAngle = pivot.localEulerAngles.x;
                lookAngle = transform.eulerAngles.y;

                return;
            }

            lookAngle += (mouseX * rotateSpeed) / Time.deltaTime;
            pivotAngle -= (mouseY * pivotSpeed) / Time.deltaTime;
            pivotAngle = Mathf.Clamp(pivotAngle, minPivot, maxPivot);

            Vector3 euler = Vector3.zero;
            euler.y = lookAngle;
            targetRotation = targetRotation = Quaternion.Euler(euler);
            transform.rotation = targetRotation;


            Vector2 pivotEuler = Vector3.zero;
            pivotEuler.x = pivotAngle;
            pivotRotation = Quaternion.Euler(pivotEuler);
            pivot.localRotation = pivotRotation;
        }
    } 

}