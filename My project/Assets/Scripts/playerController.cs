using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _force;
    private bool _canPress = false;


    // Start is called before the first frame update
    void Start()
    {
        // Getting the player object and rigidbody
        _rb = _player.GetComponent<Rigidbody2D>();
    }

    private void manageSlider(bool canPress) {

        _canPress = canPress;
        print(_canPress);

    }
    private void OnEnable()
    {
        
        sliderLogic.onCenter += manageSlider;
    }

    private void OnDisable()
    {
        
        sliderLogic.onCenter -= manageSlider;
    }

    public void player1() {
        if (_canPress) {
            _rb.AddForce(new Vector3(_force, 0, 0));
        }
    }

}
