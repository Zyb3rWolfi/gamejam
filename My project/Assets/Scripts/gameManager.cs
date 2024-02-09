using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _panel;
    public static event Action moveBackground;
    
    public void StartGame() {

        StartCoroutine(Countdown());
    }

    private void Start()
    {
        Time.timeScale = 0f;

    }
    private void OnEnable()
    {
        playerController.playerWon += playerWon;
    }

    private void OnDisable()
    {
        playerController.playerWon -= playerWon;
    }

    private void playerWon(string name) {
        Time.timeScale = 0f;
        _text.text = $"Player {name} Won!";
        _text.gameObject.SetActive(true);
    }

    private IEnumerator Countdown()
    {
        _panel.SetActive(false);
        _text.gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            print(i);
            _text.text = $"{3-i}";
            yield return new WaitForSecondsRealtime(1.0f);
        }
        _text.gameObject.SetActive(false);
        moveBackground?.Invoke();
        Time.timeScale = 1f;

    }
}
