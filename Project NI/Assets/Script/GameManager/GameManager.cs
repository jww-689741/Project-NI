using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
<<<<<<< HEAD
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

=======
    public float limitTime; // ���� ���ѽð�
    public Text minutes; // Ÿ�̸� �� �ؽ�Ʈ
    public Text seconds; // Ÿ�̸� �� �ؽ�Ʈ
    public Text milliseconds; // Ÿ�̸� �и��� �ؽ�Ʈ
    public Text scoreUi; // ���ھ� UI �ؽ�Ʈ
    public Text hpText; // HP �ؽ�Ʈ
    public Image hpBar; // HP ��
    public GameObject missileCart; // �̻��� �߻� ������Ʈ
    public Text missileCartUi; // �̻��� īƮ���� UI
    public Text ScoreUi; // ���ھ� UI
    private float genaralTime = 0f; // ���� ����ð�
    private int totalScore = 0;
    private delegate void Control();
    Control control;

    private void Start()
    {
        control = SetTimer;
        control += SetPlayerHp;
        control += SetBulletCount;
        control += SetBoom;
        control += SetScore;
    }
>>>>>>> origin/Pks
    void FixedUpdate()
    {
        control();
    }

<<<<<<< HEAD
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
=======
    // ����ð� ���� �� ���ѽð� ��Ʈ��
    private void SetTimer()
    {
        if (genaralTime <= limitTime)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(genaralTime);
            genaralTime += Time.deltaTime; // ���� �ð� ����
>>>>>>> origin/Pks
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
<<<<<<< HEAD
        ScoreUi.text = score.ToString();
=======
        totalScore += Lol.score;
        ScoreUi.text = totalScore.ToString();
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
>>>>>>> origin/Pks
    }

    // ��ź�߻� ī��Ʈ ����
    private void SetBoom()
    {
        missileCartUi.text = "X " + (missileCart.GetComponent<MissileLauncher>().GetCart()).ToString();
    }

<<<<<<< HEAD
=======
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
>>>>>>> origin/Pks
}
