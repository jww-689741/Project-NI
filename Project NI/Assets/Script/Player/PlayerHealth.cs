using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject deadPopup;
    public GameObject explosion; // �ı� ȿ��

    [SerializeField]
    private Image content; // HP�� UI

    [SerializeField]
    private Text contentText; // HP�� �ؽ�Ʈ

    [SerializeField]
    private Stat hp; // ü�� ��

    [SerializeField]
    private float maxHp; // ü�� �ִ밪

    [SerializeField]
    private Stat defense; // ����� ��

    [SerializeField]
    private float maxDefense; // ����� �ִ밪

    [SerializeField]
    private float currentDefense; // ����� ���簪

    // ���� �ӵ�
    [SerializeField]
    private float lerpSpeed;

    private void Start()
    {
        hp.SetDefaultStat(maxHp, maxHp); // ü�� �� �ʱ�ȭ
        defense.SetDefaultStat(currentDefense, maxDefense);
        contentText.text = hp.currentValue + " / " + hp.maxValue;
    }
    // ��� ����
    private void Update()
    {
        // �� ��ȭ �� ������ �϶�
        if (hp.GetRatio() != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, hp.GetRatio(), Time.deltaTime * lerpSpeed);
            contentText.text = hp.currentValue + " / " + hp.maxValue;
        }
        if (hp.GetRatio() <= 0)
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            this.gameObject.SetActive(false);
            Invoke("Dead", 1.5f);
        }
    }

    // ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            GameManager.instance.pAttack = GetComponent<PlayerAttack>().GetAttack();
            if(other.GetComponent<DirectBullet>() != null)
            {
                hp.currentValue -= GameManager.instance.GetDamage(0, defense.currentValue, other.GetComponent<DirectBullet>().GetAttackDamageToBullet());
            }
            else if (other.GetComponent<ChaserBullet>() != null)
            {
                hp.currentValue -= GameManager.instance.GetDamage(0, defense.currentValue, other.GetComponent<ChaserBullet>().GetAttackDamageToBullet());
            }
            else if (other.GetComponent<MissileBomb>() != null)
            {
                hp.currentValue -= GameManager.instance.GetDamage(0, defense.currentValue, other.GetComponent<MissileBomb>().GetAttackDamageToBullet());
            }
            else if (other.GetComponent<HowitzerBullet>() != null)
            {
                hp.currentValue -= GameManager.instance.GetDamage(0, defense.currentValue, other.GetComponent<HowitzerBullet>().GetAttackDamageToBullet());
            }
            else if (other.GetComponent<SpinnerBullet>() != null)
            {
                hp.currentValue -= GameManager.instance.GetDamage(0, defense.currentValue, other.GetComponent<SpinnerBullet>().GetAttackDamageToBullet());
            }
            other.gameObject.SetActive(false);
        }
        else if (hp.GetRatio() > 0 && other.gameObject.CompareTag("RecItem"))
        {
            Recovery();
        }
    }

    // ȸ�� �� ���� �޼ҵ�
    // ȸ���� �� ȣ�� �ʿ�
    public void Recovery()
    {
        hp.currentValue += 10;
    }

    // ��� �� ���� �޼ҵ�
    public void Dead()
    {
        Time.timeScale = 0;
        deadPopup.SetActive(true);
    }
}
