using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
// 테스트 완료 후 Debug.Log 모두 지워주기 바람

public class UIEvent : MonoBehaviour
{
    [SerializeField]
    private PlayerStat statusD;
    [SerializeField]
    private PlayerStat statusK;
    [SerializeField]
    private PlayerStat statusS;

    public GameObject gameQuitPopUp;
    public GameObject settingWindow;
    public GameObject upgradeWindow;
    private GameObject gold;
    private Transform P;
    //private Button button;
    // 팝업 활성화
    public void PopUpActivation()
    {
        gameQuitPopUp.SetActive(true);
    }

    // 팝업 닫기 버튼
    public void PopUpExit()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false); // 클릭한 버튼의 부모 UI를 비활성화
    }

    // 나가기 버튼
    public void QuitGame()
    {
        if(SceneManager.GetActiveScene().name == "Main")
        {
            Application.Quit(); // 게임 종료
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }

    // 나가기 취소
    public void QuitGameCancle()
    {
        gameQuitPopUp.SetActive(false);
    }

    public void SettingActivation()
    {
        settingWindow.SetActive(true);
    }

    public void UpgradeActivation()
    {
        upgradeWindow.SetActive(true);
    }

    // 스테이지 이어하기
    public void StageContinue()
    {
        Debug.Log("Stage Continue");
    }

    // 스테이지 다시하기
    public void StageRestart()
    {
        Debug.Log("Stage Restart");
    }
    public void Upgrade()
    {
       
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;    // 클릭한 오브젝트(버튼)
        P = clickObject.transform.parent;                                          // 클릭한 오브젝트의 부모
        gold = GameObject.FindWithTag("Gold");                                     // 현재 가지고 있는 골드
        string text = clickObject.transform.GetChild(0).GetComponent<Text>().text;
        string tempStr = Regex.Replace(text,@"\D","");
        int countchanger = int.Parse(P.GetChild(3).GetComponent<Text>().text);
        int goldchanger = int.Parse(tempStr);
        int goldtext = int.Parse(gold.transform.GetChild(0).GetComponent<Text>().text);
        if (countchanger < 5 && goldchanger < goldtext)                           // 강화카운트가 5미만이고 가지고 있는 골드가 충분할 때
        {
            countchanger += 1;
            goldtext -= goldchanger; 
            goldchanger += 20000;
            Debug.Log("0 :" + P.name);
            SetStatUp(P.name);
           
        }
        P.GetChild(3).GetComponent<Text>().text = countchanger.ToString();
        clickObject.transform.GetChild(0).GetComponent<Text>().text = goldchanger.ToString() + "G";
        gold.transform.GetChild(0).GetComponent<Text>().text = goldtext.ToString();

    }
    public void SetStatUp(string name)
    {
        statusD.StatUp(name);
        statusK.StatUp(name);
        statusS.StatUp(name);
    }
}