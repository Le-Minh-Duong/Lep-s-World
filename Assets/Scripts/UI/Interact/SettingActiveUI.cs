using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingActiveUI : MonoBehaviour
{
    [SerializeField] private Button setting;
    [SerializeField] private Button buttonPause;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonMenu;
    [SerializeField] private GameObject backgroundSetting;

    private void Start()
    {
        setting.onClick.AddListener(ButtonSetting);
        buttonPause.onClick.AddListener(ButtonPause);
        buttonRestart.onClick.AddListener(ButtonRestart);
        buttonMenu.onClick.AddListener(ButtonMenu);
    }

    private void ButtonSetting()
    {
        backgroundSetting.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void ButtonPause()
    {
        Time.timeScale = 1.0f;
        backgroundSetting.SetActive(false);
    }
    
    private void ButtonRestart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ButtonMenu()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.LoadMenuScene();
    }
}
