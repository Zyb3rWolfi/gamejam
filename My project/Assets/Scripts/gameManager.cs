using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    public static event Action moveBackground;
    private void Start()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        Time.timeScale = 0f;
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
