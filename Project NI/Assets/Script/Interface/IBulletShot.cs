using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* �� �������̽��� źȯ �߻�� ���õ� �߻�޼ҵ带 ���� */
public interface IBulletShot
{
    IEnumerator Shot(Vector3 directionVector); // �߻� �ڷ�ƾ���� ���������� ȸ�� ���Ⱚ�� �Ű������� ���� ����
    public float GetAttackSpeedToBullet(); // �÷��̾��� ���� �������� ���� �ӵ��� �����ϱ� ������ źȯ�� ���ݼӵ� ���� �Ѱ��ִ� �޼ҵ�
}
