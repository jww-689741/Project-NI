using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// 테스트 완료 후 Debug.Log 모두 지워주기 바람

public class UiManager : MonoBehaviour
{
    public GameObject pauseUI;
    private bool pauseFlag = false;

    // UI 활성화 & 비활성화
    public void ActivateUI(GameObject target)
    {
        target.SetActive(true);
    }

    // UI 비활성화
    public void DisableUI(GameObject target)
    {
        target.SetActive(false);
    }

    // 씬 전환
    public void SceneTransition(string target)
    {
        SceneManager.LoadScene(target);
    }

    // 게임 종료
    public void QuitApplication()
    {
        Application.Quit();
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

    // 일시정지
    private void Pause(GameObject target)
    {
        if (!pauseFlag)
        {
            Time.timeScale = 0;
            target.SetActive(true);
            pauseFlag = true;
        }
        else
        {
            Time.timeScale = 1;
            target.SetActive(false);
            pauseFlag = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(pauseUI);
        }
    }

}
