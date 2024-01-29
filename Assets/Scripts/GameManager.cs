using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Audio;
using UnityEngine.SceneManagement;
using AudioType = UnityCore.Audio.AudioType;

public class GameManager : MonoBehaviour
{
    public bool isGamePaused = false;
    [SerializeField]
    private bool isFirstPotion = true;

    [Header("Music")]
    public AudioType levelMusic;

    [Header("Potions")] 
    public List<PotionObject> potions;

    UIManager uiManager;
    CameraView cam;

    private static GameManager instance;
    [SerializeField]
    private int potionMissedThrow = 0;

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
    public void TriggerPotionTrhow(bool active)
    {
        Tuto tuto = GameObject.FindObjectOfType<Tuto>();


        if (tuto != null)
        {
            if (active)
            {

                potionMissedThrow++;
                if (isFirstPotion || potionMissedThrow >= 5)
                {
                    tuto.TriggerPotionThrowInfos(active);
                    potionMissedThrow = 0;
                }

            }
            else
                tuto.TriggerPotionThrowInfos(false);
        }

        isFirstPotion = false;
    }


    public void SetGamePause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1; 
    }
}