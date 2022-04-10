using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 개체당 개별 이동 클레스
public class EnemyMove : MonoBehaviour
{
    Vector3 position = new Vector3(0, 0, 0);
    public float movementSpeed; // 이동속도
    GameObject player;
    Rigidbody rb;

    public float waitTime;

    private delegate void Control();
    Control control;

    void Start()
    {
        position = this.gameObject.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.gameObject.GetComponent<Rigidbody>();
        control += move;
        //control += run;
        control += AutoMove;
        //control += CheckActive;
        // control += RotateAroundPlayer;
    }

    void Update()
    {
        control();
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2f);
    }

    private void move()
    {
        this.gameObject.transform.LookAt(player.transform);
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, position, movementSpeed * Time.deltaTime);
    }

    public IEnumerator MoveToPosition(float timeToMove)
    {
        ChangeVector3(Random.Range(-15, 15), Random.Range(-15, 15), Random.Range(-15, 15));
        var currentPos = this.gameObject.transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }

    private void CheckActive()
    {
        if (this.gameObject.transform.position.z < player.transform.position.z - 10)
        {
            this.gameObject.SetActive(false); // 비활성화
        }
        else if (this.gameObject.transform.position.x < player.transform.position.x - 50)
        {
            this.gameObject.SetActive(false); // 비활성화
        }
    }

    // 들이받기
    public void MoveToPlayer()
    {
        position.Set(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    public void ChangeVector3(float x, float y, float z)
    {
        position.x = x;
        position.y = y;
        position.z = z;
    }

    public void ChangeVector3ToPlayer(float x, float y, float z)
    {
        position.x = x + player.transform.position.x;
        position.y = y + player.transform.position.y;
        position.z = z + player.transform.position.z;
    }

    // 일정거리 유지
    public void StayPosition()
    {
        position.z = player.transform.position.z;
    }

    public void RotateAroundPlayer()
    {
        this.gameObject.transform.RotateAround(player.transform.position, Vector3.down, movementSpeed * Time.deltaTime);
    }

    public void RotateAroundPosition()
    {
        this.gameObject.transform.RotateAround(position, Vector3.up, movementSpeed * Time.deltaTime);
    }

    void run()
    {
        transform.position = Vector3.Slerp(this.gameObject.transform.position, position, movementSpeed * Time.deltaTime * 0.1f);
    }

    float CalculDistance()
    {
        Vector3 start = this.gameObject.transform.position;

        return Mathf.Sqrt((Mathf.Pow((position.x - start.x), 2) + Mathf.Pow((position.z - start.z), 2)) + Mathf.Pow((position.y - start.y), 2));
    }

    float timer = 0.0f;

    void AutoMove()
    {
        Vector3 mep = this.gameObject.transform.position;
        float x = 0;
        float y = 0;
        float z = 0;

        timer += Time.deltaTime;


        if (mep.x > player.transform.position.x - 20 && mep.x < player.transform.position.x + 20)
        {
            x = Random.Range(-20, 20);
        }
        else
        {
            x = Random.Range(-20, 20);
        }

        if (mep.y > player.transform.position.y - 15 && mep.y < player.transform.position.y + 15)
        {
            y = Random.Range(-15, 15);
        }
        else
        {
            y = Random.Range(-15, 15);
        }

        if (mep.z > player.transform.position.z + 10)
        {
            z = player.transform.position.z;
        }
        else if (mep.z <= player.transform.position.z + 10)
        {
            z = Random.Range(30, 70);
            ChangeVector3(x, y, z);
            timer = 0;
        }

        if (timer > 2)
        {
            ChangeVector3(x, y, z);
            timer = 0;
        }
    }
}
