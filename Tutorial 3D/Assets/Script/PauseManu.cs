using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUi;
    [SerializeField] private SaveNLoad theSaveNLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if(!GameManager.isPause)
            {
                CallManu();
            }
            else
            {
                CloseMenu();
            }
        }
    }
    private void CallManu()
    {
        GameManager.isPause = true;
        go_BaseUi.SetActive(true);
        Time.timeScale = 0f;
    }
    private void CloseMenu()
    {
        GameManager.isPause = false;
        go_BaseUi.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ClickSave()
    {
        Debug.Log("세이브");
        theSaveNLoad.SaveDate();
    }

    public void ClickLoad()
    {
        Debug.Log("로드");
        theSaveNLoad.LoadData();
    }

    public void ClickExit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }
}
