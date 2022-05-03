using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* ������ �޴����� ���������ϵ��� ��
 * Assets/Create/Scriptable Object/Player Stat ��η� ���� ���� */
[CreateAssetMenu(fileName = "Enemy Stat", menuName = "Scriptable Object/Ememy Stat", order = int.MaxValue)]

/* �� Ŭ������ ���� �⺻ �ɷ�ġ�� �̸��� �����ϴ� ������Ƽ Ŭ�����̸�
 * ��� �ʵ��� �� ������ ����Ƽ �����Ϳ��� �����ϵ��� ��
 * ���� �뷱���� ������ �ֹǷ� �� ���濡 �����ϵ��� �� */
public class EnemyStat : ScriptableObject
{
    // �̸�
    [SerializeField]
    private string enemyName;
    public string EnemyName { get { return enemyName; } }

    // ü��
    // ���� �������� ����
    [SerializeField]
    private int hp;
    public int Hp { get { return hp; } }

    // ����
    // ���� �������� ����
    [SerializeField]
    private int defense;
    public int Defense { get { return defense; } }

    // ���ݷ�
    // ���� �������� ����
    [SerializeField]
    private int attackDamage;
    public int AttackDamage { get { return attackDamage; } }

    // �̵��ӵ�
    // ���� �������� ����
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    // ���ݼӵ�
    // ���� �������� ����
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    [SerializeField]
    private float score;
    public float Score { get { return score; } }
}
