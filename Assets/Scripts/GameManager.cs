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

   public GameObject pauseScreen;
   public GameObject gameOverScreen;
   public TextMeshProUGUI scoreText;
   public TextMeshProUGUI endScoreText;

   private void Awake()
   {
      if (Instance != null && Instance != this) Destroy(this);
      else Instance = this;
   }

   // Start is called before the first frame update
   void Start()
   {
   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetButtonDown("Cancel"))
      {
         TogglePauseScreen();
      }

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
      gameOverScreen.SetActive(true);
      TogglePause();
   }
}
