using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 테스트 완료 후 Debug.Log 모두 지워주기 바람

public class StageUIEvent : MonoBehaviour
{
    public GameObject popup; // 팝업 프리팹

    // 나가기 버튼
    public void QuitGame()
    {
        Debug.Log("Quit");
    }

    // 나가기 취소
    public void QuitGameCancle()
    {
        Debug.Log("Quit Cancle");
        popup.SetActive(false);
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
        popup.SetActive(true);
    }

    // 셋팅 옵션
    public void Settings()
    {
        Debug.Log("Settings");
    }
}
