using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.InputSystem;
using Random=UnityEngine.Random;
using UnityEngine.InputSystem;
public class playerController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _force;
    [SerializeField] private float delay;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject _debrisPrefab;
    private bool _canPress = false;
    private bool penaltyActive = false;
    private bool playerTimout = false;
    
    public static event Action<string, bool> perfectClick;
    public static event Action<string, GameObject> targetDebris;
    public static event Action<string> playerWon;

    // Start is called before the first frame update
    void Start()
    {
        // Getting the player object and rigidbody
        _rb = _player.GetComponent<Rigidbody2D>();
    }

    private void manageSlider(bool canPress, GameObject player) {
        _canPress = canPress;
        
    }
    private void OnEnable()
    {
        
        sliderLogic.onCenter += manageSlider;
    }

    private void OnDisable()
    {
        
        sliderLogic.onCenter -= manageSlider;
    }

    public void player1(InputAction.CallbackContext context) {
        if (context.performed)
        {
            if (_canPress)
            {
                if (!playerTimout)
                {
                    StartCoroutine(pressedPerfectly(delay));
                }
            } else {
                if (!penaltyActive) {
                    StartCoroutine(penaltyTimeout(delay));
                }
            }
            
        }
    }


    private IEnumerator penaltyTimeout(float delay)
    {
        text.text = "missed!";
        text.gameObject.SetActive(true);
        float rnd_x = Random.Range(9, 10);
        float rnd_y = Random.Range(0, 3);
        Instantiate(_debrisPrefab, new Vector3(rnd_x, rnd_y, 0), Quaternion.identity);
        penaltyActive = true;
        perfectClick?.Invoke(gameObject.tag, false);
        targetDebris?.Invoke(gameObject.tag, _player);
        yield return new WaitForSeconds(delay);
        text.gameObject.SetActive(false);
        penaltyActive = false;

    }
    private IEnumerator pressedPerfectly(float delay)
        {
            playerTimout = true;
            float amount;
            perfectClick?.Invoke(gameObject.tag, true);
            text.text = "perfect";
            text.gameObject.SetActive(true);
            float rnd = Random.Range(500, 1000);
            _rb.AddForce(new Vector3(rnd, 0, 0));
            yield return new WaitForSeconds(delay);
            text.gameObject.SetActive(false);
            playerTimout = false;
    
        }

    private void OnTriggerEnter2D(Collider2D other)
    {  
       if (other.CompareTag("goal")) {
            playerWon?.Invoke(_player.tag);
       } 
    }
}
