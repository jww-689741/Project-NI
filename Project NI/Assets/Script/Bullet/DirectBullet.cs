using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DirectBullet : MonoBehaviour, IBulletShot
{

    // 탄환의 공격속도를 반환하는 메소드 재정의
    public float GetAttackSpeedToBullet()
    {
        var status = GetComponent<DirectBulletStatusManager>(); // 직사탄환의 스탯 데이터 접근
        return status.GetAttackSpeed(); // 공격속도 값 반환
    }

    // 발사 추상 코루틴 재정의
    public IEnumerator Shot(Vector3 directionVector)
    {
        var status = GetComponent<DirectBulletStatusManager>(); // 직사탄환의 스탯 데이터 접근
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > status.GetHoldingTime()) break;

            transform.Translate(Vector3.forward * Time.deltaTime * status.GetShotSpeed()); // 탄환 발사
            yield return null; // 코루틴 딜레이 없음
        }

        this.gameObject.SetActive(false); // 비활성화
    }

}
