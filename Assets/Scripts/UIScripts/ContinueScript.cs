using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContinueScript : MonoBehaviour
{
  /* Aim of this script: countdown on gameover screen starting from 10,
  once countdown is over, continue button disappears and other buttons move,
  and retry turns into "gameover"*/
  [SerializeField] private TextMeshProUGUI _countdownText;
  [SerializeField] private Button _continueButton;
  [SerializeField] private RectTransform _menuButton; //for moving the two buttons
  [SerializeField] private RectTransform _quitButton;
  private float _timer = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
      if (_countdownText != null)
        {
            StartCoroutine(Continue());
        }
    }
    private IEnumerator Continue()
    {
        float _remainingTime = _timer;
        float _moveButtons = 5f;

        while (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
            _countdownText.text = "Retry? " + Mathf.Ceil(_remainingTime).ToString(); //update with ints
            yield return null; // Wait until the next frame
        }

        _countdownText.text = "Gameover...";
        _continueButton.gameObject.SetActive(false); //continue button disappears
      /*  _remainingTime = 0; //reuse to save
        while (_remainingTime < _moveButtons)
        {


        }*/
    }
}
