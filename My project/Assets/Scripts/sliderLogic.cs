using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class sliderLogic : MonoBehaviour
{
    [SerializeField] private GameObject _slider;
    [SerializeField] private Vector3 _velocity;
    private bool _canPress = false;
    public static event Action<bool> onCenter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _slider.transform.Translate(_velocity * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {   

        _velocity.x = -_velocity.x;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        onCenter?.Invoke(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        onCenter?.Invoke(false);
    }
}
