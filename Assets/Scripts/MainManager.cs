using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text IngameNameText;
    public Text BestScoreText;
    public Text BestScoreNameText;
    public GameObject brick;
    
    private bool m_Started = false;
    private int m_Points;
    private int BestScore;
    public string BestScoreNameMain;
    
    private bool m_GameOver = false;

    public MainManagerMenu MainManagerMenuObj;

    public string PlayerNameIngame;

    
    // Start is called before the first frame update
    void Start()
    {
        MainManagerMenuObj = FindObjectOfType<MainManagerMenu>();

        PlayerNameIngame = MainManagerMenuObj.PlayerName;
        IngameNameText.text = ("Your Name: " + PlayerNameIngame);

        BestScoreNameMain = MainManagerMenuObj.BestScoreNameMenu;
        BestScoreNameText.text = ("Name: " + MainManagerMenuObj.BestScoreNameMenu);
        BestScoreText.text = ("Best Score: " + MainManagerMenuObj.BestScoreMenu);

        BestScore = MainManagerMenuObj.BestScoreMenu;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        StartCoroutine("CheckForBlocks");
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (m_Points > MainManagerMenuObj.BestScoreMenu)
        {
            BestScore = m_Points;
            BestScoreNameMain = MainManagerMenuObj.PlayerName;
            BestScoreText.text = ("Best Score: " + BestScore);
            BestScoreNameText.text = ("Name: " + BestScoreNameMain);
            MainManagerMenuObj.BestScoreMenu = (BestScore);
            MainManagerMenuObj.BestScoreNameMenu = (BestScoreNameMain);
            MainManagerMenuObj.SaveBestScoreInfo();
        }
    }

    public void ReplenBlocks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    IEnumerator CheckForBlocks()
    {
        yield return new WaitForSeconds(1);
        brick = GameObject.Find("BrickPrefab(Clone)");
        Debug.Log(brick);
        if (brick = null)
        {
            ReplenBlocks();
        }
    }
}
