using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
<<<<<<< HEAD
    public float shotSpeed; // �߻� �ӵ�

    private Transform startTransform;

    private void Start()
    {
        startTransform = this.transform;
    }
    // źȯ �߻� �ڷ�ƾ
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

            transform.Translate(aimingPoint * Time.deltaTime * shotSpeed); // źȯ �߻�
            yield return null; // �ڷ�ƾ ������ ����
        }

        gameObject.SetActive(false); // ��Ȱ��ȭ
=======
            timer += Time.deltaTime;    // �ð� ����
            if (timer > 2)               // 2�ʵ� �ݺ����� ����������.
                break;

            transform.Translate(Vector3.forward * Time.deltaTime * 40f);    // �Ѿ��� �����δ�.
            yield return null;
        }

        // �Ѿ� ��Ȱ��ȭ.
        gameObject.SetActive(false);
>>>>>>> origin/Pks
    }
}
