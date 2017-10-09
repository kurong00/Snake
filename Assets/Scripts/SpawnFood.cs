using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {

    public List<Transform> spawnArea = new List<Transform>();
    public static SpawnFood instance = null;
    int index = 0;
    static int foodCount = 0;
    GameObject prefab;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    void Start ()
    {
        prefab = Resources.Load("food") as GameObject;
        StartCoroutine(Food());
    }

    void SpawnRandomFood()
    {
        index = Random.Range(0, 6);
        Vector3 pos = new Vector3(Random.Range(spawnArea[index].transform.position.x,0),4,
            Random.Range(spawnArea[index].transform.position.z,0));
        GameObject food = GameObject.Instantiate(prefab,pos,Quaternion.identity);
        foodCount++;
        food.transform.SetParent(spawnArea[index].transform);
    }

    IEnumerator Food()
    {
        while (true)
        {
            yield return new WaitForSeconds(foodCount * 0.5f);
            SpawnRandomFood();
        }
    }

    public void DecreaseFood()
    {
        foodCount--;
    }
}
