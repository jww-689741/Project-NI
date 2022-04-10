using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float shotSpeed; // �߻� �ӵ�
    // źȯ �߻� �ڷ�ƾ
    IEnumerator Shot(Vector3[] shotVecter)
    {
        Vector3 directionVector = ((shotVecter[0] - shotVecter[1]).normalized); // (���콺 ��ǥ - �߻� �������� ��ǥ).���⺤��
    
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 2) break;

            transform.Translate(directionVector * Time.deltaTime * shotSpeed); // źȯ �߻�
            yield return null; // �ڷ�ƾ ������ ����
        }

        gameObject.SetActive(false); // ��Ȱ��ȭ
    }
}
