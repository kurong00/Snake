using UnityEngine;

public class FoodEffect : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, Random.Range(1, 5), 0));
    }
}
