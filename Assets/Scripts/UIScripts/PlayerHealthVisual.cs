using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthVisual : MonoBehaviour
{
  [SerializeField] GameObject _armature;
  [SerializeField] private TextMeshProUGUI _HP;

  private PlayerHealthScript _getHealth;
  private int _health = 0;
    // Start is called before the first frame update
    void Start()
    {
      _getHealth = _armature.GetComponent<PlayerHealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
      _health = _getHealth.GetHealth();
      _HP.text = "HP: " + _health.ToString();
    }
}
