using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{

    List<Transform> snakeBodyList = new List<Transform>();
    float snakeSpeed = 10.0f;
    float rotateAngles = 90.0f;
    float currentSpeed;
    Quaternion initRotate;
    void Start()
    {
        InitSnakeData();
    }

    void InitSnakeData()
    {
        snakeBodyList.Insert(0, gameObject.transform);
        initRotate = snakeBodyList[0].rotation;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        currentSpeed = snakeSpeed;
        if (Input.GetKey(KeyCode.UpArrow))
            currentSpeed *= 2;
        if (snakeBodyList.Count != 0)
        {
            snakeBodyList[0].Translate(snakeBodyList[0].forward * currentSpeed * Time.deltaTime,Space.World);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            /*StopAllCoroutines();
            StartCoroutine(RotateSnakeHead(rotateAngles-180));
            rotateAngles -= 90;*/
            snakeBodyList[0].Rotate(Vector3.up * -90 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //StopAllCoroutines();
            //StartCoroutine(RotateSnakeHead(rotateAngles));
            //rotateAngles += 90;
            snakeBodyList[0].Rotate(Vector3.up * 90 * Time.deltaTime);
        }
    }

    IEnumerator RotateSnakeHead(float amount)
    {
        Quaternion angle = Quaternion.Euler(0, amount, 0) * initRotate;
        while (snakeBodyList[0].rotation != angle)
        {
            snakeBodyList[0].rotation = Quaternion.Lerp(snakeBodyList[0].rotation, angle, Time.deltaTime);
            yield return null;
        }
    }
}
