using UnityEngine;
using System.Collections;

namespace RPG
{

    [CreateAssetMenu(menuName = "Equipment/Weapon")]
    public class Weapon : ScriptableObject
    {

        [Header("Properties")]
        public string name;
        public string description;
        public float weight = 3f;
        public int durability = 100;

        [Header("Type")]
        public WeaponTypeEnum type;

        [Header("Damage Reducer")]
        public int physicalDamage = 0;
        public int fireDamage = 0;
        public int frostDamage = 0;
        public int lightningDamage = 0;
        public int magicDamage = 0;

        [Header("Auxiliar Effects")]
        public int poison = 0;
        public int curse = 0;
        public int bleed = 0;

        [Header("Requirements")]
        public int strength = 0;
        public int dexterity = 0;
        public int intelligence = 0;
        public int faith = 0;

        [Header("Scaling")]
        public float strengthScaling = 0f;
        public float dexterityScaling = 0f;
        public float intelligenceScaling = 0f;
        public float faithScaling = 0f;

        [Header("Animations")]
        public AnimatorOverrideController animator;

        [Header("UI")]
        public Sprite sprite;

        [Header("Graphics")]
        public GameObject prefab;

        [Tooltip("Are animations for the right hand. If so, this will allow mirroring to the left if equipping weapon on a left hand.")]
        public bool usesRightHandAnimations = true;

    }

}
