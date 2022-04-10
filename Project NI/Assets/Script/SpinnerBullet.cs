using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBullet : MonoBehaviour
{
    public float centerBulletSpeed; // 중앙탄 발사속도
    public float spinnerBulletInterval = 0.5f; // 회전탄 발사 간격

    IEnumerator Shot(bool direction)
    {
        float timer = 0; // 전체 탄 지속시간용
        float timeTemp = 0; // 자탄 발사 간격용
        int spinnerBulletCount = 0; // 자탄 발사 순서
        for (int i = 0; i < 10; i++) // 자탄 초기 비활성화
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        while (true)
        {
            timer += Time.deltaTime;
            if (timer < 0.5f) // 0.5초동안 중앙 탄 전방 발사
            {
                this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * centerBulletSpeed);
            }
            else if (timer > 5) break; // 전체 시간이 5초가 넘어가면 루프 아웃
            else
            {
                if(direction) this.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * 30f); // 방향이 ture값일 때 시계 반대방향 회전
                else this.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * -30f); // 방향이 false값일 때 시계방향 회전
                timeTemp += Time.deltaTime;
                if (timeTemp > spinnerBulletInterval) // 자탄 활성화
                {
                    timeTemp = 0;
                    this.gameObject.transform.GetChild(spinnerBulletCount).gameObject.SetActive(true);
                    if (spinnerBulletCount < 9) spinnerBulletCount++;
                }
            }
            yield return null; // 코루틴 딜레이 없음
        }
        this.gameObject.SetActive(false); // 비활성화
    }
}
