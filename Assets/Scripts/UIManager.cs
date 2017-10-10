using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null;
    AudioSource backgoundMusic;
    AudioSource soundEffect;
    Vector3 leftJoyStick;
    Vector3 rightJoyStick;
    int highScore;
    public GameObject stopPanel;
    public GameObject gameOverPanel;
    public Slider music;
    public Slider sound;
    public Image joyStick;
    public Text score;
    public Text finalScore;
    public Text bestScore;
    public GameObject hint;
    private void Awake()
    {
        if (instance)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    void Start () {
        backgoundMusic = GetComponent<AudioSource>();
        soundEffect = GameObject.Find("colider").GetComponent<AudioSource>();
        music.value = backgoundMusic.volume;
        sound.value = soundEffect.volume;
        leftJoyStick = joyStick.transform.position;
        rightJoyStick = new Vector3(300, leftJoyStick.y, 0);
        if (!PlayerPrefs.HasKey("BestScore"))
            PlayerPrefs.SetInt("BestScore", 0);
        highScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    public void StopPanel()
    {
        stopPanel.SetActive(stopPanel.activeSelf == true ? false : true);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void MusicVolumnChange()
    {
        backgoundMusic.volume = music.value;
    }

    public void SoundVolumnChange()
    {
        soundEffect.volume = sound.value;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeJoyStick()
    {
        if (joyStick.transform.position == leftJoyStick)
            joyStick.transform.position = rightJoyStick;
        else
            joyStick.transform.position = leftJoyStick;
        JoyStick.instance.CenterPos = new Vector2(joyStick.transform.position.x,
                joyStick.transform.position.y);
    }

    public void AddScore(int amount)
    {
        score.text = amount.ToString();
    }

    public void GameOver(int amount)
    {
        gameOverPanel.SetActive(true);
        finalScore.text = amount.ToString();
        bestScore.text = highScore.ToString();
        if (amount > highScore)
        {
            bestScore.text = amount.ToString();
            PlayerPrefs.SetInt("BestScore", amount);
            hint.SetActive(true);
        }
        Time.timeScale = 0;
        backgoundMusic.Stop();
    }

    public void RePlay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
