using Assets.Libs.Config;
using Assets.Scripts.Language;
using Assets.Scripts.LogManager;
using Assets.Scripts.Module.ScreenManager;
using Assets.Scripts.Module.Snake;
using Assets.Scripts.SpriteManager;
using Assets.Scripts.ThemeModel;
using Assets.Scripts.View.GameCaption;
using Assets.Scripts.View.GameRuningView;
using Assets.Scripts.View.GameSetting;
using Assets.Scripts.View.HomeView;

using UnityEngine;

/// <summary>
/// 程序入口，挂载在UICamera下
/// </summary>
public class APPStart : MonoBehaviour
{
    /// <summary>
    /// Unity��Ϣ
    /// </summary>
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;

        //帧率定为60帧
        Application.targetFrameRate = 60;
    }
    
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        var uiRoot = GameObject.Find("UIRoot").transform;
        ScreenManager.instance.Init();
        LogManager.instance.Init();
        LanguageModel.instance.Init();
        ThemeModel.instance.Init();
        SpriteManager.instance.Init();
        FontManager.instance.Init();
        GlobalSetting.instance.Init();
        HomeView.instance.Init(uiRoot.Find("HomeView"));
        GameRunningView.instance.Init(uiRoot.Find("GameRunningView"));
        ConfigManager.instance.LoadLastConfig();
        GameSettingView.instance.Init(uiRoot.Find("GameSettingView"));
        GameCaptionView.instance.Init(uiRoot.Find("GameCaptionView"));
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        GameRunningView.instance.Update();
        ScreenManager.instance.Update();
    }

    /// <summary>
    /// The on trigger enter 2 d.
    /// </summary>
    /// <param name="collision">
    /// The collision.
    /// </param>
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Snake.instance.OnTriggerEnter2D(collision);
    }

    /// <summary>
    /// The on disable. 
    /// </summary>
    void OnDisable()
    {
        ConfigManager.instance.SaveLastConfig();
        LogManager.instance.Quit();
    }
}
