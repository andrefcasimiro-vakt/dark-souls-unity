using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{

    [RequireComponent(typeof(SphereCollider))]
    public class TargetDetector : MonoBehaviour
    {
        [Header("Detection")]
        public string[] tags;

        [Header("Chase")]
        public float delayAfterStopChasing = 3f;

        [Header("Debug")]
        public GameObject detectedTarget;
        public bool TargetInRange => detectedTarget != null;

        private void OnTriggerEnter(Collider other)
        {
            foreach (string tag in tags)
            {
                if (tag == other.gameObject.tag)
                {
                    detectedTarget = other.gameObject;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == detectedTarget)
            {
                StartCoroutine(StopChaseAfterDelay());
            }
        }

        private IEnumerator StopChaseAfterDelay()
        {
            yield return new WaitForSeconds(delayAfterStopChasing);
            detectedTarget = null;
        }

        public Vector3 GetNearestTargetPosition()
        {
            return detectedTarget?.transform.position ?? Vector3.zero;
        }

    }

}
