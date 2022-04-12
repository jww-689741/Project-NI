using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 본 클래스는 직사탄환의 기본 능력치와 이름을 설정하는 프로퍼티 클래스의 값에 접근하는 클래스이며
 * 본 클래스를 적합한 프리팹에 적용하고 아래에 상술할 메소드를 통해서만 값을 호출할 수 있음 */
public class DirectBulletStatusManager : MonoBehaviour
{
    // 직사탄환의 스크립터블 오브젝트
    [SerializeField]
    private DirectBulletStat statusData;
    public DirectBulletStat StatusData { set { statusData = value; } }

    // 탄환 이름값 접근
    public string GetName()
    {
        return statusData.BulletName;
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
