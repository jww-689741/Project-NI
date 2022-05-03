using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBullet : MonoBehaviour, IBulletShot
{
    public IEnumerator Shot(Vector3 directionVector)
    {
        var status = GetComponent<SpinnerBulletStausManager>();
        var timer = 0f; // 전체 탄 지속시간용
        var timeTemp = 0f; // 자탄 발사 간격용
        var count = 0; // 자탄 수 카운트
        var bulletTransform = this.gameObject.transform; // 자기 자신의 Transform
        for (int i = 0; i < status.GetChildBulletNumber(); i++) // 자탄 초기 비활성화
        {
            bulletTransform.GetChild(i).gameObject.SetActive(false);
        }
        while (true)
        {
            timer += Time.deltaTime;
            if (timer < status.GetAttackSpeed()) // 0.5초동안 중앙 탄 전방 발사
            {
                bulletTransform.Translate(Vector3.forward * Time.deltaTime * status.GetShotSpeed());
            }
            else if (timer > status.GetHoldingTime()) break; // 유지시간을 넘어가면 루프 아웃
            else
            {
                if (status.GetRotateDirection()) bulletTransform.Rotate(Vector3.up * Time.deltaTime * -status.GetRotateAngle()); // 시계 방향 회전
                else bulletTransform.Rotate(Vector3.up * Time.deltaTime * status.GetRotateAngle()); // 시계 반대 방향 회전
                timeTemp += Time.deltaTime;
                if (timeTemp > status.GetAttackSpeed()) // 자탄 활성화
                {
                    timeTemp = 0;
                    bulletTransform.GetChild(count).gameObject.SetActive(true);
                    if (count < status.GetChildBulletNumber()) ++count;
                }
            }
            yield return null; // 코루틴 딜레이 없음
        }
        this.gameObject.SetActive(false); // 비활성화
    }

    public float GetAttackSpeedToBullet()
    {
        var status = GetComponent<SpinnerBulletStausManager>();
        return status.GetAttackSpeed();
    }
}
