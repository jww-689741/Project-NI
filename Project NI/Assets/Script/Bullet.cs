using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
<<<<<<< HEAD
    public float shotSpeed; // 발사 속도

    private Transform startTransform;

    private void Start()
    {
        startTransform = this.transform;
    }
    // 탄환 발사 코루틴
    IEnumerator Shot(Vector3 aimingPoint)
=======
    IEnumerator MoveBullet()
>>>>>>> origin/Pks
    {
        float timer = 0;
        while (true)
        {
<<<<<<< HEAD
            timer += Time.deltaTime;
            if (timer > 2) break;

            transform.Translate(aimingPoint * Time.deltaTime * shotSpeed); // 탄환 발사
            yield return null; // 코루틴 딜레이 없음
        }

        gameObject.SetActive(false); // 비활성화
=======
            timer += Time.deltaTime;    // 시간 축적
            if (timer > 2)               // 2초뒤 반복문을 빠져나간다.
                break;

            transform.Translate(Vector3.forward * Time.deltaTime * 40f);    // 총알을 움직인다.
            yield return null;
        }

        // 총알 비활성화.
        gameObject.SetActive(false);
>>>>>>> origin/Pks
    }
}
