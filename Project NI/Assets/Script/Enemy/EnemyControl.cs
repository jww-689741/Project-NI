using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float generationGap;
    public Transform player; // �÷��̾� ��ǥ�� ����
    GameObject[] enemys; // �� ������Ʈ ���� �� ��ȯ�� ���� �迭
    List<int> numberOfEnemy = new List<int>(); // �� ���� ��ü �� ����Ʈ, List�� ����� ������ ������ �ٸ� ����Ʈ�� �����ص� ����

    Vector3 position = Vector3.zero; // ���� ���� ��

    void Start()
    {
        StartCoroutine(testCoroutine()); // �ڷ�ƾ ����
    }

    void Update()
    {

    }

    private IEnumerator testCoroutine()
    {
        int i = 0; // counts ��
        bool stop = true;

        numberOfEnemy.Add(10); // counts�� 1 �߰�

        while (stop) // stop�� true�� ���� �ݺ�
        {
            yield return new WaitForSeconds(3f); // 3�� ���

            SpawnEnemy(numberOfEnemy[i]); // counts[i]��° �� ��ŭ �� ������Ʈ ����

            if (numberOfEnemy.Count - 1 == i) // counts�� ���̻� ���� ���� ���
            {
                stop = false; // stop�� false�� ����, ���� �ݺ����� ����ȴ�.
            }

            i++;
        }
    }

    // �� ������Ʈ ���� �޼ҵ�
    private void SpawnEnemy(int count)
    {
        enemys = ObjectManager.instance.GetEnemy("test", count); // �� count ��ŭ ����Ʈ�� ��������

        for (int i = 0; i < count; i++) // ����Ʈ�� ������Ʈ��
        {
            enemys[i].SetActive(true); // Ȱ��ȭ
        }

        SetVEnemyPosition(enemys); // ���� ����
    }

    // �Ϸ� Ⱦ��
    private void SetHorizontalEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            generationGap = (middle - i) * -2;
            ChangeSpawnPosition(generationGap, 0, 20);
            enemys[i].transform.position = position; // ��ġ ����
        }
    }

    // �Ϸ� ���� 
    private void SetVerticalEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            generationGap = (middle - i) * -2;
            ChangeSpawnPosition(0, 0, 20 + generationGap);
            enemys[i].transform.position = position; // ��ġ ����
        }
    }

    // ���� V��
    private void SetVEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            generationGap = (middle - i) * -2;
            ChangeSpawnPosition(21 + generationGap, 16, 150 - Mathf.Abs(generationGap));
            enemys[i].transform.position = position; // ��ġ ����
        }
    }

    // ������ ���� �� �߾ӿ� �� ģ�� ��ȣ ���ϱ�
    private int MiddleNumber(int count)
    {
        return Mathf.CeilToInt(count / 2); // �߾ӿ� ���� ������Ʈ ��ȣ
    }

    // �� ������Ʈ ���� ���� ���� �� ����
    private void ChangeSpawnPosition(float x, float y, float z)
    {
        position = new Vector3(x + player.position.x, y + player.position.y, z + player.position.z);
    }

    // ���� ���� ���� ���� ���� �߰��� ���� ���ϴ� �޼ҵ�
    private void AddSpawnPosition(GameObject[] enemy, float x, float y, float z)
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            position = new Vector3(x + enemys[i].transform.position.x, y + enemys[i].transform.position.y, z + enemys[i].transform.position.z);
            enemys[i].transform.position = position;
        }
    }
}