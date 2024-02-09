using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random=UnityEngine.Random;

public class sliderLogic : MonoBehaviour
{
    [SerializeField] private GameObject _slider;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _parentObject;

    private bool _canPress = false;
    public float _score = 1;
    public static event Action<bool, GameObject> onCenter;

    public float shakeDuration = 0.5f; // Duration of the shake effect
    public float shakeIntensity = 0.1f; // Intensity of the shake effect

    private Vector3 originalPosition; // Original position of the object
    private float currentShakeDuration = 0f; 

    private void OnEnable()
    {
        playerController.perfectClick += OnPerfectHit;
    }

    private void OnDisable()
    {
        playerController.perfectClick -= OnPerfectHit;
    }

    private void Start()
    {
        originalPosition = _parentObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _slider.transform.Translate(_velocity * Time.deltaTime);

        if (currentShakeDuration > 0)
        {
            // Generate a random offset within the intensity range
            Vector2 randomOffset = Random.insideUnitCircle * shakeIntensity;

            // Apply the offset to the object's position
            _parentObject.gameObject.transform.position = originalPosition + new Vector3(randomOffset.x, randomOffset.y, 0);

            // Reduce the remaining shake duration
            currentShakeDuration -= Time.deltaTime;
        }
        else
        {
            // Reset the object's position to its original position
            _parentObject.gameObject.transform.position = originalPosition;
        }
    }
    public void Shake()
    {
        currentShakeDuration = shakeDuration; // Start the shake effect
    }

    private void OnPerfectHit(string tag, bool perfectHit)
    {
        if (CompareTag(tag))
        {
            switch (perfectHit)
            {
                case true:
                {

                    if ( _velocity.x > 13 || _velocity.x < -13)
                    {
                        return;
                    }
                    float rnd = Random.Range(1.0f, 1.2f);
                    _velocity *= rnd;
                    break;
                }
                case false:
                {
                    Shake();

                    if (_velocity.x < 2 && _velocity.x > 0)
                    {
                        return;
                    }
                    float rnd = Random.Range(0.7f, 0.9f);
                    _velocity *= rnd;
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {   
        if (other.gameObject.CompareTag("goal")) {
            return;
        }
        _velocity.x = -_velocity.x;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        onCenter?.Invoke(true, _player);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        onCenter?.Invoke(false, _player);
    }
}
