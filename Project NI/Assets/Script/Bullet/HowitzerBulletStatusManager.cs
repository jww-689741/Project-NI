using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* �� Ŭ������ ���źȯ�� �⺻ �ɷ�ġ�� �̸��� �����ϴ� ������Ƽ Ŭ������ ���� �����ϴ� Ŭ�����̸�
 * �� Ŭ������ ������ �����տ� �����ϰ� �Ʒ��� ����� �޼ҵ带 ���ؼ��� ���� ȣ���� �� ���� */
public class HowitzerBulletStatusManager : MonoBehaviour
{
    // ���źȯ�� ��ũ���ͺ� ������Ʈ
    [SerializeField]
    private HowitzerBulletStat statusData;
    public HowitzerBulletStat StatusData { set { statusData = value; } }

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

    // �߻簢 �� ����
    public float GetFiringAngle()
    {
        return statusData.FiringAngle;
    }

    // ����ġ �� ����
    public float GetWeight()
    {
        return statusData.Weight;
    }

}
