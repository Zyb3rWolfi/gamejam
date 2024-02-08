using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class backgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _background_prefab;
    [SerializeField] private Vector3 _spawnCo;

    [SerializeField] private float speed;

    [SerializeField] bool _move = false;

    private void OnEnable()
    {
        gameManager.moveBackground += manageMoveBackground;
    }

    private void OnDisable()
    {
        gameManager.moveBackground -= manageMoveBackground;
    }

    private void manageMoveBackground()
    {
        _move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_move)
        {
            _background.transform.Translate(new Vector3(speed, 0, 0));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("barrier"))
        {
            return;
        }
        print("exited");
        Instantiate(_background_prefab, _spawnCo, Quaternion.identity);
    }
}
