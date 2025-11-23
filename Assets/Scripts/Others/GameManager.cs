using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public float gameDuration = 180f;   // 3 minutes
    public int totalEnemies;            // Set this manually or count at Start

    private float timer;
    public int killedEnemies = 0;
    private bool gameEnded = false;
    
    public TextMeshProUGUI timetext;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject instructionSreen;
    //FX Sound 
    public AudioClip winSound;
    public AudioClip loseSound;

    private void Awake()
    {
        if (Instance == null)
        {
          
            Instance = this;
            
        }
        
    }

    void Start()
    {
        timer = gameDuration;
        StartCoroutine(ToggleInstruction());


    }
    private IEnumerator ToggleInstruction()
    {
       
        instructionSreen.SetActive(true);
        yield return new WaitForSeconds(5);
        instructionSreen.SetActive(false);
    }
    void Update()
    {
        totalEnemies = Object.FindObjectsByType<EnemyStateManager>(FindObjectsSortMode.None).Length;
        if (gameEnded) return;

        timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timetext.text = $"{minutes:0}:{seconds:00}";

        if (timer <= 0f)
        {
            timetext.text = "0:00";
            LoseGame();
        }
        if (totalEnemies == 0)
        {
            WinGame();
        }

    }



    public void WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;   

        winScreen.SetActive(true);
        Time.timeScale = 0f;
        SoundFXManager.Instance.PlaySound(winSound, transform);
    }


    public void LoseGame()
    {
        if (gameEnded) return;

        gameEnded = true;   
        loseScreen.SetActive(true);
        SoundFXManager.Instance.PlaySound(loseSound, transform);
        Time.timeScale = 0f;
    }

}
