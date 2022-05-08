using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* �� Ŭ������ ����źȯ�� �⺻ �ɷ�ġ�� �̸��� �����ϴ� ������Ƽ Ŭ������ ���� �����ϴ� Ŭ�����̸�
 * �� Ŭ������ ������ �����տ� �����ϰ� �Ʒ��� ����� �޼ҵ带 ���ؼ��� ���� ȣ���� �� ���� */
public class DirectBulletStatusManager : MonoBehaviour
{
    // ����źȯ�� ��ũ���ͺ� ������Ʈ
    [SerializeField]
    private DirectBulletStat statusData;
    public DirectBulletStat StatusData { set { statusData = value; } }

    // źȯ �̸��� ����
    public string GetName()
    {
        return statusData.BulletName;
    }

    // ���ݷ� �� ����
    public int GetAttackDamage()
    {
        return statusData.AttackDamage;
    }

    // ���ݼӵ� �� ����
    public float GetAttackSpeed()
    {
        return statusData.AttackSpeed;
    }

    // �߻�ӵ� �� ����
    public float GetShotSpeed()
    {
        return statusData.ShotSpeed;
    }

    // �����ð� �� ����
    public float GetHoldingTime()
    {
        return statusData.HoldingTime;
    }
}
