using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMoveScript : MonoBehaviour
{
  [SerializeField] float _speed = 3.0f;
  [SerializeField] float _turnSpeed = 2.5f;
  [SerializeField] float _obstacleRange = 0.025f;
  [SerializeField] float _alertTime = 50.0f;
  [SerializeField] float _boostDuration = 2.0f;
  [SerializeField] float _nonBoost = 10.0f;
  [SerializeField] GameObject _visionCone; //detects player if they walk into it and starts alert phase
  [SerializeField] GameObject _buttSphere; //turning off the butt by player pressing e
  [SerializeField] private AudioClip[] audioClips;
  [SerializeField] private AudioClip _quietSound;
  [SerializeField] private AudioSource audioSource;

  private readonly float _sphereRadius = 0.75f;
  private float _turnTimer; //how long it takes to turn
  private float _realSpeed; //actual speed with or without boost
  private float _realTurn; //actual speed of turn with or without boost
  private float _fasterTurn;
  private bool _isAlive; //lets roomba move
  private bool _isAlert; //extra behavior, when alert there is periodic speed boost using coroutine
  private bool _isBoosting;
  private bool _isntTurning = true;
    // Start is called before the first frame update
    void Start()
    {
      SetAlive(true);
      SetAlert(false);
      _realSpeed = _speed;
      _realTurn = _turnSpeed;
      _fasterTurn = _turnSpeed * 1.5f;

      StartCoroutine(PeriodicBoost()); //boost is always ongoing, just switching speed depending on if Alert
                                      //is this the most efficient to have it always on? no clue
    }

    // Update is called once per frame
    void Update()
    {
      if (_isAlive)
      {
        if (_isntTurning) //when not turning, move
          {
            transform.Translate(0, 0, _realSpeed * Time.deltaTime); //shmooving
            Ray ray = new(transform.position, transform.forward);
            // Cast a sphere in the direction of the ray and see if it collides
            // with the walls.
            //had to disable as error: ray would hit vision radius cone, causing constant rotation - fixed by making cone ignored by raycast
            if (Physics.SphereCast(ray, _sphereRadius, out RaycastHit hit))
            {
              GameObject hitObj = hit.transform.gameObject;
              if (hit.distance < _obstacleRange) //When wall is too close, turn slowly
              {
                  float theta = Random.Range(90, 270);
                  transform.Translate(0,0,0);
                  _isntTurning = false;
                  StartCoroutine(TurnDude(theta));
              }
            }
          }
        }
      }

    public IEnumerator TurnDude (float degrees)//coroutine that makes roombas turn slowly
      {
        if(_isAlert)
        {
          _realTurn = _fasterTurn;
          Debug.Log("fast turns!");
        }
        else
        {
          _realTurn = _turnSpeed;
          Debug.Log("normal");
        }
        while(_turnTimer < _realTurn)
        {
          transform.Rotate(0, degrees * (Time.deltaTime / _realTurn), 0);
          _turnTimer += Time.deltaTime;
        //  Debug.Log("time is at" + _turnTimer);
          yield return null;
        }
        _turnTimer = 0.0f; //back to 0 for next turning
        _isntTurning = true;
        yield return new WaitForSeconds(1.0f);
      }
    public IEnumerator AlertTime() //coroutine for determining how long alert state will be
    //before returning to normal
      {
        float a = 0.0f;//local var
        while(a < _alertTime)
        {
          a += Time.deltaTime; //once a hits total time, stop alert
          //  Debug.Log("alert time is at" + a);
          yield return null;
        }
        SetAlert(false);
        yield return new WaitForSeconds(10.0f); //no alerts for 10 secs after even if player is seen
      }

    public IEnumerator PeriodicBoost()
    {
      while(_isAlive) //skip whole thing if dead
      {
        if(_isAlert)
        {
            // Apply boost speed
            _realSpeed *= 1.5f; //this is fine because after this it's set back to normal
            //       Debug.Log("Boosting!");
            AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.clip = randomClip;
            audioSource.Play();
            yield return new WaitForSeconds(_boostDuration); // boost for duration
         }
      //   Debug.Log("Normal...");
           _realSpeed = _speed; //in case Alert time ends while still boosting
           audioSource.clip = _quietSound;
           audioSource.Play();
           yield return new WaitForSeconds(_nonBoost);
      }
      yield return new WaitForSeconds(1.0f); //end coroutine if dead
    }
    //starter methods
    public void SetAlive(bool alive)
    {
        _isAlive = alive;
    }

    public void SetAlert(bool alert)
    {
      _isAlert = alert;
      if (_isAlive && alert) //if alive and alert is true alive (is this needed, or can it be done in the update?)
      {                      //alive first since if alive, no need to worry abt alert
        AlertTime();
      }
    }
}
