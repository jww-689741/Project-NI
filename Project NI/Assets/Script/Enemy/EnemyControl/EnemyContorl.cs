using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContorl : MonoBehaviour
{
    public float generationGap;
    public Transform player; // �÷��̾� ��ǥ�� ����

    private GameObject[] enemys; // �� ������Ʈ ���� �� ��ȯ�� ���� �迭
    private Vector3 position = Vector3.zero; // ���� ���� ��
    private List<int> numberOfEnemy = new List<int>(); // �� ���� ��ü �� ����Ʈ, List�� ����� ������ ������ �ٸ� ����Ʈ�� �����ص� ����
    private List<Dictionary<string, object>> data;

    void Awake()
    {
        data = ReadCSV.Read("StageInformation");
    }

    void Start()
    {
<<<<<<< HEAD
        StartCoroutine(startSpawn()); // �ڷ�ƾ ����
=======
        //StartCoroutine(startSpawn()); // �ڷ�ƾ ����
>>>>>>> origin/Pks
    }

    void Update()
    {

    }

    private IEnumerator startSpawn()
    {
        int i = 0; // counts ��

        int sara, billy, betty, irving, selma; // �� ��ü�� ����

        while (true) // stop�� true�� ���� �ݺ�
        {
            if (data.Count - 1 == i) // data�� ���̻� ���� ���� ���
            {
                break; //�ݺ����� ����ȴ�.
            }

            sara = int.Parse((data[i]["sara"] + "")); // ������Ʈ �� ���ڿ��� ����
            billy = int.Parse((data[i]["billy"] + "")); // ������Ʈ �� ���ڿ��� ����
            betty = int.Parse((data[i]["betty"] + "")); // ������Ʈ �� ���ڿ��� ����
            irving = int.Parse((data[i]["irving"] + "")); // ������Ʈ �� ���ڿ��� ����
            selma = int.Parse((data[i]["selma"] + "")); // ������Ʈ �� ���ڿ��� ����

            if (sara != 0) // �� 1 �����Ͱ� 0�� �ƴ� ��
            {
                SpawnEnemy("sara", sara);
            }
            if (billy != 0) // �� 2 �����Ͱ� 0�� �ƴ� ��
            {
                SpawnEnemy("billy", billy);
            }
            if (betty != 0) // �� 3 �����Ͱ� 0�� �ƴ� ��
            {
                SpawnEnemy("betty", betty);
            }
            if (irving != 0) // �� 4 �����Ͱ� 0�� �ƴ� ��
            {
                SpawnEnemy("irving", irving);
            }
            if (selma != 0) // �� 5 �����Ͱ� 0�� �ƴ� ��
            {
                SpawnEnemy("selma", selma);
            }
            i++;

            yield return new WaitForSeconds(10f); // 10�� ���
        }
    }

    // �� ������Ʈ ���� �޼ҵ�
    private void SpawnEnemy(string name, int count)
    {
<<<<<<< HEAD
        enemys = ObjectManager.instance.GetEnemy(name, count); // �� count ��ŭ ����Ʈ�� ��������
=======
        this.enemys = ObjectManager.instance.GetEnemy(name, count); // �� count ��ŭ ����Ʈ�� ��������
>>>>>>> origin/Pks

        for (int i = 0; i < count; i++) // ����Ʈ�� ������Ʈ��
        {
            if (enemys[i] != null)
            {
                enemys[i].SetActive(true); // Ȱ��ȭ
                ChangeSpawnPosition(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(100, 160));
                enemys[i].transform.position = position; // ��ġ ����
            }        
            else
            {
                Debug.Log("������Ʈ �޴����� " + name + "(��)�� ����");
            }
        }
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