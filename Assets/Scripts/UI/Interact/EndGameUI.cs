using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    private static EndGameUI instance;
    public static EndGameUI Instance => instance;

    [SerializeField] private float timeAudioWin;
    [SerializeField] private float timeAudioLose;
    [SerializeField] private float timeUIWin;
    [SerializeField] private float timeUILose;
    [SerializeField] private Image imageStart;

    [Header("Win")]
    [SerializeField] private List<GameObject> gold;
    [SerializeField] private GameObject win;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButtonWin;
    [SerializeField] private Level levelCurrent;
    [SerializeField] private TypeResourcesLevel typeResourcesLevel;

    [Header("Lose")]
    [SerializeField] private GameObject lose;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button menuButtonLose;
    [SerializeField] private Image image;


    //[SerializeField] private Image background;
    //[SerializeField] private float time;
    //[SerializeField] private float timeMove;
    //[SerializeField] private float timeEnd;

    //[Header("Win")]
    //[SerializeField] private GameObject win;
    //[SerializeField] private List<Image> images_Win;
    //[SerializeField] private List<GameObject> gold;
    //[SerializeField] private Image imageContinue;
    //[SerializeField] private Image imageMenuWin;

    //[Header("Lose")]
    //[SerializeField] private GameObject lose;
    //[SerializeField] private List<Image> images_Lose;
    //[SerializeField] private Slider slider;
    //[SerializeField] private Color colorSlider;
    //[SerializeField] private Image imageSlider;
    //[SerializeField] private TextMeshProUGUI text;
    //[SerializeField] private Image imageRestart;
    //[SerializeField] private Image imageMenuLose;

    private void Awake()
    {
        if (EndGameUI.Instance != null) Debug.LogError("Only 1 Scrips EndGameUI Allow Exist !!!");
        instance = this;
    }

    private void Start()
    {
        levelCurrent = LevelManager.Instance.LoadDataLevel(levelCurrent.level);
        continueButton.onClick.AddListener(ContinueButton);
        menuButtonWin.onClick.AddListener(MenuButton);

        resetButton.onClick.AddListener(ResetButton);
        menuButtonLose.onClick.AddListener(MenuButton);
        image.sprite = LevelManager.Instance.GetSprite();
        imageStart.sprite = LevelManager.Instance.GetSprite();
    }

    public void Win()
    {
        if(levelCurrent.start < UpdateResourcesUI.Instance.GetNGold())
        {
            LevelManager.Instance.SaveDataLevel(levelCurrent.level, UpdateResourcesUI.Instance.GetNGold());
        }
        LevelManager.Instance.SaveDataLevel(levelCurrent.level + 1, 0);
        AudioManager.Instance.SettingAudioBackGround(0f);
        StartCoroutine(AudioWin());
        StartCoroutine(UIWin());
    }

    public void Lose()
    {
        AudioManager.Instance.SettingAudioBackGround(0f);
        StartCoroutine(AudioLose());
        StartCoroutine(UILose());
    }

    private IEnumerator AudioWin()
    {
        yield return new WaitForSeconds(timeAudioWin);
        AudioManager.Instance.ActiveAudioVirtual(0);
    }

    private IEnumerator UIWin()
    {
        yield return new WaitForSeconds(timeUIWin);
        win.SetActive(true);

        for (int i = 0; i < UpdateResourcesUI.Instance.GetNGold(); i++)
        {
            gold[i].SetActive(true);
        }
        //WinGame();
    }

    private IEnumerator AudioLose()
    {
        yield return new WaitForSeconds(timeAudioLose);
        AudioManager.Instance.ActiveAudioVirtual(1);
    }

    private IEnumerator UILose()
    {
        yield return new WaitForSeconds(timeUILose);
        lose.SetActive(true);
        slider.value = CameraFollow.Instance.RateDistance() * 100f;
        text.text = $"{(int)(CameraFollow.Instance.RateDistance() * 100f)} %";
        //LoseGame();
    }

    private void MenuButton()
    {
        GameManager.Instance.LoadMenuScene();
    }
    
    private void ResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void ContinueButton()
    {
        LevelManager.Instance.ActiveLoading(typeResourcesLevel);
    }

    //private void EndGame(List<Image> images, GameObject end)
    //{
    //    Color color = Color.black;
    //    color.a = 0f;
    //    AudioManager.Instance.SettingAudioBackGround(0f);
    //    foreach (var item in images)
    //    {
    //        item.color = color;
    //    }
    //    background.color = color;
    //    complete.SetActive(true);
    //    end.SetActive(true);
    //}

    //private void WinGame()
    //{
    //    EndGame(images_Win,win);
    //    Color color = Color.black;
    //    color.a = 0f;
    //    imageMenuWin.color = color;
    //    imageContinue.color = color;
    //    color.a = 0.5f;

    //    DOVirtual.Float(0f, 0.5f, time, colorA => { color.a = colorA; background.color = color; }).onComplete += () =>
    //    DOTween.Sequence(color = Color.white).onComplete += () =>
    //    DOVirtual.Int(0,UpdateResourcesUI.Instance.GetNGold(),0f, n=> { for (int i = 0; i <n;i++ ) { gold[i].SetActive(true); }; }).onComplete += ()=>
    //    DOVirtual.Float(0f, 1f, time, colorA => { color.a = colorA; images_Win.ForEach(item => item.color = color);}).onComplete += () =>
    //    DOVirtual.Float(400f,0f,timeMove, position => gold.ForEach(item => item.transform.localPosition = UnityEngine.Vector3.up * position)).onComplete += () =>
    //    DOVirtual.DelayedCall(time,null).onComplete += ()=>
    //    DOTween.Sequence(color.a = 0f).onComplete += () =>
    //    DOVirtual.Int(0,1,0f, n => imageMenuWin.gameObject.SetActive(true)).onComplete += () =>
    //    DOVirtual.Float(0,1f, timeEnd, colorA => { color.a = colorA; imageMenuWin.color = color;  }).onComplete += () =>
    //    DOTween.Sequence(color.a = 0f).onComplete += () =>
    //    DOVirtual.Int(0, 1, 0f, n => imageContinue.gameObject.SetActive(true)).onComplete += () =>
    //    DOVirtual.Float(0, 1f, timeEnd, colorA => { color.a = colorA; imageContinue.color = color; });
    //}

    //private void LoseGame()
    //{
    //    EndGame(images_Lose,lose);
    //    Color color = Color.black;
    //    color.a = 0f;
    //    colorSlider.a = 0f;
    //    text.color = color;
    //    imageSlider.color = color;
    //    imageMenuLose.color = color;
    //    imageRestart.color = color;
    //    color.a = 0.5f;

    //    slider.value = CameraFollow.Instance.RateDistance() * 100f;
    //    text.text = $"{(int)(CameraFollow.Instance.RateDistance() * 100f)} %";
    //    DOVirtual.Float(0f, 0.5f, time, colorA => { color.a = colorA; background.color = color; }).onComplete += () =>
    //    DOTween.Sequence(color = Color.white).onComplete += () =>
    //    DOVirtual.Float(0f, 1f, time, colorA => { color.a = colorA; colorSlider.a = colorA; images_Lose.ForEach(item => item.color = color); text.color = color; imageSlider.color = colorSlider; }).onComplete += () =>
    //    DOVirtual.DelayedCall(time,null).onComplete += ()=>
    //    DOTween.Sequence(color.a = 0f).onComplete += () =>
    //    DOVirtual.Int(0, 1, 0f, n => imageMenuLose.gameObject.SetActive(true)).onComplete += () =>
    //    DOVirtual.Float(0, 1f, timeEnd, colorA => { color.a = colorA; imageMenuLose.color = color; }).onComplete += () =>
    //    DOTween.Sequence(color.a = 0f).onComplete += () =>
    //    DOVirtual.Int(0, 1, 0f, n => imageRestart.gameObject.SetActive(true)).onComplete += () =>
    //    DOVirtual.Float(0, 1f, timeEnd, colorA => { color.a = colorA; imageRestart.color = color; });
    //}



    private void OnDisable()
    {
        StopAllCoroutines();
        //DOTween.KillAll();
    }
}
