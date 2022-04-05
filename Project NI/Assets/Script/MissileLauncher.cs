using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] GameObject m_goMissile = null;        //미사일 프리팹 변수 선언
    [SerializeField] Transform m_tfMissileSpawn = null;    //발사될 위치 변수 선언

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))                //스페이스바 -> 미사일 생성 -> 위로 발사
        {
            GameObject t_missile = Instantiate(m_goMissile, m_tfMissileSpawn.position, Quaternion.identity);
            t_missile.GetComponent<Rigidbody>().velocity = Vector3.up * 5f;
        }
    }
}
