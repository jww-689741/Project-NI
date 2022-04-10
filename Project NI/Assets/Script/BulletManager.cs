using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float playerShotSpeed; // 플레이어 발사 속도
    public float satelliteShotSpeed; // 새틀라이트 발사 속도
    public float spinnerBulletSpeed; // 회전탄 발사속도
    public float spinnerBulletInterval = 0.1f; // 회전탄 발사 간격

    public float firingAngle = 45.0f;  //각도
    public float gravity = 9.8f;  //중력
    public float weight = 1.3f;   //가중치값
    
    private Transform m_tfTarget = null; //transform 변수
    private float timeTemp;
    private float m_currentSpeed = 0f;   //미사일 현재속도
    [SerializeField] LayerMask m_layerMask = 0; //원하는 레이어만 검출해주는 LayerMask 선언
    // 플레이어의 발사 발사 코루틴
    IEnumerator Shot(Vector3[] shotVecter)
    {
        if (this.gameObject.name == "DirectBullet")
        {
            Vector3 directionVector = ((shotVecter[0] - shotVecter[1]).normalized); // (마우스 좌표 - 발사 시작지점 좌표).방향벡터
            float timer = 0;
            while (true)
            {
                timer += Time.deltaTime;
                if (timer > 2) break;

                transform.Translate(directionVector * Time.deltaTime * playerShotSpeed); // 탄환 발사
                yield return null; // 코루틴 딜레이 없음
            }
        }
        else if (this.gameObject.name == "HowitzerBullet")
        {
            yield return new WaitForSeconds(0.2f);   // 0.2초 대기
                                                     // 대상거리
            float target_Distance = 20f;

            //지정된 각도에서 대상에 오브젝트를 던지는 데 필요한 속도를 계산
            float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

            // 속도의 X Y 컴포넨트 추출
            float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad) * weight;
            float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad) * weight;

            // 비행 시간을 계산합니다.
            float filghtDuration = target_Distance / Vx + weight;

            // 비행 시간동안 오브젝트 활성화
            float elapse_time = 0;
            while (elapse_time < filghtDuration)
            {
                transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);  //포물선으로 탄환 발사
                elapse_time += Time.deltaTime;
                yield return null;
            }
        }
        else if (this.gameObject.name == "Buckshot")
        {
            Vector3 directionVector = ((shotVecter[0] - shotVecter[1]).normalized); // (마우스 좌표 - 발사 시작지점 좌표).방향벡터
            float timer = 0;
            while (true)
            {
                timer += Time.deltaTime;
                if (timer > 2) break;
                for (int i = 0; i < 5; i++)
                {
                    this.transform.GetChild(i).gameObject.transform.Translate(directionVector * Time.deltaTime * playerShotSpeed); // 탄환 발사
                }
                yield return null; // 코루틴 딜레이 없음
            }
        }
        else if (this.gameObject.name == "SpinnerBullet")
        {
            float timer = 0;
            int spinnerBulletCount = 0;
            while (true)
            {
                timer += Time.deltaTime;
                if (timer < spinnerBulletInterval)
                {
                    this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * spinnerBulletSpeed);
                }
                else if (timer > 2) break;
                else
                {
                    this.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * 30f);
                    if (timer > spinnerBulletInterval)
                    {
                        if (timer > spinnerBulletInterval * 1.5f && spinnerBulletCount < 9)
                        {
                            spinnerBulletCount++;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            this.gameObject.transform.GetChild(spinnerBulletCount).GetChild(i).Translate(Vector3.forward * Time.deltaTime * spinnerBulletSpeed);
                        }
                    }
                }
                yield return null; // 코루틴 딜레이 없음
            }
        }
        else if (this.gameObject.name == "ChaserBullet")
        {
            Vector3 directionVector = ((shotVecter[0] - shotVecter[1]).normalized); // (마우스 좌표 - 발사 시작지점 좌표).방향벡터
            float timer = 0;
            SearchEnemy(); //적 탐지
            while (true)
            {
                timer += Time.deltaTime;
                if (timer > 2) break;
                transform.Translate(directionVector * Time.deltaTime * playerShotSpeed);
                if (m_tfTarget != null)
                {
                    if (m_currentSpeed <= playerShotSpeed)  //현재 속도가 최대속도 이하일때 최대속도까지 가속
                        m_currentSpeed += playerShotSpeed * Time.deltaTime;

                    Vector3 t_dir = (m_tfTarget.position - transform.position).normalized; //표적위치 - 미사일 위치 = 방향과 거리 산출 normalized로 방향만 남김
                    transform.up = Vector3.Lerp(transform.up, t_dir, 0.25f);   //미사일 y축(머리)를 해당 방향으로 설정
                    transform.Translate(transform.up * Time.deltaTime * m_currentSpeed);  //표적이 있으면 미사일 위로 가속
                }
                else
                {
                    transform.Translate(directionVector * Time.deltaTime * playerShotSpeed); // 탄환 발사
                    SearchEnemy(); //적 탐지
                }
                yield return null; // 코루틴 딜레이 없음
            }
        }
        this.gameObject.SetActive(false); // 비활성화
    }

    // 새틀라이트의 발사 코루틴

    IEnumerator SatelliteShot()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 2) break;

            transform.Translate(Vector3.forward * Time.deltaTime * satelliteShotSpeed); // 탄환 발사
            yield return null; // 코루틴 딜레이 없음
        }

        gameObject.SetActive(false); // 비활성화
    }

    // 적탐지 함수
    void SearchEnemy()  //표적 탐지 함수
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, 100f, m_layerMask);  //반경 100m내의 특정 레이어 컬라이더 검출

        if (t_cols.Length > 0)  //검출된 것들 중 하나를 랜덤으로 표적 설정
        {
            m_tfTarget = t_cols[Random.Range(0, t_cols.Length)].transform;
        }
    }
    private void OnCollisionEnter(Collision collision)  //태그가 Enemy인 컬라이더와 충돌하면 둘다 삭제
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
