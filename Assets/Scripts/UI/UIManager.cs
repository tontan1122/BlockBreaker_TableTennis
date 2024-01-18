using UnityEngine;

/// <summary>
/// UIの全体管理
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField, Header("ゲームパネル")]
    private GameObject gamePanel;

    [SerializeField, Header("リザルトパネル")]
    private GameObject resultPanel;

    [SerializeField, Header("裏卓球部のイメージ")]
    private GameObject hardModeImage;

    [SerializeField, Header("クラス参照:UI関係")]
    private SmoothBlinkingText smoothBlinkingText;
    [SerializeField]
    private SelectStageController selectStageController;
    [SerializeField]
    private GameUIController gameUIController;
    [SerializeField]
    private ResultController resultController;
    [SerializeField]
    private PauseUIController pauseUIController;

    [SerializeField, Header("UIアニメーション")]
    private PanelActiveAnimation[] panelActiveAnimation;

    private void Start()
    {
        gamePanel.SetActive(false);
        hardModeImage.SetActive(false);

        for (int i = 0; i < panelActiveAnimation.Length; i++)
        {
            panelActiveAnimation[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        DisplayHardodeImage();
    }

    /// <summary>
    /// タイトルのUI挙動
    /// </summary>
    public void OperateTitleUI()
    {
        smoothBlinkingText.DisplayText();       //テキストの浮遊感の表現
    }

    /// <summary>
    /// タイトルのテキストの変更
    /// </summary>
    public void ChangeTitleText()
    {
        smoothBlinkingText.ChangeText();
    }

    /// <summary>
    /// 裏の表示をする
    /// </summary>
    private void DisplayHardodeImage()
    {
        if (Time.timeScale == 1.5f)
        {
            if (!hardModeImage.activeSelf)
            {
                hardModeImage.SetActive(true);
            }
        }
        else
        {
            if (hardModeImage.activeSelf)
            {
                hardModeImage.SetActive(false);
            }
        }
    }

    /// <summary>
    /// ステージセレクトのUI挙動
    /// </summary>
    /// <param name="panelActive">パネルの表示、非表示</param>
    /// <param name="level">保存されている最高レベル</param>
    /// <param name="movingStageNumber">移動先のステージ番号</param>
    public void OperateStageSelectUI(bool panelActive, int level, int movingStageNumber)
    {
        if (panelActive)
        {
            panelActiveAnimation[0].Open();
            selectStageController.SetScrollPosition(movingStageNumber); // ボタンの位置調整
            selectStageController.ChangeButtonInteractivity(level);    //ボタンのオンオフ更新
        }
        else
        {
            panelActiveAnimation[0].Close();
        }
    }

    /// <summary>
    /// ゲームのUI挙動
    /// </summary>
    /// <param name="panelActive">パネルの表示、非表示</param>
    public void OperateGameUI(bool panelActive)
    {
        gamePanel.SetActive(panelActive);
    }

    /// <summary>
    /// ミスの表示テキストの変更
    /// </summary>
    /// <param name="missCount"></param>
    public void OperateMissCountText(int missCount)
    {
        gameUIController.ChengeMissCountText(missCount);  // ミスカウントのテキスト変更
    }

    /// <summary>
    /// ステージ数のテキスト変更
    /// </summary>
    /// <param name="level">現在のレベル</param>
    public void OperateStageLevelText(int level)
    {
        gameUIController.ChangeStageText(level);    // ステージ数のテキスト変更
    }

    /// <summary>
    /// ヒントパネルの表示、非表示
    /// </summary>
    /// <param name="active">パネルを表示するかどうか</param>
    public void SwitchHintPanelVisibility(bool active)
    {
        if (active)
        {
            panelActiveAnimation[2].Open();
        }
        else
        {
            panelActiveAnimation[2].Close();
        }
    }

    /// <summary>
    /// リザルトのUI挙動
    /// </summary>
    /// <param name="panelActive">パネルを表示するかどうか</param>
    public void SwitchResultPanelVisibility(bool panelActive)
    {
        if (panelActive)
        {
            panelActiveAnimation[1].Open();
        }
        else
        {
            panelActiveAnimation[1].Close();
        }
    }

    /// <summary>
    /// どのステージかの確認
    /// </summary>
    /// <param name="level">現在のレベル</param>
    public void CheckStageLevel(int level)
    {
        if (resultPanel.activeSelf)
        {
            resultController.CheckFinalStage(level);
        }
    }

    /// <summary>
    /// 設定パネルのアクティブ
    /// </summary>
    public void SettingActive(bool active)
    {
        if (active)
        {
            panelActiveAnimation[3].Open();
        }
        else
        {
            panelActiveAnimation[3].Close();
        }
        smoothBlinkingText.SetSettingActive(active);
    }

    /// <summary>
    /// ポーズパネルを表示、非表示する
    /// </summary>
    /// <param name="active">ポーズするかどうか</param>
    public void PausePanelActive(bool active)
    {
        if (active)
        {
            panelActiveAnimation[4].Open();
            pauseUIController.Pause();
        }
        else
        {
            panelActiveAnimation[4].Close();
            pauseUIController.Resume();
        }
    }

    /// <summary>
    /// 終了確認画面の表示、非表示
    /// </summary>
    /// <param name="active">表示するかどうか</param>
    public void QuitGamePanelActive(bool active)
    {
        if (active)
        {
            panelActiveAnimation[5].Open();
        }
        else
        {
            panelActiveAnimation[5].Close();
        }
        smoothBlinkingText.SetQuitActive(active);
    }

    /// <summary>
    /// クレジットパネルの表示、非表示
    /// </summary>
    /// <param name="active">表示するかどうか</param>
    public void CreditPanelActive(bool active)
    {
        if (active)
        {
            panelActiveAnimation[6].Open();
        }
        else
        {
            panelActiveAnimation[6].Close();
        }
    }

    /// <summary>
    /// 何かしらのパネルが表示されているかどうか
    /// </summary>
    /// <returns>何かしらのパネルが表示されているかどうか</returns>
    public bool GetAnyPanelActive()
    {
        bool value = false;
        for (int i = 0; i < panelActiveAnimation.Length; i++)
        {
            if (panelActiveAnimation[i].gameObject.activeSelf)
            {
                value = true;
            }
        }
        return value;
    }
}
