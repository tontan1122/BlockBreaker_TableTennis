using UnityEngine;

public class UIManager : MonoBehaviour
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
    public void TitleUI()
    {
        smoothBlinkingText.TextDisplay();       //テキストの浮遊感の表現
    }

    /// <summary>
    /// 設定パネルのアクティブ
    /// </summary>
    public void SettingActive(bool active)
    {
        settingPanel.SetActive(active);
        smoothBlinkingText.SettingActiveCheck(active);
    }

    public bool getSettingActive
    {
        get { return settingPanel.activeSelf; }
    }

    /// <summary>
    /// ステージセレクトのUI挙動
    /// </summary>
    /// <param name="panelActive">パネルの表示、非表示</param>
    /// <param name="level">保存されている最高レベル</param>
    public void StageSelectUI(bool panelActive, int level)
    {
        stageSelectPanel.SetActive(panelActive);
        if (stageSelectPanel.activeSelf)    // パネルが表示されているなら
        {
            selectStageController.CheakSelectPush(level);    //ボタンのオンオフ更新
        }
    }

    /// <summary>
    /// ゲームのUI挙動
    /// </summary>
    /// <param name="panelActive">パネルの表示、非表示</param>
    public void GameUI(bool panelActive)
    {
        gamePanel.SetActive(panelActive);
    }

    /// <summary>
    /// ミスの表示テキストの変更
    /// </summary>
    /// <param name="missCount"></param>
    public void GameUI_MissCountText(int missCount)
    {
        gameUIController.MissCountText(missCount);  // ミスカウントのテキスト変更
    }

    /// <summary>
    /// ステージ数のテキスト変更
    /// </summary>
    /// <param name="level">現在のレベル</param>
    public void GameUI_ChangeStageText(int level)
    {
        gameUIController.ChangeStageText(level);    // ステージ数のテキスト変更
    }

    public void GameUI_HintPanel(bool active)
    {
        hintPanel.SetActive(active);
    }

    /// <summary>
    /// リザルトのUI挙動
    /// </summary>
    /// <param name="panelActive">パネルの表示、非表示</param>
    public void ResultUI(bool panelActive)
    {
        resultPanel.SetActive(panelActive);
    }

    /// <summary>
    /// 最後のステージかどうか判断
    /// </summary>
    /// <param name="level">現在のレベル</param>
    public void ResultUI_CheckStage(int level)
    {
        if (resultPanel.activeSelf)
        {
            resultController.CheckFinalStage(level);
        }
    }
}
