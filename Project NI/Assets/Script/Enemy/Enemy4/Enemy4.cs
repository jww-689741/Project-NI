using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    public float movementSpeed; // �̵��ӵ�
    Transform player; // �÷��̾� ��ǥ�� �������� ���� ������Ʈ

    private Vector3 position = Vector3.zero; // ��ǥ ���� ���� ��, �� ������ ������Ʈ�� �����δ�.
    private Rigidbody rb; // ������ �ٵ�
    private Transform enemyTransform; // �ڱ� �ڽ��� ��ǥ�� ����

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        position = enemyTransform.position; // �ʱ� ��ǥ�� ��ǥ ��ǥ�� �Է�, ������ �����ϱ� ���� �ʿ�
    }

    void Update()
    {
        move(); // ��������Ʈ control ����
        StepMovement();
    }

    // �̵� �޼ҵ�
    private void move()
    {
        enemyTransform.LookAt(player); // �÷��̾ �ٶ󺸰� �Ѵ�.
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, position, movementSpeed * Time.deltaTime); // ���� ��ǥ���� ��ǥ ��ǥ�� ������ �ӵ��� �̵�
    }

    // �÷��̾� ���� ��ǥ ��ǥ ���� �޼ҵ�
    public void ChangeTargetToPlayer(float x, float y, float z)
    {
        position.x = x + player.position.x;
        position.y = y + player.position.y;
        position.z = z + player.position.z;
    }

    // ��������
    void StepMovement()
    {
        Vector3 mep = enemyTransform.position; // �ش� ������Ʈ ���� ��ǥ ��������

        bool change = false; // ��ǥ ��ǥ�� ���� Ȯ�ο� 

        float x;
        float z = 0;

        if (mep == position)
        {
            if (mep.z >= 80)
            {
                z = 80;

                if (mep.z == 80)
                {
                    z = 60;
                }
                change = true;
            }
            else if (mep.z >= 60)
            {
                z = 60;

                if (mep.z == 60)
                {
                    z = 40;
                }
                change = true;
            }
            else if (mep.z >= 40)
            {
                z = 40;

                if (mep.z == 40)
                {
                    z = 20;
                }
                change = true;
            }
            else if (mep.z >= 20)
            {
                z = 20;

                if (mep.z == 20)
                {
                    z = 0;
                }
                change = true;
            }
        }

        if (change) // �� if ������ ���� ���� ?�� ��
        {
            x = 30 * DistanceCalculation(mep.x);
            ChangeTargetToPlayer(x, Random.Range(-20, 20), z - player.position.z); // ���� ��ǥ�� �̵�
        }
    }

    // 
    private int DistanceCalculation(float number)
    {
        if (number >= 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
