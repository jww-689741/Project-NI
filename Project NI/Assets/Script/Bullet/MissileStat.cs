using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*에디터 메뉴에서 생성가능하도록 함
 * Assets/Create/Scriptable Object/Missile Stat 경로로 생성 가능 */
[CreateAssetMenu(fileName = "Missile Stat", menuName = "Scriptable Object/Missile Stat", order = int.MaxValue)]

/* 본 클래스는 미사일의 기본 능력치와 이름을 설정하는 프로퍼티 클래스이며
 * 모든 필드의 값 변경은 유니티 에디터에서 조정하도록 함
 * 게임 밸런스에 영향을 주므로 값 변경에 유의하도록 함 */

public class MissileStat : ScriptableObject
{
    // 이름
    [SerializeField]
    private string bulletName;
    public string BulletName { get { return bulletName; } }

    // 공격력
    // 값이 높을수록 강함
    [SerializeField]
    private int attackDamage;
    public int AttackDamage { get { return attackDamage; } }

    // 공격속도
    // 값이 낮을수록 빠름
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    // 발사최고속도
    // 값이 높을수록 빠름
    [SerializeField]
    private float shotSpeed;
    public float ShotSpeed { get { return shotSpeed; } }
}
