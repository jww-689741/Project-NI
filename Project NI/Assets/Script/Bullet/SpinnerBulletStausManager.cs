using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBulletStausManager : MonoBehaviour
{
    // ȸ��ź�� ��ũ���ͺ� ������Ʈ
    [SerializeField]
    private SpinnerBulletStat statusData;
    public SpinnerBulletStat StatusData { set { statusData = value; } }


    // źȯ �̸��� ����
    public string GetName()
    {
        return statusData.BulletName;
    }

    // ��ź�� �� ����
    public int GetChildBulletNumber()
    {
        return statusData.ChildBulletNumber;
    }

    // ȸ������ �� ����
    public float GetRotateAngle()
    {
        return statusData.RotateAngle;
    }

    // ȸ������ �� ����
    public bool GetRotateDirection()
    {
        return statusData.RotateDirection;
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
