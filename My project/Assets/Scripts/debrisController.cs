using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debrisController : MonoBehaviour
{
    [SerializeField] private GameObject _debris;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed = 5;
    private GameObject _target;
    private bool _targetEnable = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = _debris.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetEnable) {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);   
        }
    }

    private void OnEnable()
    {
        playerController.targetDebris += manageEvent;
    }

    private void OnDisable()
    {
        playerController.targetDebris -= manageEvent;
    }

    private void manageEvent(string tag, GameObject player) {
        if (tag == player.tag && !_targetEnable) {
            _target = player;
            _targetEnable = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2")) {
            StartCoroutine(waitForDestroy());
        }
    }

    private IEnumerator waitForDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);

    }
}
