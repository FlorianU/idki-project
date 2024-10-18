using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   public float currentScore = 0;
   public bool isPaused;
   public bool canInteract;

   public GameObject pauseScreen;
   public GameObject gameOverScreen;
   public GameObject instructionsScreen;
   public TextMeshProUGUI scoreText;
   public TextMeshProUGUI highScoreText;
   public TextMeshProUGUI endScoreText;

   private void Awake()
   {
      if (Instance != null && Instance != this) Destroy(this);
      else Instance = this;
   }

   // Start is called before the first frame update
   void Start()
   {
      TogglePause();
      instructionsScreen.SetActive(true);
   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetButtonDown("Cancel"))
      {
         TogglePauseScreen();
      }

   }

   public void StartGame()
   {
      canInteract = true;
      TogglePause();
      instructionsScreen.SetActive(false);
   }

   public void TogglePauseScreen()
   {
      TogglePause();
      pauseScreen.SetActive(isPaused);
   }


   public void TogglePause()
   {
      isPaused = !isPaused;
      Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
      Cursor.visible = isPaused;
      Time.timeScale = isPaused ? 0 : 1;
   }

   public void IncreaseScore(float score)
   {
      currentScore += score;
      scoreText.text = currentScore.ToString("0.00") + " $";
      endScoreText.text = currentScore.ToString("0.00") + " $";
   }

   public void EndGame()
   {
      var highscore = PlayerPrefs.GetFloat("highscore");
      if (currentScore > highscore)
      {
         highscore = currentScore;
         PlayerPrefs.SetFloat("highscore", highscore);
         PlayerPrefs.Save();
      }

      highScoreText.text = "Highscore: " + highscore.ToString("0.00") + " $";
      gameOverScreen.SetActive(true);
      TogglePause();
   }

   public void DisableInteraction()
   {
      canInteract = false;
   }
}
