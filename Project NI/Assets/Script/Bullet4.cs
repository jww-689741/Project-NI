using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet4 : MonoBehaviour
{
    public float shotSpeed; // 발사 속도
    // 탄환 발사 코루틴
    IEnumerator Shot(int direction)
    {
        Vector3 directionVector =Vector3.forward;
        if (direction == 0)  // 정면 발사
        {
           directionVector = transform.forward; // 위치 지정
        }
        else if (direction == 1) //좌측 발사
        {
            directionVector = -transform.right;
        }
        else if (direction == 2) //우측 발사
        {
            directionVector = transform.right;
        }
        else if (direction == 3) //후면 발사
        {
            directionVector = -transform.forward;
        }
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 2) break;

            transform.position += directionVector * Time.deltaTime * shotSpeed; // 탄환 발사
            yield return null; // 코루틴 딜레이 없음
        }

        gameObject.SetActive(false); // 비활성화
    }
}
