using UnityEngine;
using System.Collections.Generic;

namespace cryingpants
{

    public class FSMBehaviour: MonoBehaviour
    {
        public List<Vector3> states;

        public void Start()
        {
            Debug.Log("Initing list");
            states.Add(new Vector3(0f, 0f, 0f));
        }

    }
}
