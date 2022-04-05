using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : MonoBehaviour
{
    public float shotSpeed; // 발사 속도
    // 탄환 발사 코루틴
    IEnumerator Shot(Vector3[] shotVecter)
    {
        Vector3 directionVector = ((shotVecter[0] - shotVecter[1]).normalized); // (마우스 좌표 - 발사 시작지점 좌표).방향벡터
        Debug.Log(directionVector);
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 2) break;
            //transform.Translate(directionVector * Time.deltaTime * shotSpeed); // 탄환 발사
            for(int i =0; i<5;i++)
            {
                this.transform.GetChild(i).gameObject.transform.Translate(directionVector * Time.deltaTime * shotSpeed);
            }
            yield return null; // 코루틴 딜레이 없음
        }
        gameObject.SetActive(false);
        //this.transform.GetChild(0).gameObject.SetActive(false); // 비활성화
        //this.transform.GetChild(1).gameObject.SetActive(false);
        //this.transform.GetChild(2).gameObject.SetActive(false);
        //this.transform.GetChild(3).gameObject.SetActive(false);
        //this.transform.GetChild(4).gameObject.SetActive(false);
    }
}
