using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _force;
    private bool _canPress = false;
    private bool penaltyActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // Getting the player object and rigidbody
        _rb = _player.GetComponent<Rigidbody2D>();
    }

    private void manageSlider(bool canPress, GameObject player) {
            _canPress = canPress;

        print(_player.tag);

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
        } else {
            if (!penaltyActive) {
                StartCoroutine(penaltyTimeout());
            }
        }
    }


    private IEnumerator penaltyTimeout()
    {
        penaltyActive = true;
        float amount;
        amount = Random.Range(-200, -500);
        _rb.AddForce(new Vector3(amount, 0, 0));
        yield return new WaitForSeconds(1);
        penaltyActive = false;

    }
}
