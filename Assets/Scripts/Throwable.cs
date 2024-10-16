using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwable : MonoBehaviour
{
   //public GameObject crosshair1, crosshair2;
   public bool interactable, pickedup;
   public float throwAmount;

   private Transform objTransform, cameraTrans;
   private Rigidbody objRigidbody;
   private Outline outline;

   private void Start()
   {
      objRigidbody = GetComponent<Rigidbody>();
      outline = gameObject.GetComponent<Outline>();
      outline.enabled = false;
      objTransform = gameObject.transform;
      cameraTrans = GameObject.Find("PlayerCamera").transform;
   }

   void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("MainCamera"))
      {
         outline.enabled = true;
         //crosshair1.SetActive(false);
         //crosshair2.SetActive(true);
         interactable = true;
      }
   }
   void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("MainCamera"))
      {
         if (pickedup == false)
         {
            outline.enabled = false;
            //crosshair1.SetActive(true);
            //crosshair2.SetActive(false);
            interactable = false;
         }
         if (pickedup == true)
         {
            objTransform.parent = null;
            objRigidbody.useGravity = true;
            //crosshair1.SetActive(true);
            //crosshair2.SetActive(false);
            interactable = false;
            pickedup = false;
         }
      }
   }
   void Update()
   {
      if (interactable == true)
      {
         if (Input.GetMouseButtonDown(0))
         {
            objTransform.parent = cameraTrans;
            objRigidbody.useGravity = false;
            pickedup = true;
         }
         if (Input.GetMouseButtonUp(0))
         {
            objTransform.parent = null;
            objRigidbody.useGravity = true;
            pickedup = false;
         }
         if (pickedup == true)
         {
            if (Input.GetMouseButtonDown(1))
            {
               objTransform.parent = null;
               objRigidbody.useGravity = true;
               objRigidbody.velocity = cameraTrans.forward * throwAmount * Time.deltaTime;
               pickedup = false;
            }
         }
      }
   }
}