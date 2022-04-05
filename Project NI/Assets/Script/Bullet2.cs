using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public float firingAngle = 45.0f;  //포물선각도
    public float gravity = 9.8f; //중력
    public float weight = 1.3f;  //가중치
    // 탄환 발사 코루틴
    IEnumerator Shot(Vector3[] shotVecter)
    {
        yield return new WaitForSeconds(0.5f);  // 0.5초 기다림

        float target_Distance = 20f;  //포물선 거리

        //지정된 각도에서 대상에 오브젝트를 던지는 데 필요한 속도를 계산합니다.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        //x축 y축 속도 계산
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad) * weight;
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad) * weight;

        //비행시간 계산
        float filghtDuration = target_Distance / Vx + weight;

        //비행시간동안 오브젝트 활성화
        float elapse_time = 0;
        while(elapse_time < filghtDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            elapse_time += Time.deltaTime;
            yield return null;
        }
        
        gameObject.SetActive(false); // 비활성화
    }
}
