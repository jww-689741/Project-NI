using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float limitTime; // ���� ���ѽð�
    public Text minutes; // Ÿ�̸� �� �ؽ�Ʈ
    public Text seconds; // Ÿ�̸� �� �ؽ�Ʈ
    public Text milliseconds; // Ÿ�̸� �и��� �ؽ�Ʈ
    public Text scoreUi; // ���ھ� UI �ؽ�Ʈ
    private float genaralTime = 0f; // ���� ����ð�

    void FixedUpdate()
    {
        SetTimer();
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

    }
}
