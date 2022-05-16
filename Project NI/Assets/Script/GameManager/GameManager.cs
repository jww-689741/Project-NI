using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //싱글톤
    public float limitTime; // 게임 제한시간
    public Text minutes; // 타이머 분 텍스트
    public Text seconds; // 타이머 초 텍스트
    public Text milliseconds; // 타이머 밀리초 텍스트
    public GameObject missileCart; // 미사일 발사 오브젝트
    public Text missileCartUi; // 미사일 카트리지 UI
    public Text ScoreUi; // 스코어 UI
    public GameObject pauseUi;
    private float genaralTime = 0f; // 게임 진행시간
    private float score = 0; // 스코어
    private bool pauseFlag = false;
    private delegate void Control();
    Control control;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        control = SetTimer;
        control += SetBoom;
        control += SetScore;
        control += Pause;
    }

    void FixedUpdate()
    {
        control();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseFlag)
            {
                Time.timeScale = 0;
                pauseUi.SetActive(true);
                pauseFlag = true;
            }
            else
            {
                Time.timeScale = 1;
                pauseUi.SetActive(false);
                pauseFlag = false;
            }
        }
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
        ScoreUi.text = score.ToString();
    }

    // 전탄발사 카운트 증감
    private void SetBoom()
    {
        missileCartUi.text = "X " + (missileCart.GetComponent<MissileLauncher>().GetCart()).ToString();
    }

}
