using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject target;
    public float moveSpeed;
    Vector3 offset;
    Quaternion quaternion;
	void Update () {
		Move();
	}

    private void Move()
    {
        offset = (target.transform.position - transform.position).normalized;
        quaternion = Quaternion.LookRotation(offset);
        quaternion.x = transform.rotation.x;
        quaternion.z = transform.rotation.z;
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * 100);
        transform.position = Vector3.Slerp(transform.position, target.transform.position, Time.deltaTime );

    }
}
