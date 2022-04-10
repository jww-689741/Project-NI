using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet4 : MonoBehaviour
{
    public float shotSpeed; // �߻� �ӵ�
    // źȯ �߻� �ڷ�ƾ
    IEnumerator Shot(int direction)
    {
        Vector3 directionVector =Vector3.forward;
        if (direction == 0)  // ���� �߻�
        {
           directionVector = transform.forward; // ��ġ ����
        }
        else if (direction == 1) //���� �߻�
        {
            directionVector = -transform.right;
        }
        else if (direction == 2) //���� �߻�
        {
            directionVector = transform.right;
        }
        else if (direction == 3) //�ĸ� �߻�
        {
            directionVector = -transform.forward;
        }
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 2) break;

            transform.position += directionVector * Time.deltaTime * shotSpeed; // źȯ �߻�
            yield return null; // �ڷ�ƾ ������ ����
        }

        gameObject.SetActive(false); // ��Ȱ��ȭ
    }
}
