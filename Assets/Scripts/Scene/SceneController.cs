using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("Loading panel: ")]
    [SerializeField] private GameObject loadingScreen;

    private Slider loadingSlider;


    private bool IsLoadingSliderAvailable => loadingSlider != null;
    private bool IsLoadingScreenAvailable => loadingScreen != null;
    private void Start()
    {
        if (IsLoadingScreenAvailable)
        {
            loadingSlider = loadingScreen.GetComponentInChildren<Slider>();
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (IsLoadingScreenAvailable)
        {
            loadingScreen.SetActive(true);
        }
        StartCoroutine(LoadLevelAsync(sceneName));
    }

    private IEnumerator LoadLevelAsync(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = false;

        float sliderSpeed = 0.5f;

        while (!loadOperation.isDone)
        {
            float targetProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);

            if (IsLoadingSliderAvailable)
            {
                while (loadingSlider.value < targetProgress)
                {
                    loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, targetProgress, Time.deltaTime * sliderSpeed);
                    yield return null;
                }
            }

            if (loadOperation.progress >= 0.9f)
            {
                if (IsLoadingSliderAvailable)
                {
                    while (loadingSlider.value < 1f)
                    {
                        loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, 1f, Time.deltaTime * sliderSpeed);
                        yield return null;
                    }
                }
                //yield return new WaitForSeconds(3f);
                loadOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
