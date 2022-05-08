using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBulletStausManager : MonoBehaviour
{
    // 회전탄의 스크립터블 오브젝트
    [SerializeField]
    private SpinnerBulletStat statusData;
    public SpinnerBulletStat StatusData { set { statusData = value; } }


    // 탄환 이름값 접근
    public string GetName()
    {
        return statusData.BulletName;
    }

    // 자탄수 값 접근
    public int GetChildBulletNumber()
    {
        return statusData.ChildBulletNumber;
    }

    // 회전각도 값 접근
    public float GetRotateAngle()
    {
        return statusData.RotateAngle;
    }

    // 회전방향 값 접근
    public bool GetRotateDirection()
    {
        return statusData.RotateDirection;
    }

    // 공격력 값 접근
    public int GetAttackDamage()
    {
        return statusData.AttackDamage;
    }

    // 공격속도 값 접근
    public float GetAttackSpeed()
    {
        return statusData.AttackSpeed;
    }

    // 발사속도 값 접근
    public float GetShotSpeed()
    {
        return statusData.ShotSpeed;
    }

    // 유지시간 값 접근
    public float GetHoldingTime()
    {
        return statusData.HoldingTime;
    }
}
