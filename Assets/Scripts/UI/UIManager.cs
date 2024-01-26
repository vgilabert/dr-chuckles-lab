using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityCore.Audio;
using UnityEngine.SceneManagement;
using AudioType = UnityCore.Audio.AudioType;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject levelComplete;
    [SerializeField]
    LevelLoader levelLoader;


    // Update is called once per frame
    void Update()
    {
        pauseMenu.SetActive(GameManager.Instance.isGamePaused);

        //PlayerInfos.GetComponent<Animator>().SetBool("Start", GameManager.Instance.isPlayerDead);
    }

    public void GoToMenu()
    {
        AudioController.instance.PlayAudio(AudioType.SFX_MenuButtonPressed);
        Time.timeScale = 1f;
        AudioController.instance.StopAudio(GameManager.Instance.levelMusic, true, .5f);
        
        GameManager.Instance.isGamePaused = false;
        StartCoroutine(levelLoader.LoadLevel(0));
    }

    public void Resume()
    {
        ButtonPressed();
        GameManager.Instance.isGamePaused = false;
        GameManager.Instance.SetGamePause(GameManager.Instance.isGamePaused);
    }
    
    public void SOLONG()
    {
        Application.Quit();
    }

    public void ButtonHover()
    {
        AudioController.Instance.PlayAudio(AudioType.SFX_ButtonHover, false, 0, Random.Range(.9f, 1.1f));
    }

    public void ButtonPressed()
    {
        AudioController.Instance.PlayAudio(AudioType.SFX_ButtonPressed, false, 0, Random.Range(.95f, 1f));
    }
}