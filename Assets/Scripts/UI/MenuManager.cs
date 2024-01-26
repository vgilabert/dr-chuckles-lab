using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityCore.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AudioType = UnityCore.Audio.AudioType;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject btnPanel;

    public LevelLoader levelLoader;
    public float transitionTime = 1;
    public AudioType menuMusic;
    
    [SerializeField] List<Button> lvlBtns;
    int unlockedLvls = 0;

    void Awake()
    {

        if (PlayerPrefs.GetInt("Unlocked") == 0)
            PlayerPrefs.SetInt("Unlocked", 1);
    }



    public void Play()
    {
        AudioController.instance.PlayAudio(AudioType.SFX_PlayButtonPressed);
        StartCoroutine(levelLoader.LoadLevel(PlayerPrefs.GetInt("Unlocked")));
    }

    void Start()
    {
        PlayerPrefs.SetInt("STindex", 0);

        AudioController.instance.PlayAudio(menuMusic, true, 1f);

        AudioController.instance.tracks[2].volume = PlayerPrefs.GetFloat("Music");

       UpdateData();
       
       unlockedLvls = PlayerPrefs.GetInt("Unlocked");

       for (int i = 0; i < lvlBtns.Count; i++)
       {
           if (i >= unlockedLvls)
           {
               lvlBtns[i].interactable = false;
               lvlBtns[i].gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "LOCKED";
           }
       }
    }
    void UpdateData()
    {

    }

    public void LoadSpecificLevel(int lvl)
    {
        AudioController.instance.PlayAudio(AudioType.SFX_PlayButtonPressed);
        StartCoroutine(levelLoader.LoadLevel(lvl));
    }

    public void SOLONG()
    {
        Application.Quit();
    }

    public void ButtonHover()
    {
        AudioController.Instance.PlayAudio(AudioType.SFX_ButtonHover, false, 0, Random.Range(.9f,1.1f));
    }

    public void ButtonPressed()
    {
        AudioController.Instance.PlayAudio(AudioType.SFX_ButtonPressed, false, 0, Random.Range(.95f, 1.05f));
    }
}