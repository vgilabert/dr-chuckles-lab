using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Audio;
using UnityEngine.SceneManagement;
using AudioType = UnityCore.Audio.AudioType;

public class GameManager : MonoBehaviour
{
    public bool isGamePaused = false;

    [Header("Music")]
    public AudioType levelMusic;

    [Header("Potions")] 
    public List<PotionObject> potions;

    UIManager uiManager;
    CameraView cam;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("Game Manager not found!");
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        AudioController.instance.tracks[0].volume = PlayerPrefs.GetFloat("Music");
        AudioController.AudioTrack track = AudioController.instance.tracks[0];
        AudioType type = AudioType.None;
        foreach (AudioController.AudioObject obj in track.audio)
        {
            if (obj.clip == track.source.clip)
            {
                type = obj.type;
                break;
            }
        }

        if (levelMusic != type)
        {
            AudioController.instance.PlayAudio(levelMusic, true, 0.2f);
        }
        uiManager = FindObjectOfType<UIManager>();
        cam = FindObjectOfType<CameraView>();
    }
    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            SetGamePause(isGamePaused);
        }

        if(isGamePaused)
        {

        }

    }

    public void SetGamePause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1; 
    }
}