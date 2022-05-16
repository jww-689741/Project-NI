using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //�̱���
    public float limitTime; // ���� ���ѽð�
    public Text minutes; // Ÿ�̸� �� �ؽ�Ʈ
    public Text seconds; // Ÿ�̸� �� �ؽ�Ʈ
    public Text milliseconds; // Ÿ�̸� �и��� �ؽ�Ʈ
    public GameObject missileCart; // �̻��� �߻� ������Ʈ
    public Text missileCartUi; // �̻��� īƮ���� UI
    public Text ScoreUi; // ���ھ� UI
    public GameObject pauseUi;
    private float genaralTime = 0f; // ���� ����ð�
    private float score = 0; // ���ھ�
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

    // ����ð� ���� �� ���ѽð� ��Ʈ��
    private void SetTimer()
    {
        if (genaralTime <= limitTime)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(genaralTime);
            genaralTime += Time.deltaTime; // ���� �ð� ����
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

    // ���ھ� ��� �� ������ UI�� �� �Է�
    private void SetScore()
    {
        ScoreUi.text = score.ToString();
    }

    // ��ź�߻� ī��Ʈ ����
    private void SetBoom()
    {
        missileCartUi.text = "X " + (missileCart.GetComponent<MissileLauncher>().GetCart()).ToString();
    }

}
