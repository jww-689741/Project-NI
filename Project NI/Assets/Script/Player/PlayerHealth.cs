using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject deadPopup;
    public GameObject explosion; // 파괴 효과

    [SerializeField]
    private Image content; // HP바 UI

    [SerializeField]
    private Text contentText; // HP바 텍스트

    [SerializeField]
    private Stat hp; // 스탯 값

    [SerializeField]
    private float maxHp; // 체력 최대값

    // 보간 속도
    [SerializeField]
    private float lerpSpeed;

    private void Start()
    {
        hp.SetDefaultStat(maxHp, maxHp); // 체력 값 초기화
        contentText.text = hp.currentValue + " / " + hp.maxValue;
    }
    // 사망 감지
    private void Update()
    {
        // 값 변화 시 게이지 하락
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

    // 판정
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

    // 피격 시 동작 메소드
    // 데미지 값 호출 필요
    public void Damage()
    {
        hp.currentValue -= 100; // 데미지 값 감산
        Debug.Log(hp.currentValue);
    }

    // 회복 시 동작 메소드
    // 회복량 값 호출 필요
    public void Recovery()
    {
        hp.currentValue += 10;
    }

    // 사망 시 동작 메소드
    public void Dead()
    {
        Time.timeScale = 0;
        deadPopup.SetActive(true);
    }
}
