using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*에디터 메뉴에서 생성가능하도록 함
 * Assets/Create/Scriptable Object/Direct Bullet Stat 경로로 생성 가능 */
[CreateAssetMenu(fileName = "Spinner Bullet Stat", menuName = "Scriptable Object/Spinner Bullet Stat", order = int.MaxValue)]

/* 본 클래스는 회전탄의 기본 능력치와 이름을 설정하는 프로퍼티 클래스이며
 * 모든 필드의 값 변경은 유니티 에디터에서 조정하도록 함
 * 게임 밸런스에 영향을 주므로 값 변경에 유의하도록 함 */

public class SpinnerBulletStat : ScriptableObject
{
    // 이름
    [SerializeField]
    private string bulletName;
    public string BulletName { get { return bulletName; } }

    // 자탄 수
    [SerializeField]
    private int childBulletNumber;
    public int ChildBulletNumber { get { return childBulletNumber; } }

    // 회전각도
    [SerializeField]
    private int rotateAngle;
    public int RotateAngle { get { return rotateAngle; } }

    // 회전방향
    // true : 시계방향 false : 시계 반대방향
    [SerializeField]
    private bool rotateDirection;
    public bool RotateDirection { get { return rotateDirection; } }

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
}
