using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 에디터 메뉴에서 생성가능하도록 함
 * Assets/Create/Scriptable Object/Player Stat 경로로 생성 가능 */
[CreateAssetMenu(fileName = "Enemy Stat", menuName = "Scriptable Object/Ememy Stat", order = int.MaxValue)]

/* 본 클래스는 적의 기본 능력치와 이름을 설정하는 프로퍼티 클래스이며
 * 모든 필드의 값 변경은 유니티 에디터에서 조정하도록 함
 * 게임 밸런스에 영향을 주므로 값 변경에 유의하도록 함 */
public class EnemyStat : ScriptableObject
{
    // 이름
    [SerializeField]
    private string enemyName;
    public string EnemyName { get { return enemyName; } }

    // 체력
    // 값이 높을수록 강함
    [SerializeField]
    private int hp;
    public int Hp { get { return hp; } }

    // 방어력
    // 값이 높을수록 강함
    [SerializeField]
    private int defense;
    public int Defense { get { return defense; } }

    // 공격력
    // 값이 높을수록 강함
    [SerializeField]
    private int attackDamage;
    public int AttackDamage { get { return attackDamage; } }

    // 이동속도
    // 값이 높을수록 빠름
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    // 공격속도
    // 값이 낮을수록 빠름
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    [SerializeField]
    private float score;
    public float Score { get { return score; } }
}
