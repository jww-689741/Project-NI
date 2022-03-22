using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    IEnumerator MoveBullet()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;    // 시간 축적
            if (timer > 2)               // 2초뒤 반복문을 빠져나간다.
                break;

            transform.Translate(Vector3.forward * Time.deltaTime * 40f);    // 총알을 움직인다.
            yield return null;
        }

        // 총알 비활성화.
        gameObject.SetActive(false);
    }
}
