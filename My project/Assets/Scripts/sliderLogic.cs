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
    private bool _canPress = false;
    public float _score = 1;
    public static event Action<bool, GameObject> onCenter;
    // Start is called before the first frame update

    private void OnEnable()
    {
        playerController.perfectClick += OnPerfectHit;
    }

    private void OnDisable()
    {
        playerController.perfectClick -= OnPerfectHit;
    }

    // Update is called once per frame
    void Update()
    {
        _slider.transform.Translate(_velocity * Time.deltaTime);
    }

    private void OnPerfectHit(string tag, bool perfectHit)
    {
        if (CompareTag(tag))
        {
            switch (perfectHit)
            {
                case true:
                {
                    if (_score < 0.8f)
                    {
                        _score = 1.0f;
                        _velocity.x = 4.0f;
                    }

                    if (_score > 1.12 || _velocity.x > 14.3)
                    {
                        return;
                    }
                    float rnd = Random.Range(0.02f, 0.08f);
                    _score += rnd;
                    _velocity *= _score;
                    break;
                }
                case false:
                {
                    if (_score < 0.6f)
                    {
                        break;
                    }

                    if (_velocity.x > 14)
                    {
                        _velocity.x = 4;
                    }
                    float rnd = Random.Range(0.02f, 0.05f);
                    _score -= rnd;
                    _velocity *= _score;
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {   

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
