using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Rigidbody m_rigid = null;         //rigidbody 변수
    Transform m_tfTarget = null;      //transform 변수

    [SerializeField] float m_speed = 0f; //미사일 최고속도
    float m_currentSpeed = 0f;           //미사일 현재속도
    [SerializeField] LayerMask m_layerMask = 0; //원하는 레이어만 검출해주는 LayerMask 선언
    [SerializeField] ParticleSystem m_psEffect = null; //미사일 파티클 시스템 변수

    void SearchEnemy()  //표적 탐지 함수
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, 100f, m_layerMask);  //반경 100m내의 특정 레이어 컬라이더 검출

        if(t_cols.Length > 0)  //검출된 것들 중 하나를 랜덤으로 표적 설정
        {
            m_tfTarget = t_cols[Random.Range(0, t_cols.Length)].transform;
        }
    }

    IEnumerator LaunchDelay() //코루틴
    {
        yield return new WaitUntil(() => m_rigid.velocity.y < 0f);  //velocity의 y값이 0이하가 될 떄까지 대기
        //yield return new WaitForSeconds(0.1f);  //0.1초 대기

        SearchEnemy(); //적 탐지
        m_psEffect.Play();  //파티클 시작

        yield return new WaitForSeconds(5f);   //5초가 지나도 아무일 없으면 미사일 파괴
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
        StartCoroutine(LaunchDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if(m_tfTarget != null)
        {
            if (m_currentSpeed <= m_speed)  //현재 속도가 최대속도 이하일때 최대속도까지 가속
                m_currentSpeed += m_speed * Time.deltaTime;

            transform.position += transform.up * m_currentSpeed * Time.deltaTime;  //표적이 있으면 미사일 위로 가속

            Vector3 t_dir = (m_tfTarget.position - transform.position).normalized; //표적위치 - 미사일 위치 = 방향과 거리 산출 normalized로 방향만 남김
            transform.up = Vector3.Lerp(transform.up, t_dir, 0.25f);   //미사일 y축(머리)를 해당 방향으로 설정
        }
    }


    private void OnCollisionEnter(Collision collision)  //태그가 Enemy인 컬라이더와 충돌하면 둘다 삭제
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
