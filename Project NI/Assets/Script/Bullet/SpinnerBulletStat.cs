using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*������ �޴����� ���������ϵ��� ��
 * Assets/Create/Scriptable Object/Direct Bullet Stat ��η� ���� ���� */
[CreateAssetMenu(fileName = "Spinner Bullet Stat", menuName = "Scriptable Object/Spinner Bullet Stat", order = int.MaxValue)]

/* �� Ŭ������ ȸ��ź�� �⺻ �ɷ�ġ�� �̸��� �����ϴ� ������Ƽ Ŭ�����̸�
 * ��� �ʵ��� �� ������ ����Ƽ �����Ϳ��� �����ϵ��� ��
 * ���� �뷱���� ������ �ֹǷ� �� ���濡 �����ϵ��� �� */

public class SpinnerBulletStat : ScriptableObject
{
    // �̸�
    [SerializeField]
    private string bulletName;
    public string BulletName { get { return bulletName; } }

    // ��ź ��
    [SerializeField]
    private int childBulletNumber;
    public int ChildBulletNumber { get { return childBulletNumber; } }

    // ȸ������
    [SerializeField]
    private int rotateAngle;
    public int RotateAngle { get { return rotateAngle; } }

    // ȸ������
    // true : �ð���� false : �ð� �ݴ����
    [SerializeField]
    private bool rotateDirection;
    public bool RotateDirection { get { return rotateDirection; } }

    // ���ݷ�
    // ���� �������� ����
    [SerializeField]
    private int attackDamage;
    public int AttackDamage { get { return attackDamage; } }

    // ���ݼӵ�
    // ���� �������� ����
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    // �߻�ӵ�
    // ���� �������� ����
    [SerializeField]
    private float shotSpeed;
    public float ShotSpeed { get { return shotSpeed; } }

    // �����ð�
    // ���� �������� ª��
    [SerializeField]
    private float holdingTime;
    public float HoldingTime { get { return holdingTime; } }
}
