using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*에디터 메뉴에서 생성가능하도록 함
 * Assets/Create/Scriptable Object/Howitzer Bullet Stat 경로로 생성 가능 */
[CreateAssetMenu(fileName = "Howitzer Bullet Stat", menuName = "Scriptable Object/Howitzer Bullet Stat", order = int.MaxValue)]

/* 본 클래스는 직사탄환의 기본 능력치와 이름을 설정하는 프로퍼티 클래스이며
 * 모든 필드의 값 변경은 유니티 에디터에서 조정하도록 함
 * 게임 밸런스에 영향을 주므로 값 변경에 유의하도록 함 */
public class HowitzerBulletStat : ScriptableObject
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

    // 발사속도
    // 값이 높을수록 빠름
    [SerializeField]
    private float shotSpeed;
    public float ShotSpeed { get { return shotSpeed; } }

    // 유지시간
    // 값이 낮을수록 짧음
    [SerializeField]
    private float holdingTime;
    public float HoldingTime { get { return holdingTime; } }

    // 발사각
    // 값이 높을수록 각이 커짐
    [SerializeField]
    private float firingAngle;
    public float FiringAngle { get { return firingAngle; } }

    // 가중치
    // 값이 높을수록 각이 커짐
    [SerializeField]
    private float weight;
    public float Weight { get { return weight; } }
}
