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
    public Text hpText; // HP �ؽ�Ʈ
    public Image hpBar; // HP ��
    private float genaralTime = 0f; // ���� ����ð�
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

    // ü�� ������ ����
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

    // ��ź�߻� ī��Ʈ ����
    private void SetBoom()
    {

    }

    // źȯ �Ҹ� ī��Ʈ ����
    private void SetBulletCount()
    {
        var bullet = PlayerManager.instance.hasbullets;
        var count = PlayerManager.instance.cartridgeCount;
        if (bullet == 0)
        {
            scoreUi.text = "X ��";
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
