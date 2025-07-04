using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndScene : MonoBehaviour
{
    public Image fadePanel; 
    public CanvasGroup fadeCanvasGroup; 

    public float fadeDuration = 1f;

    public void OnClickEnd()
    {
        if (fadeCanvasGroup != null)
            fadeCanvasGroup.blocksRaycasts = true;

        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    private void Start()
    {
        if (fadePanel != null)
        {
            if (fadeCanvasGroup != null)
                fadeCanvasGroup.blocksRaycasts = true;

            fadePanel.color = new Color(0, 0, 0, 1f);

            fadePanel.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                if (fadeCanvasGroup != null)
                    fadeCanvasGroup.blocksRaycasts = false;
            });
        }
    }

}
