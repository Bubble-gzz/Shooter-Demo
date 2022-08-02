using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class DataCoinText : MonoBehaviour
{
    // Start is called before the first frame update
    int changeSpeed;
    int currentNumber;
    float _currentNumber;
    Text text;
    void Start()
    {
        currentNumber = GamePlay.dataCoin;
        _currentNumber = currentNumber;
        text = GetComponent<Text>();
        text.text = currentNumber.ToString();
        changeSpeed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) GamePlay.dataCoin += 1000;
        if (Input.GetKeyDown(KeyCode.E)) GamePlay.dataCoin -= 1000;
        if (Input.GetKeyDown(KeyCode.R)) GamePlay.dataCoin += 10;
        int targetNumber = GamePlay.dataCoin;
        //Debug.Log("targetNumber: " + targetNumber + " currentNumber: " + currentNumber);
        if (currentNumber != targetNumber)
        {
            int sign = (currentNumber < targetNumber) ? 1 : -1;
            int gap = Mathf.Abs(targetNumber - currentNumber);
            changeSpeed = Mathf.Max(10, gap * 5);
            float delta = changeSpeed * Time.deltaTime;
            if (gap < delta) _currentNumber = targetNumber;
            else _currentNumber += sign * delta;
            currentNumber = (int)Mathf.Round(_currentNumber);
            text.text = currentNumber.ToString();
        }
    }
}
