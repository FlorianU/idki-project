using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private float currentScore = 0;

   public TextMeshProUGUI scoreText;

   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {

   }

   public void IncreaseScore(float score)
   {
      currentScore += score;
      scoreText.text = currentScore.ToString("0.00") + " $";
   }
}
