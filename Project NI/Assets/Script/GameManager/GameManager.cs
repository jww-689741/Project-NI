using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱���

    // ���� �� ���ھ�, ������
    public float score = 0; // ���ھ�
    public float price = 0; // ������

    // �� ��� �ʵ�
    public float pAttack; // �÷��̾� ���ݷ� ���
    public float pSpeed; // �÷��̾� ���ݼӵ� ���
    public float eAttack; // ���� ��ü ���ݷ� ���
    public float eSpeed; // ���� ��ü ���ݼӵ� ���
    public float bSpeed; // źȯ ���ݼӵ� ���

    // ���� �⺻ ���� ��
    public float limitTime; // ���� ���ѽð�

    // UiManager�� �̵��� ���
    public Text minutes; // Ÿ�̸� �� �ؽ�Ʈ
    public Text seconds; // Ÿ�̸� �� �ؽ�Ʈ
    public Text milliseconds; // Ÿ�̸� �и��� �ؽ�Ʈ
    public GameObject missileCart; // �̻��� �߻� ������Ʈ
    public Text missileCartUi; // �̻��� īƮ���� UI
    public Text ScoreUi; // ���ھ� UI

    private float currentTime = 0f; // ���� ����ð�
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

    // ������ �� ��ȯ �޼ҵ� // �Ű������� �������� ���� �� ��� 0 : �÷��̾�, 1 : ���� ��ü
    public float GetDamage(int target, float defanse, float attack)
    {
        if(defanse == 0) // ����ó��
        {
            return -1;
        }
        else if(target == 0) // �÷��̾� ��� ������ ���
        {
            var damage = (1 - defanse) * (attack + (attack * eAttack));
            eAttack = 0;
            return damage; // ������ �� ��ȯ
        }
        else // ���� ��ü ��� ������ ���
        {
            var damage = (1 - defanse) * (attack + (attack * pAttack));
            pAttack = 0;
            return damage; // ������ �� ��ȯ
        }
    }

    // ����ð� ���� �� ���ѽð� ��Ʈ��
    private void SetTimer()
    {
        if (currentTime <= limitTime)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            currentTime += Time.deltaTime; // ���� �ð� ����
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
