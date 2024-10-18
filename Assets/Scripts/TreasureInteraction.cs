using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureInteraction : MonoBehaviour
{
   public ParticleSystem vanishingParticles;
   public AudioSource audioSource;
   public float value;
   private GameManager gameManager;

   // Start is called before the first frame update
   void Start()
   {
      gameObject.GetComponent<Outline>().enabled = false;
      gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
   }

   // Update is called once per frame
   void Update()
   {

   }

   private void OnTriggerEnter(Collider other)
   {

   }

   void OnTriggerStay(Collider other)
   {
      if (GameManager.Instance.canInteract)
      {
         // Collide with camera collider
         if (other.CompareTag("MainCamera"))
         {
            // Enable outline
            gameObject.GetComponent<Outline>().enabled = true;

            if (Input.GetMouseButtonDown(0))
            {
               // Make loot disappear on click
               audioSource.Play(0);
               vanishingParticles.Play();
               gameObject.SetActive(false);
               gameManager.IncreaseScore(value);
            }
         }
      }
   }

   void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("MainCamera"))
      {
         gameObject.GetComponent<Outline>().enabled = false;
      }
   }
}
