using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Irving : MonoBehaviour
{
    private Vector3 position = Vector3.zero; // ��ǥ ���� ���� ��, �� ������ ������Ʈ�� �����δ�.
    private Transform player; // �÷��̾� ��ǥ�� �������� ���� ������Ʈ
    private Transform enemyTransform; // �ڱ� �ڽ��� ��ǥ�� ����
    private float movementSpeed; // �̵��ӵ�
    private bool direction;

    private void Awake()
    {
        enemyTransform = this.gameObject.transform;
    }

    void Start()
    {
        var status = GetComponent<SaraStatusManager>();
        player = GameObject.FindWithTag("Player").transform;
        position = enemyTransform.position;
        movementSpeed = status.GetMoveSpeed();
        direction = Random.value > 0.5f;
    }

    void Update()
    {
        move();
        StepMovement();
    }

    // �̵� �޼ҵ�
    private void move()
    {
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
