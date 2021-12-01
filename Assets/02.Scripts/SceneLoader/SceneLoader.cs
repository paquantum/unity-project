using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public UnityEvent OnLoadBegin = new UnityEvent();
    public UnityEvent OnLoadEnd = new UnityEvent();
    public ScreenFader screenFader = null;

    private bool isLoading = false;

    private void Awake()
    {
        SceneManager.sceneLoaded += SetActiveScene;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SetActiveScene;        
    }

    public void LoadNewScene(string sceneName)
    {
        if(!isLoading)
            StartCoroutine(LoadScene(sceneName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        isLoading = true;
        
        OnLoadBegin.Invoke();
        yield return screenFader.StartFadeIn();
        yield return StartCoroutine(UnloadCurrent());
        yield return new WaitForSeconds(3.0f);// for testing
        yield return StartCoroutine(LoadNew(sceneName));

		yield return null;
    }

    private IEnumerator LoadEnd()
    {
        yield return new WaitForSeconds(1.0f);// for testing
        //씬이 로드가 완료 되면 fade out
        screenFader.StartFadeOut();
        OnLoadEnd?.Invoke();
        isLoading = false;
        yield return null;

    }
    private IEnumerator UnloadCurrent()
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        //unloadOperation.progress
        while(!unloadOperation.isDone)
            yield return null;
    }

    private IEnumerator LoadNew(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return null;
    }

    private void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
        if(isLoading)
            StartCoroutine(LoadEnd());
    }
}
