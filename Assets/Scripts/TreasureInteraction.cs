using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureInteraction : MonoBehaviour
{
    public ParticleSystem vanishingParticles;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Outline>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) { 
            vanishingParticles.Play();
            // make loot disappear on click
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    private void OnMouseEnter()
    {
        // enable outline when mouse hovers over object
        gameObject.GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit()
    {
        // disable outline when mouse leaves object
        gameObject.GetComponent<Outline>().enabled = false;

    }
}
