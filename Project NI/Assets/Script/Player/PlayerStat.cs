using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ������ �޴����� ���������ϵ��� ��
 * Assets/Create/Scriptable Object/Player Stat ��η� ���� ���� */
[CreateAssetMenu(fileName = "Player Stat", menuName = "Scriptable Object/Player Stat", order = int.MaxValue)]

/* �� Ŭ������ �÷��̾��� �⺻ �ɷ�ġ�� �̸��� �����ϴ� ������Ƽ Ŭ�����̸�
 * ��� �ʵ��� �� ������ ����Ƽ �����Ϳ��� �����ϵ��� ��
 * ���� �뷱���� ������ �ֹǷ� �� ���濡 �����ϵ��� �� */
public class PlayerStat : ScriptableObject
{
    // �̸�
    [SerializeField]
    private string playerName;
    public string PlayerName { get { return playerName; } }

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
<<<<<<< HEAD
=======

    public void StatUp(string name)
    {
        if (name.Equals("WapponUp"))
        {
            attackDamage += 10;
        }else if(name.Equals("ShieldUp"))
        {
            defense += 10;
        }else if (name.Equals("HPUp"))
        {
            hp += 100;
        }
    }
>>>>>>> origin/Pks
}
