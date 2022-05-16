using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
// �׽�Ʈ �Ϸ� �� Debug.Log ��� �����ֱ� �ٶ�

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
    // �˾� Ȱ��ȭ
    public void PopUpActivation()
    {
        gameQuitPopUp.SetActive(true);
    }

    // �˾� �ݱ� ��ư
    public void PopUpExit()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false); // Ŭ���� ��ư�� �θ� UI�� ��Ȱ��ȭ
    }

    // ������ ��ư
    public void QuitGame()
    {
        if(SceneManager.GetActiveScene().name == "Main")
        {
            Application.Quit(); // ���� ����
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }

    // ������ ���
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

    // �������� �̾��ϱ�
    public void StageContinue()
    {
        Debug.Log("Stage Continue");
    }

    // �������� �ٽ��ϱ�
    public void StageRestart()
    {
        Debug.Log("Stage Restart");
    }
    public void Upgrade()
    {
       
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;    // Ŭ���� ������Ʈ(��ư)
        P = clickObject.transform.parent;                                          // Ŭ���� ������Ʈ�� �θ�
        gold = GameObject.FindWithTag("Gold");                                     // ���� ������ �ִ� ���
        string text = clickObject.transform.GetChild(0).GetComponent<Text>().text;
        string tempStr = Regex.Replace(text,@"\D","");
        int countchanger = int.Parse(P.GetChild(3).GetComponent<Text>().text);
        int goldchanger = int.Parse(tempStr);
        int goldtext = int.Parse(gold.transform.GetChild(0).GetComponent<Text>().text);
        if (countchanger < 5 && goldchanger < goldtext)                           // ��ȭī��Ʈ�� 5�̸��̰� ������ �ִ� ��尡 ����� ��
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