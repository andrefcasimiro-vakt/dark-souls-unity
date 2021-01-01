using System.Linq;
using UnityEngine;

namespace RPG
{

    public static class FindNearestObject<T>
    {
        public static GameObject Find(Transform transform)
        {
            return Object.FindObjectsOfType<GameObject>()
                   .Where(target => target.GetComponent<T>() != null)
                   .OrderBy(target => Vector3.Distance(transform.position, target.transform.position))
                   .FirstOrDefault();
        }

    }
}

