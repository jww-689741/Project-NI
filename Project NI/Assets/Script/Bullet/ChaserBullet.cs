using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ChaserBullet : MonoBehaviour
{
    [SerializeField] LayerMask m_layerMask = 0; //원하는 레이어만 검출해주는 LayerMask 선언
    private Transform m_tfTarget = null; //transform 변수
    private float m_currentSpeed = 0f;   //미사일 현재속도

    public float GetAttackDamageToBullet()
    {
        var status = GetComponent<ChaserBulletStatusManager>(); // 탄환의 스탯 데이터 접근
        return status.GetAttackDamage();
    }

    // 발사 추상 코루틴
    public IEnumerator Shot(Vector3 directionVector)
    {
        var status = GetComponent<ChaserBulletStatusManager>(); // 직사탄환의 스탯 데이터 접근
        float timer = 0;
        //SearchEnemy(); //적 탐지
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > status.GetHoldingTime()) break;
            transform.forward = Vector3.Lerp(transform.forward, directionVector, 0.25f);
            transform.Translate(transform.forward * Time.deltaTime * status.GetShotSpeed());
            if (m_tfTarget != null)
            {
                if (m_currentSpeed <= status.GetShotSpeed())  //현재 속도가 최대속도 이하일때 최대속도까지 가속
                    m_currentSpeed += status.GetShotSpeed() * Time.deltaTime;

                Vector3 t_dir = (m_tfTarget.position - transform.position).normalized; //표적위치 - 미사일 위치 = 방향과 거리 산출 normalized로 방향만 남김
                transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.25f);   //미사일 y축(머리)를 해당 방향으로 설정
                transform.Translate(transform.forward * Time.deltaTime * m_currentSpeed);  //표적이 있으면 미사일 위로 가속
            }
            else
            {
                transform.Translate(directionVector * Time.deltaTime * status.GetShotSpeed()); // 탄환 발사
                SearchEnemy(); //적 탐지
            }
            yield return null; // 코루틴 딜레이 없음
        }

        this.gameObject.SetActive(false); // 비활성화
    }
    void SearchEnemy()  //표적 탐지 함수
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, 50f, m_layerMask);  //반경 50m내의 특정 레이어 컬라이더 검출

        if (t_cols.Length > 0)  //검출된 것들 중 하나를 랜덤으로 표적 설정
        {
            m_tfTarget = t_cols[Random.Range(0, t_cols.Length)].transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            // Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
