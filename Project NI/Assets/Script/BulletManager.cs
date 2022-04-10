using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float playerShotSpeed; // 발사 속도
    public float SatelliteShotSpeed; // 발사 속도

    public float firingAngle = 45.0f;  //각도
    public float gravity = 9.8f;  //중력
    public float weight = 1.3f;   //가중치값

    // 플레이어의 발사 발사 코루틴
    IEnumerator Shot(Vector3[] shotVecter)
    {
        if(this.gameObject.name == "DirectBullet")
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
            yield return new WaitForSeconds(0.5f);   // 0.5초 대기
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
            gameObject.SetActive(false); // 비활성화
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

            gameObject.SetActive(false); // 비활성화
        }

        gameObject.SetActive(false); // 비활성화
    }

    // 새틀라이트의 발사 코루틴

    IEnumerator SatelliteShot()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 2) break;

            transform.Translate(Vector3.forward * Time.deltaTime * SatelliteShotSpeed); // 탄환 발사
            yield return null; // 코루틴 딜레이 없음
        }

        gameObject.SetActive(false); // 비활성화
    }
}
