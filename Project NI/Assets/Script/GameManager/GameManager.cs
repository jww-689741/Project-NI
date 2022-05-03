using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float limitTime; // 게임 제한시간
    public Text minutes; // 타이머 분 텍스트
    public Text seconds; // 타이머 초 텍스트
    public Text milliseconds; // 타이머 밀리초 텍스트
    public Text scoreUi; // 스코어 UI 텍스트
    private float genaralTime = 0f; // 게임 진행시간

    void FixedUpdate()
    {
        SetTimer();
    }

    // 진행시간 누적 및 제한시간 컨트롤
    private void SetTimer()
    {
        if (genaralTime <= limitTime)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(genaralTime);
            genaralTime += Time.deltaTime; // 진행 시간 누적
            minutes.text = string.Format("{0:00}", timeSpan.Minutes);
            seconds.text = string.Format("{0:00}", timeSpan.Seconds);
            milliseconds.text = string.Format("{0:00}", timeSpan.Milliseconds / 10);
        }
        else
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(limitTime);
            minutes.text = string.Format("{0:00}", timeSpan.Minutes);
            seconds.text = string.Format("{0:00}", timeSpan.Seconds);
            milliseconds.text = string.Format("{0:00}", timeSpan.Milliseconds / 10);
        }
    }

    // 스코어 계산 할 때마다 UI에 값 입력
    private void SetScore()
    {

    }
}
