using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] GameObject m_goMissile = null;        //미사일 프리팹 변수 선언
    [SerializeField] Transform m_tfMissileSpawn = null;    //발사될 위치 변수 선언
    [SerializeField] LayerMask m_layerMask = 0; //원하는 레이어만 검출해주는 LayerMask 선언
    private int i = 0;
    float time = 0;
    float coolTime = 0;
    int count = 0;
    int cart = 3; // 장탄 수
    bool flag = false;
    bool coolFlag = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && cart > 0 && !coolFlag)                //스페이스바 -> 미사일 생성 -> 위로 발사
        {
            count = SearchEnemy();
            flag = true;
            coolFlag = true;
            cart--;
        }
        if (coolFlag)
        {
            coolTime += Time.deltaTime;
            Debug.Log(coolTime);
            if (coolTime >= 1)
            {
                coolFlag = false;
                coolTime = 0;

            }
        }
        if(flag && time>0.1f)
        {
                SetMissile(ObjectManager.instance.GetBullet("MissileBomb"),count);
                time = 0;
                count--;

                if (count == 0) flag = false;
          
        }
    
    }

    public int GetCart()
    {
        return this.cart;
    }

    private void SetMissile(GameObject Missile, int count)
    {
        var bulletTf = Missile.transform; // 탄환의 transform값
        var playerTf = this.transform; // 플레이어의 transform값
        bulletTf.position = new Vector3(playerTf.position.x, playerTf.position.y, playerTf.position.z+1); // 위치 지정

        if (Missile == null) return; // 받아올 탄환이 없을 경우 반환
        Missile.SetActive(true); // 활성화
        Missile.GetComponent<MissileBomb>().StartCoroutine("Launch",count);
        
        
    }
    int SearchEnemy()  //표적 탐지 함수
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, 500f, m_layerMask);  //반경 100m내의 특정 레이어 컬라이더 검출
        Debug.Log(t_cols.Length);

       if (t_cols.Length > 0)
        {
            return t_cols.Length;
        }
        else
        {
            return 0;
        }
    }
}
