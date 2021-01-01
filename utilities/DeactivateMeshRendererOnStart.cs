﻿using UnityEngine;
using System.Collections;

namespace RPG
{

    public class DeactivateMeshRendererOnStart : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}