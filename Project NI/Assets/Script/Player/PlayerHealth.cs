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
    private Stat hp; // ���� ��

    [SerializeField]
    private float maxHp; // ü�� �ִ밪

    // ���� �ӵ�
    [SerializeField]
    private float lerpSpeed;

    private void Start()
    {
        hp.SetDefaultStat(maxHp, maxHp); // ü�� �� �ʱ�ȭ
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
            Damage();
            other.gameObject.SetActive(false);
        }
        else if (hp.GetRatio() > 0 && other.gameObject.CompareTag("RecItem"))
        {
            Recovery();
        }
    }

    // �ǰ� �� ���� �޼ҵ�
    // ������ �� ȣ�� �ʿ�
    public void Damage()
    {
        hp.currentValue -= 100; // ������ �� ����
        Debug.Log(hp.currentValue);
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
