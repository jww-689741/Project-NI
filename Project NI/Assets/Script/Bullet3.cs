using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : MonoBehaviour
{
    public float shotSpeed; // �߻� �ӵ�
    // źȯ �߻� �ڷ�ƾ
    IEnumerator Shot(Vector3[] shotVecter)
    {
        Vector3 directionVector = ((shotVecter[0] - shotVecter[1]).normalized); // (���콺 ��ǥ - �߻� �������� ��ǥ).���⺤��
        Debug.Log(directionVector);
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 2) break;
            //transform.Translate(directionVector * Time.deltaTime * shotSpeed); // źȯ �߻�
            for(int i =0; i<5;i++)
            {
                this.transform.GetChild(i).gameObject.transform.Translate(directionVector * Time.deltaTime * shotSpeed);
            }
            yield return null; // �ڷ�ƾ ������ ����
        }
        gameObject.SetActive(false);
        //this.transform.GetChild(0).gameObject.SetActive(false); // ��Ȱ��ȭ
        //this.transform.GetChild(1).gameObject.SetActive(false);
        //this.transform.GetChild(2).gameObject.SetActive(false);
        //this.transform.GetChild(3).gameObject.SetActive(false);
        //this.transform.GetChild(4).gameObject.SetActive(false);
    }
}
