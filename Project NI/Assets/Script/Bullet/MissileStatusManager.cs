using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* �� Ŭ������ �̻����� �⺻ �ɷ�ġ�� �̸��� �����ϴ� ������Ƽ Ŭ������ ���� �����ϴ� Ŭ�����̸�
 * �� Ŭ������ ������ �����տ� �����ϰ� �Ʒ��� ����� �޼ҵ带 ���ؼ��� ���� ȣ���� �� ���� */
public class MissileStatusManager : MonoBehaviour
{
    // �̻����� ��ũ���ͺ� ������Ʈ
    [SerializeField]
    private MissileStat statusData;
    public MissileStat StatusData { set { statusData = value; } }

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
}
