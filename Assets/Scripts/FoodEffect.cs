using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEffect : MonoBehaviour
{


    void Start()
    {

    }


    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, Random.Range(1, 5), 0));
    }
}
