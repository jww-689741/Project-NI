using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*������ �޴����� ���������ϵ��� ��
 * Assets/Create/Scriptable Object/Missile Stat ��η� ���� ���� */
[CreateAssetMenu(fileName = "Missile Stat", menuName = "Scriptable Object/Missile Stat", order = int.MaxValue)]

/* �� Ŭ������ �̻����� �⺻ �ɷ�ġ�� �̸��� �����ϴ� ������Ƽ Ŭ�����̸�
 * ��� �ʵ��� �� ������ ����Ƽ �����Ϳ��� �����ϵ��� ��
 * ���� �뷱���� ������ �ֹǷ� �� ���濡 �����ϵ��� �� */

public class MissileStat : ScriptableObject
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

    // �߻��ְ�ӵ�
    // ���� �������� ����
    [SerializeField]
    private float shotSpeed;
    public float ShotSpeed { get { return shotSpeed; } }
}
