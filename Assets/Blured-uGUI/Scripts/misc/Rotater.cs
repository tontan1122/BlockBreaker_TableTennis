using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example.uGUI
{
    internal class Rotater : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _rotate = Vector3.zero;

        private void Update()
        {
            transform.Rotate(_rotate);
        }
    }
}
