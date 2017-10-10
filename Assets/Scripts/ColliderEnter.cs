using UnityEngine;

public class ColliderEnter : MonoBehaviour {

    AudioSource audioSource;
    int score = 0;
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "barrier" || other.tag == "bodyPart")
            UIManager.instance.GameOver(score);
        if (other.tag == "food")
        {
            score += 10;
            UIManager.instance.AddScore(score);
            audioSource.Play();
            Destroy(other.gameObject,0.2f);
            SnakeMove.instance.AddBodyPart();
            SpawnFood.instance.DecreaseFood();
        }
    }
}
