using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* �� Ŭ������ ���� �⺻ �ɷ�ġ�� �̸��� �����ϴ� ������Ƽ Ŭ������ ���� �����ϴ� Ŭ�����̸�
 * �� Ŭ������ ������ �����տ� �����ϰ� �Ʒ��� ����� �޼ҵ带 ���ؼ��� ���� ȣ���� �� ���� */
public class IrvingStatusManager : MonoBehaviour
{
    // �÷��̾��� ��ũ���ͺ� ������Ʈ
    [SerializeField]
    private EnemyStat statusData;
    public EnemyStat StatusData { set { statusData = value; } }

    // �÷��̾� �̸��� ����
    public string GetName()
    {
        return statusData.EnemyName;
    }

    // ü�� �� ����
    public int GetHP()
    {
        return statusData.Hp;
    }

    // ���� �� ����
    public int GetDefense()
    {
        return statusData.Defense;
    }

    // ���ݷ� �� ����
    public int GetAttackDamage()
    {
        return statusData.AttackDamage;
    }

    // �̵��ӵ� �� ����
    public float GetMoveSpeed()
    {
        return statusData.MoveSpeed;
    }

    // ���ݼӵ� �� ����
    public float GetAttackSpeed()
    {
        return statusData.AttackSpeed;
    }

    // ���ھ� �� ����
    public float GetScore()
    {
        return statusData.Score;
    }
}
