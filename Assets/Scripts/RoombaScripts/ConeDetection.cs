using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeDetectionScript : MonoBehaviour //trigger script for Cone
{
    private RoombaMoveScript parentScript;
    [SerializeField] AudioSource _emitter;
    [SerializeField] AudioClip _alertSound;

    void Start()
    {
        // Find the parent script component
        parentScript = GetComponentInParent<RoombaMoveScript>();
    }
    void OnTriggerEnter(Collider other) //if collide with player set parentscript value to Alert
  {
    Debug.Log("collision with " + other.tag);
      if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
      {
        Debug.Log("player has been seen!"); //why is it not working
          // Check if the parent script is assigned
          if (parentScript != null)
          {
              _emitter.clip = _alertSound;
              _emitter.Play();
              parentScript.SetAlert(true); // Update the boolean value in the parent script
          }
      }
  }//no need for triggerexit as all that matters is if player was seen

}
