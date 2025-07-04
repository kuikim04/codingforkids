using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMainMenu : MonoBehaviour
{
    public static GameMainMenu instance;

    public Image fadePanel;
    public float fadeDuration = 0.5f;
    private bool isTransitioning = false;

    [SerializeField] private Button homeBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button prevBtn;


    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject wrongPanel;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex >= 1 && sceneIndex <= 12)
        {
            SoundManager.instance.PlayGameplayMusic();
            SoundManager.instance.PlayVoiceForScene(sceneIndex);
        }
        if (fadePanel != null)
        {
            fadePanel.color = new Color(0, 0, 0, 1f);
            fadePanel.raycastTarget = true; 
            fadePanel.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                fadePanel.raycastTarget = false; 
            });
        }

        homeBtn.onClick.AddListener(BackToMenu);
        nextBtn.onClick.AddListener(NextScene);
        prevBtn.onClick.AddListener(PrevScene);


        winPanel.SetActive(false);
        wrongPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        homeBtn.onClick.RemoveListener(BackToMenu);
        nextBtn.onClick.RemoveListener(NextScene);
        prevBtn.onClick.RemoveListener(PrevScene);
    }

    public void BackToMenu()
    {
        SoundManager.instance.PlayClickSound();

        if (isTransitioning) return;
        isTransitioning = true;

        FadeAndLoadScene(0); 
    }

    public void NextScene()
    {
        SoundManager.instance.PlayClickSound();

        if (isTransitioning) return;
        isTransitioning = true;

        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            FadeAndLoadScene(nextScene);
        }
    }

    public void PrevScene()
    {
        SoundManager.instance.PlayClickSound();

        if (isTransitioning) return;
        isTransitioning = true;

        int prevScene = SceneManager.GetActiveScene().buildIndex - 1;
        if (prevScene >= 0)
        {
            FadeAndLoadScene(prevScene);
        }
    }

    private void FadeAndLoadScene(int sceneIndex)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.raycastTarget = true; 

        fadePanel.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneIndex);
        });
    }


    public void GameWin()
    {
        SoundManager.instance.PlayWinSound();
        winPanel.SetActive(true);
    }
   
    public void GameLose()
    {
        wrongPanel.SetActive(true);
        SoundManager.instance.PlayWrongSound();
    }
}
