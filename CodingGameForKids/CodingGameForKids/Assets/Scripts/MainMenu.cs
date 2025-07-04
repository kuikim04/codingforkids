using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialPanelA;
    public GameObject tutorialPanelB;

    public Image fadePanel;
    public CanvasGroup raycastBlocker; 
    public float fadeDuration = 1f;
    private bool isLoading = false;

    private void Start()
    {
        SoundManager.instance.PlayMenuMusic();

        if (fadePanel != null)
        {
            fadePanel.color = new Color(0, 0, 0, 1f);
            fadePanel.DOFade(0f, fadeDuration);
        }

        if (raycastBlocker != null)
        {
            raycastBlocker.blocksRaycasts = false;
        }

        if (Singleton.instance != null && Singleton.instance.IsPlaying)
        {
            tutorialPanelA.SetActive(false);
            tutorialPanelB.SetActive(false);
        }
        else
        {
            tutorialPanelA.SetActive(true);
            tutorialPanelB.SetActive(true);
        }
    }

    public void OnClickPanel(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void OnClickStartGame()
    {
        Singleton.instance.StartGame();
    }

    public void ClickSelectLevel(int level)
    {
        if (!Singleton.instance.IsPlaying)
            OnClickStartGame();

        SoundManager.instance.PlayClickSound();

        if (isLoading) return;
        isLoading = true;

        if (raycastBlocker != null)
        {
            raycastBlocker.blocksRaycasts = true;
            raycastBlocker.interactable = false;
        }

        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            SceneManager.LoadScene(level);
        });
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
