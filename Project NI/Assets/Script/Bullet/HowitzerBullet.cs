using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowitzerBullet : MonoBehaviour, IBulletShot
{
    public float GetAttackSpeedToBullet()
    {
        var status = GetComponent<HowitzerBulletStatusManager>(); // 곡사탄환의 스탯 데이터 접근
        return status.GetAttackSpeed();
    }

    public IEnumerator Shot()
    {
        var status = GetComponent<HowitzerBulletStatusManager>(); // 곡사탄환의 스탯 데이터 접근
        var gravity = -Physics.gravity.y; // 중력값
        yield return new WaitForSeconds(0.01f);   // 0.2초 대기

        float target_Distance = 50f; // 대상거리

        //지정된 각도에서 대상에 오브젝트를 던지는 데 필요한 속도를 계산
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * status.GetFiringAngle() * Mathf.Deg2Rad) / gravity);

        // 속도의 X Y 컴포넨트 추출
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(status.GetFiringAngle() * Mathf.Deg2Rad) * status.GetWeight();
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(status.GetFiringAngle() * Mathf.Deg2Rad) * status.GetWeight();

        // 비행 시간을 계산합니다.
        float filghtDuration = target_Distance / Vx + status.GetWeight();

        // 비행 시간동안 오브젝트 활성화
        float elapse_time = 0;
        while (elapse_time < filghtDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);  //포물선으로 탄환 발사
            elapse_time += Time.deltaTime;
            yield return null;
        }
        this.gameObject.SetActive(false); // 비활성화
    }
}
