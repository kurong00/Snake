using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour {

    List<Transform> snakeBodyList = new List<Transform>();
    float snakeSpeed = 20.0f;
    float currentSpeed;
	void Start ()
    {
        InitSnakeData();
    }
	
    void InitSnakeData()
    {
        currentSpeed = snakeSpeed;
        snakeBodyList.Insert(0, gameObject.transform);
    }

	void Update () {
        Move();
	}

    void Move()
    {
        if (snakeBodyList.Count != 0)
        {
            snakeBodyList[0].Translate(snakeBodyList[0].forward * currentSpeed * Time.deltaTime);
        }
    }
}
