using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckShot : MonoBehaviour, IBulletShot
{
    public float GetAttackSpeedToBullet()
    {
        var status = GetComponent<DirectBulletStatusManager>(); // 직사탄환의 스탯 데이터 접근
        return status.GetAttackSpeed(); // 공격속도 값 반환
    }

    public IEnumerator Shot(Vector3 directionVector)
    {
        var status = GetComponent<DirectBulletStatusManager>(); // 직사탄환의 스탯 데이터 접근
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > status.GetHoldingTime()) break;
            for (int i = 0; i < 5; i++)
            {
                transform.GetChild(i).gameObject.transform.Translate(directionVector * Time.deltaTime * status.GetShotSpeed()); // 자탄 탄환 발사
            }
            yield return null; // 코루틴 딜레이 없음
        }

        this.gameObject.SetActive(false); // 비활성화
    }
}
