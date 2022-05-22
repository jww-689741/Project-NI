using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 본 클래스는 적의 기본 능력치와 이름을 설정하는 프로퍼티 클래스의 값에 접근하는 클래스이며
 * 본 클래스를 적합한 프리팹에 적용하고 아래에 상술할 메소드를 통해서만 값을 호출할 수 있음 */
public class IrvingStatusManager : MonoBehaviour
{
    // 플레이어의 스크립터블 오브젝트
    [SerializeField]
    private EnemyStat statusData;
    public EnemyStat StatusData { set { statusData = value; } }

    // 플레이어 이름값 접근
    public string GetName()
    {
        return statusData.EnemyName;
    }

    // 체력 값 접근
    public int GetHP()
    {
        return statusData.Hp;
    }

    // 방어력 값 접근
    public int GetDefense()
    {
        return statusData.Defense;
    }

    // 공격력 값 접근
    public int GetAttackDamage()
    {
        return statusData.AttackDamage;
    }

    // 이동속도 값 접근
    public float GetMoveSpeed()
    {
        return statusData.MoveSpeed;
    }

    // 공격속도 값 접근
    public float GetAttackSpeed()
    {
        return statusData.AttackSpeed;
    }

    // 스코어 값 접근
    public float GetScore()
    {
        return statusData.Score;
    }
}
