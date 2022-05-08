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
    public Text hpText; // HP 텍스트
    public Image hpBar; // HP 바
    private float genaralTime = 0f; // 게임 진행시간
    private delegate void Control();
    Control control;

    private void Start()
    {
        control = SetTimer;
        control += SetPlayerHp;
        control += SetBulletCount;
    }
    void FixedUpdate()
    {
        control();
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

    // 체력 게이지 증감
    private void SetPlayerHp()
    {
        var hp = PlayerManager.instance.GetHp();
        var maxhp = PlayerManager.instance.GetComponent<PlayerStatusManager>().GetHP();
        if(hp > 0)
        {
            hpBar.fillAmount = (hp / (maxhp / 100)) / 100;
            hpText.text = hp.ToString();
        }
        else
        {
            hpBar.fillAmount = 0;
            hpText.text = 0.ToString();
        }
    }

    // 전탄발사 카운트 증감
    private void SetBoom()
    {

    }

    // 탄환 소모 카운트 증감
    private void SetBulletCount()
    {
        var bullet = PlayerManager.instance.hasbullets;
        var count = PlayerManager.instance.cartridgeCount;
        if (bullet == 0)
        {
            scoreUi.text = "X ∞";
        }
        else if(bullet == 1){
            scoreUi.text = "X " + count;
        }
        else if (bullet == 2)
        {
            scoreUi.text = "X " + count;
        }
        else if (bullet == 3)
        {
            scoreUi.text = "X " + count;
        }
        else if (bullet == 4)
        {
            scoreUi.text = "X " + count;
        }
    }
}
