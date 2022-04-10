using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float shotSpeed; // �߻� �ӵ�

    private Transform startTransform;

    private void Start()
    {
        startTransform = this.transform;
    }
    // źȯ �߻� �ڷ�ƾ
    IEnumerator Shot(Vector3 aimingPoint)
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 2) break;

            transform.Translate(aimingPoint * Time.deltaTime * shotSpeed); // źȯ �߻�
            yield return null; // �ڷ�ƾ ������ ����
        }

        gameObject.SetActive(false); // ��Ȱ��ȭ
    }
}
