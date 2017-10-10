using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    AudioSource backgoundMusic;
    AudioSource soundEffect;
    Vector3 leftJoyStick;
    Vector3 rightJoyStick;
    public GameObject stopPanel;
    public Slider music;
    public Slider sound;
    public Image joyStick;
	void Start () {
        backgoundMusic = GetComponent<AudioSource>();
        soundEffect = GameObject.Find("colider").GetComponent<AudioSource>();
        music.value = backgoundMusic.volume;
        sound.value = soundEffect.volume;
        leftJoyStick = joyStick.transform.position;
        rightJoyStick = new Vector3(300, leftJoyStick.y, 0);
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
}
