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
            timer += Time.deltaTime;    // �ð� ����
            if (timer > 2)               // 2�ʵ� �ݺ����� ����������.
                break;

            transform.Translate(Vector3.forward * Time.deltaTime * 40f);    // �Ѿ��� �����δ�.
            yield return null;
        }

        // �Ѿ� ��Ȱ��ȭ.
        gameObject.SetActive(false);
    }
}
