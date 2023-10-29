using UniRx;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// UIの全体管理
/// </summary>
internal class UIManager : MonoBehaviour
{
    [SerializeField, Header("ステージセレクトパネル")]
    private GameObject stageSelectPanel;

    [SerializeField, Header("ゲームパネル")]
    private GameObject gamePanel;

    [SerializeField, Header("リザルトパネル")]
    private GameObject resultPanel;

    [SerializeField, Header("ヒント推奨パネル")]
    private GameObject hintPanel;

    [SerializeField, Header("設定パネル")]
    private GameObject settingPanel;

    [SerializeField, Header("クラス参照:UI関係")]
    private SmoothBlinkingText smoothBlinkingText;
    [SerializeField]
    private SelectStageController selectStageController;
    [SerializeField]
    private GameUIController gameUIController;
    [SerializeField]
    private ResultController resultController;

    [SerializeField, Header("UIアニメーション")]
    private PanelActiveAnimation[] panelActiveAnimation;

    private void Start()
    {
        stageSelectPanel.SetActive(false);
        gamePanel.SetActive(false);
        resultPanel.SetActive(false);
        hintPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    /// <summary>
    /// タイトルのUI挙動
    /// </summary>
    internal void TitleUI()
    {
        smoothBlinkingText.TextDisplay();       //テキストの浮遊感の表現
    }

    /// <summary>
    /// ステージセレクトのUI挙動
    /// </summary>
    /// <param name="panelActive">パネルの表示、非表示</param>
    /// <param name="level">保存されている最高レベル</param>
    /// <param name="movingStageNumber">移動先のステージ番号</param>
    internal void StageSelectUI(bool panelActive, int level, int movingStageNumber)
    {
        if (panelActive)
        {
            panelActiveAnimation[0].Open();
        }
        else
        {
            panelActiveAnimation[0].Close();
        }
        if (stageSelectPanel.activeSelf)    // パネルが表示されているなら
        {
            selectStageController.CheakSelectPush(level);    //ボタンのオンオフ更新
            selectStageController.SetScrollPosition(movingStageNumber);
        }
    }

    /// <summary>
    /// ゲームのUI挙動
    /// </summary>
    /// <param name="panelActive">パネルの表示、非表示</param>
    internal void GameUI(bool panelActive)
    {
        gamePanel.SetActive(panelActive);
    }

    /// <summary>
    /// ミスの表示テキストの変更
    /// </summary>
    /// <param name="missCount"></param>
    internal void GameUI_MissCountText(int missCount)
    {
        gameUIController.MissCountText(missCount);  // ミスカウントのテキスト変更
    }

    /// <summary>
    /// ステージ数のテキスト変更
    /// </summary>
    /// <param name="level">現在のレベル</param>
    internal void GameUI_ChangeStageText(int level)
    {
        gameUIController.ChangeStageText(level);    // ステージ数のテキスト変更
    }

    internal void GameUI_HintPanel(bool active)
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
    /// <param name="panelActive">パネルの表示、非表示</param>
    internal void ResultUI(bool panelActive)
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
    /// 最後のステージかどうか判断
    /// </summary>
    /// <param name="level">現在のレベル</param>
    internal void ResultUI_CheckStage(int level)
    {
        if (resultPanel.activeSelf)
        {
            resultController.CheckFinalStage(level);
        }
    }

    /// <summary>
    /// 設定パネルのアクティブ
    /// </summary>
    internal void SettingActive(bool active)
    {
        if (active)
        {
            panelActiveAnimation[3].Open();
        }
        else
        {
            panelActiveAnimation[3].Close();
        }
        smoothBlinkingText.SettingActiveCheck(active);
    }

    internal bool getSettingActive
    {
        get { return settingPanel.activeSelf; }
    }
}
