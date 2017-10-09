using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnter : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "barrier" || other.tag == "bodyPart")
            Debug.Log("enter");
    }
}
