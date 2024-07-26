using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
  [SerializeField] int _playerHealth = 3;
  [SerializeField] int _damage = 1;
  [SerializeField] string _gameOverScene;
    // Start is called before the first frame update
    void Start()
    {

    }
// should I include a healing script? so you can hide and heal, would need a coroutine of some sort that only activates when health is less than 5
    // Update is called once per frame
    void Update()
    {
        if(_playerHealth <= 0)
        {
          SceneManager.LoadScene(_gameOverScene);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
      //Debug.Log("Colliding with " + other.tag);
      if(other.CompareTag("HurtBox"))
      {
        _playerHealth -= _damage;
      //  Debug.Log("Health is " + _playerHealth);
      }
    }
    public int GetHealth()
    {
      return _playerHealth;
    }
}
