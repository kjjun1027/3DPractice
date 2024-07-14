using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class Title : MonoBehaviour
{
    public string sceneName = "GameStage";

    public static Title Instance;

    private SaveNLoad theSaveNLoad;

    private void Awake()
    {

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ClickStart()
    {
        Debug.Log("로딩");
        SceneManager.LoadScene(sceneName);
    }
    public void ClickLoad()
    {
        Debug.Log("로드");
        StartCoroutine(LoadCoroutine());
    }
    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while(!operation.isDone)
        {
            yield return null;
        }
        theSaveNLoad = FindObjectOfType<SaveNLoad>();
        theSaveNLoad.LoadData();
        Destroy(gameObject);
    }
    public void ClickExit()
    {
        Debug.Log("종료");
        Application.Quit();
    }
}
