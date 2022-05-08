using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*������ �޴����� ���������ϵ��� ��
 * Assets/Create/Scriptable Object/Howitzer Bullet Stat ��η� ���� ���� */
[CreateAssetMenu(fileName = "Howitzer Bullet Stat", menuName = "Scriptable Object/Howitzer Bullet Stat", order = int.MaxValue)]

/* �� Ŭ������ ����źȯ�� �⺻ �ɷ�ġ�� �̸��� �����ϴ� ������Ƽ Ŭ�����̸�
 * ��� �ʵ��� �� ������ ����Ƽ �����Ϳ��� �����ϵ��� ��
 * ���� �뷱���� ������ �ֹǷ� �� ���濡 �����ϵ��� �� */
public class HowitzerBulletStat : ScriptableObject
{
    // �̸�
    [SerializeField]
    private string bulletName;
    public string BulletName { get { return bulletName; } }

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

    // �߻簢
    // ���� �������� ���� Ŀ��
    [SerializeField]
    private float firingAngle;
    public float FiringAngle { get { return firingAngle; } }

    // ����ġ
    // ���� �������� ���� Ŀ��
    [SerializeField]
    private float weight;
    public float Weight { get { return weight; } }
}
