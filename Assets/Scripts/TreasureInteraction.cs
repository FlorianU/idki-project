using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureInteraction : MonoBehaviour
{
   public ParticleSystem vanishingParticles;
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
      if (other.CompareTag("MainCamera"))
      {
         gameObject.GetComponent<Outline>().enabled = true;

         if (Input.GetMouseButtonDown(0))
         {
            vanishingParticles.Play();
            // make loot disappear on click
            gameObject.SetActive(false);
            gameManager.IncreaseScore(value);
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
