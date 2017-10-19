using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    public static SnakeMove instance = null;
    public List<Transform> bodyParts = new List<Transform>();
    public float minDistanceBetweenEachPart = 3f;
    public int startBodySize;
    public float moveSpeed = 8;
    public float rotateSpeed = 250;
    GameObject bodyPrefab;
    float distanceBetweenBodyParts;
    float currentSpeed;
    Transform currentBodyPart;
    Transform previousBodyPart;
    
    void Awake()
    {

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }
    void Start()
    {
        
        bodyPrefab = Resources.Load("bodyPart") as GameObject;
        for (int i = 1; i < startBodySize; i++)
        {
            AddBodyPart();
        }
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        currentSpeed = moveSpeed;
        if (JoyStick.instance.Vertical > 0|| Input.GetKey(KeyCode.UpArrow))
            currentSpeed *= 2;
        bodyParts[0].Translate(bodyParts[0].forward * currentSpeed * Time.smoothDeltaTime, Space.World);
        if (Input.GetAxis("Horizontal") != 0)
            bodyParts[0].Rotate(Vector3.up * rotateSpeed * Time.deltaTime* Input.GetAxis("Horizontal"));
        if (JoyStick.instance.Horizon != 0)
            bodyParts[0].Rotate(Vector3.up * rotateSpeed * Time.deltaTime * JoyStick.instance.Horizon / JoyStick.instance.dragSpeed);
        if (bodyParts.Count != 0)
        {
            for (int i = 1; i < bodyParts.Count; i++)
            {
                currentBodyPart = bodyParts[i];
                previousBodyPart = bodyParts[i - 1];
                distanceBetweenBodyParts = Vector3.Distance(previousBodyPart.position, currentBodyPart.position);
                Vector3 newPos = previousBodyPart.position;
                newPos.y = bodyParts[0].position.y;
                float time = Time.deltaTime * distanceBetweenBodyParts / minDistanceBetweenEachPart * currentSpeed;
                if (time > 0.5)
                    time = 0.5f;
                currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPos, time);
                currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, previousBodyPart.rotation, time);
            }
        }
    }

    public void AddBodyPart()
    {
        
        Transform newBody = (Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].position,
            bodyParts[bodyParts.Count - 1].rotation) as GameObject).transform;
        newBody.SetParent(transform);
        bodyParts.Add(newBody);
        moveSpeed += bodyParts.Count / 8;
    }
    
}
