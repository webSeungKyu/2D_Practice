using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true;
    public float gameTime = 0;
    public bool isTimeOver = false;
    public float displayTime = 0;

    float nowTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (isCountDown)
        {
            displayTime = gameTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeOver == false)
        {
            nowTime += Time.deltaTime;
            if (isCountDown)
            {
                displayTime = gameTime - nowTime;
                if(displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            }
        }
        else
        {
            displayTime = nowTime;
            if(displayTime >= gameTime)
            {
                displayTime = gameTime;
                isTimeOver = true;
            }
        }
    }
}
