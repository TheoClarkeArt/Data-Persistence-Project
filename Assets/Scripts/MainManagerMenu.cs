using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class MainManagerMenu : MonoBehaviour
{
    public static MainManagerMenu Instance;

    public string PlayerName;

    public TextMeshProUGUI PlayerNameTextMenu;
    public GameObject PlayerNameTextObject;
    public int BestScoreMenu;
    public string BestScoreNameMenu;

    private void Awake()
    {
        PlayerNameTextMenu = PlayerNameTextObject.GetComponent<TextMeshProUGUI>();

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScoreInfo();
    }

    public void OpenMain()
    {
        PlayerName = PlayerNameTextMenu.text;
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    class SaveBestScoreData
    {
        public int BestScoreSaved;
        public string BestScoreNameSaved;
    }

    public void SaveBestScoreInfo()
    {
        SaveBestScoreData data = new SaveBestScoreData();
        data.BestScoreSaved = BestScoreMenu;
        data.BestScoreNameSaved = BestScoreNameMenu;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScoreInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveBestScoreData data = JsonUtility.FromJson<SaveBestScoreData>(json);

            BestScoreMenu = data.BestScoreSaved;
            BestScoreNameMenu = data.BestScoreNameSaved;
        }

        Debug.Log(path);
    }
}
