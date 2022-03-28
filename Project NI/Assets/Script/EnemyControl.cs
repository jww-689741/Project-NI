using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float movementSpeed; // 이동속도
    GameObject player;
    GameObject[] enemys;
    bool stop = true;
    List<int> counts = new List<int>();
    private EnemyMove script;
    int padd;

    Vector3 position = Vector3.zero;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(testCoroutine());
    }

    void Update()
    {

    }

    private IEnumerator testCoroutine()
    {
        int i = 0;

        counts.Add(10);

        while (stop)
        {
            yield return new WaitForSeconds(3f);

            SpawnEnemy(counts[i]);

            if (counts.Count - 1 == i)
            {
                stop = false;
            }

            i++;
        }
    }

    private void SpawnEnemy(int count)
    {
        enemys = ObjectManager.instance.GetEnemy("test", count);

        for (int i = 0; i < count; i++)
        {
            enemys[i].SetActive(true);
        }

        SetVEnemyPosition(enemys);
        //AddSpawnPosition(enemys, 0, 30, 100);
    }

    private void SetHorizontalEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            padd = (middle - i) * -2;
            ChangeSpawnPosition(padd, 0, 20);
            enemys[i].transform.position = position;
        }
    }

    private void SetVerticalEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            padd = (middle - i) * -2;
            ChangeSpawnPosition(0, 0, 20 + padd);
            enemys[i].transform.position = position;
        }
    }

    private void SetVEnemyPosition(GameObject[] enemys)
    {
        int middle = MiddleNumber(enemys.Length);

        for (int i = 0; i < enemys.Length; i++)
        {
            padd = (middle - i) * -2;
            ChangeSpawnPosition(21 + padd, 16, 150 - abs(padd));
            enemys[i].transform.position = position; // 위치 지정
        }
    }

    private int MiddleNumber(int count)
    {
        return Mathf.CeilToInt(count / 2); // 중앙에 있을 오브젝트 번호
    }

    private void ChangeSpawnPosition(float x, float y, float z)
    {
        position = new Vector3(x + player.transform.position.x, y + player.transform.position.y, z + player.transform.position.z);
    }

    private void AddSpawnPosition(GameObject[] enemy, float x, float y, float z)
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            position = new Vector3(x + enemys[i].transform.position.x, y + enemys[i].transform.position.y, z + enemys[i].transform.position.z);
            enemys[i].transform.position = position;
        }
    }

    private int abs(int num)
    {
        if (num < 0)
        {
            return num * -1;
        }
        else
            return num;
    }
}