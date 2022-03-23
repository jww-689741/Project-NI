using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float shotSpeed; // 발사 속도

    private Transform startTransform;

    private void Start()
    {
        startTransform = this.transform;
    }
    // 탄환 발사 코루틴
    IEnumerator Shot(Vector3 aimingPoint)
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 2) break;

            transform.Translate(aimingPoint * Time.deltaTime * shotSpeed); // 탄환 발사
            yield return null; // 코루틴 딜레이 없음
        }

        gameObject.SetActive(false); // 비활성화
    }
}
