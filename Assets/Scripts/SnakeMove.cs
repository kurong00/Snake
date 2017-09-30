using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    float distanceBetweenBodyParts;
    List<Transform> snakeBodyList = new List<Transform>();
    float snakeSpeed = 5.0f;
    float rotateAngles = 90.0f;
    float currentSpeed;
    Quaternion initRotate;
    GameObject bodyPart;
    Transform currentBodyPart;
    Transform prevBodyPart;
    public float minDistance;
    void Start()
    {
        InitSnakeData();
        bodyPart = Resources.Load("bodyPart") as GameObject;
        for(int i = 0; i < 3; i++)
        {
            AddBodyPart();
        }
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

    void AddBodyPart()
    {
        if (snakeBodyList.Count != 0)
        {
            Vector3 pos = new Vector3(snakeBodyList[snakeBodyList.Count - 1].position.x,
                snakeBodyList[snakeBodyList.Count - 1].position.y, snakeBodyList[snakeBodyList.Count - 1].position.z);
            GameObject go = Instantiate(bodyPart, pos, 
                snakeBodyList[snakeBodyList.Count - 1].rotation);
            go.transform.SetParent(snakeBodyList[0].transform);
            snakeBodyList.Add(go.transform);
        }
    }

    void Move()
    {
        currentSpeed = snakeSpeed;
        if (Input.GetKey(KeyCode.UpArrow))
            currentSpeed *= 2;
        snakeBodyList[0].Translate(snakeBodyList[0].forward * currentSpeed * Time.deltaTime,Space.World);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            StopAllCoroutines();
            StartCoroutine(RotateSnakeHead(rotateAngles-180));
            rotateAngles -= 90;
            snakeBodyList[0].Rotate(Vector3.up * 90 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            StopAllCoroutines();
            StartCoroutine(RotateSnakeHead(rotateAngles));
            rotateAngles += 90;
            snakeBodyList[0].Rotate(Vector3.up * -90 * Time.deltaTime);
        }
        //if (Input.GetAxis("Horizontal") !=0)
            //snakeBodyList[0].Rotate(Vector3.up * 90 * Time.deltaTime * Input.GetAxis("Horizontal"));
        for (int i = 1; i < snakeBodyList.Count; i++)
        {
            distanceBetweenBodyParts = Vector3.Distance(snakeBodyList[i].position , snakeBodyList[i - 1].position);
            float time = Time.deltaTime * (distanceBetweenBodyParts / 0.25f) * currentSpeed;
            if (time > 0.5)
                time = 0.5f;
            Vector3 newPos = snakeBodyList[i - 1].position;
            //newPos.y = snakeBodyList[0].position.y;
            newPos.z = snakeBodyList[i - 1].position.z - 1.8f;
            snakeBodyList[i].position = Vector3.Slerp(snakeBodyList[i].position,
                newPos, time);
            snakeBodyList[i].rotation = Quaternion.Slerp(snakeBodyList[i].rotation,
                snakeBodyList[i - 1].rotation, time);
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
