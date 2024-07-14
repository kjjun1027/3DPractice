using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveDate
{
    public Vector3 PlayerPos;
    public Vector3 PlayerRot;

    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
}
public class SaveNLoad : MonoBehaviour
{
    private SaveDate saveData = new SaveDate();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILNAME = "/SaveFile.txt";

    private PlayerController thePlayer;
    private Inventory theInven;
    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";


        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }
     public void SaveDate()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theInven = FindObjectOfType<Inventory>();

        saveData.PlayerPos = thePlayer.transform.position;
        saveData.PlayerRot = thePlayer.transform.eulerAngles;

        Slot[] slots = theInven.GetSlots();

        for(int i =0; i<slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILNAME, json);

        Debug.Log("저장완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if(File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILNAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILNAME);
            saveData = JsonUtility.FromJson<SaveDate>(loadJson);

            thePlayer = FindObjectOfType<PlayerController>();
            theInven = FindObjectOfType<Inventory>();

            thePlayer.transform.position = saveData.PlayerPos;
            thePlayer.transform.eulerAngles = saveData.PlayerRot;

            for(int i = 0; i < saveData.invenItemName.Count; i++)
            {
                theInven.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemNumber[i]);
            }

            Debug.Log("로드 완료");
        }
        else
        {
            Debug.Log("세이브 파일이 없음");
        }
    }
}
