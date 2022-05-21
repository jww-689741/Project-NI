using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤

    // 게임 내 스코어, 리워드
    public float score = 0; // 스코어
    public float price = 0; // 리워드

    // 딜 계산 필드
    public float pAttack; // 플레이어 공격력 계수
    public float pSpeed; // 플레이어 공격속도 계수
    public float eAttack; // 적성 개체 공격력 계수
    public float eSpeed; // 적성 개체 공격속도 계수
    public float bSpeed; // 탄환 공격속도 상수

    // 게임 기본 설정 값
    public float limitTime; // 게임 제한시간

    // UiManager로 이동할 대상
    public Text minutes; // 타이머 분 텍스트
    public Text seconds; // 타이머 초 텍스트
    public Text milliseconds; // 타이머 밀리초 텍스트
    public GameObject missileCart; // 미사일 발사 오브젝트
    public Text missileCartUi; // 미사일 카트리지 UI
    public Text ScoreUi; // 스코어 UI

    private float currentTime = 0f; // 게임 진행시간
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
    }

    void FixedUpdate()
    {
        control();
    }

    // 데미지 값 반환 메소드 // 매개변수는 데미지를 적용 할 대상 0 : 플레이어, 1 : 적성 개체
    public float GetDamage(int target, float defanse, float attack)
    {
        if(defanse == 0) // 예외처리
        {
            return -1;
        }
        else if(target == 0) // 플레이어 대상 데미지 계산
        {
            var damage = (1 - defanse) * (attack + (attack * eAttack));
            eAttack = 0;
            return damage; // 데미지 값 반환
        }
        else // 적성 개체 대상 데미지 계산
        {
            var damage = (1 - defanse) * (attack + (attack * pAttack));
            pAttack = 0;
            return damage; // 데미지 값 반환
        }
    }

    // 진행시간 누적 및 제한시간 컨트롤
    private void SetTimer()
    {
        if (currentTime <= limitTime)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            currentTime += Time.deltaTime; // 진행 시간 누적
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
