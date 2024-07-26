using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningSceneTransition : MonoBehaviour
{
  // Set this in the Inspector to specify the name of the scene to load
   [SerializeField] string _winningScene;

   private void OnTriggerEnter(Collider other)
   {
       if (other.CompareTag("Player")) // check if player
       {
           // Load the specified scene
           SceneManager.LoadScene(_winningScene);
       }
   }
}
