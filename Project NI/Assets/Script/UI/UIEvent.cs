using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 테스트 완료 후 Debug.Log 모두 지워주기 바람

public class UIEvent : MonoBehaviour
{
    public GameObject gameQuitPopUp;
    public GameObject settingWindow;
    public GameObject upgradeWindow;

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
        Application.Quit(); // 게임 종료
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

    // 스테이지 종료
    public void StageQuit()
    {
        Debug.Log("Stage Quit");
    }

}
