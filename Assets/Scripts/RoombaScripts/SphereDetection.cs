using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDetectionScript : MonoBehaviour //trigger script for sphere
{
    private RoombaMoveScript parentScript;
    private AudioSource audioSrc;
    private bool _playerClose;
    [SerializeField] private float _duration = 2f;

    void Start()
    {
        // Find the parent script component
        parentScript = GetComponentInParent<RoombaMoveScript>();
        audioSrc = GetComponentInParent<AudioSource>();
        _playerClose = false;
    }
    void Update()
    {
      if(_playerClose && Input.GetKey(KeyCode.E)) //if close and press e
      {
        parentScript.SetAlive(false); //kill roomba :(
        StartCoroutine(FadeOut(audioSrc,_duration)); //when  turn off, engine turns off slowly
      }
    }
    void OnTriggerEnter(Collider other)
    {
      if(other.CompareTag("Player")) //if player
      {
        if(parentScript!= null) //if assigned properly
        {
          _playerClose = true; //let player turn off with E
        }
      }
    }
    void OnTriggerExit(Collider other)
    {
      if(other.CompareTag("Player")) //if player
      {
        if(parentScript!= null) //if assigned properly
        {
          _playerClose = false; //no more turning off roomba :)
        }
      }
    }
    public IEnumerator FadeOut(AudioSource source, float duration)
   {
       if (source == null)
       {
           Debug.LogWarning("AudioSource is not assigned.");
           yield break;
       }

       float startVolume = source.volume; // Store the starting volume
       float elapsedTime = 0f;

       while (elapsedTime < duration)
       {
           elapsedTime += Time.deltaTime; // Increment elapsed time
           float newVolume = Mathf.Lerp(startVolume, 0f, elapsedTime / duration); // Calculate the new volume
           source.volume = newVolume; // Apply the new volume
           yield return null; // Wait until the next frame
       }

       source.volume = 0f; // Ensure the volume is set to 0 at the end
   }
}
