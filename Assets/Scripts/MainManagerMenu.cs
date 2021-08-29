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
    }

    public void SaveBestScore()
    {
        SaveBestScoreData data = new SaveBestScoreData();
        data.BestScoreSaved = BestScoreMenu;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {

    }
}
