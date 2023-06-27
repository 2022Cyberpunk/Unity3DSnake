using Assets.Scripts.EventManager;
using Assets.Scripts.View.GameRuningView;
using Assets.Scripts.View.HomeView;

namespace Assets.Scripts.Module.Snake
{
    using Assets.MessageBox;
    using Assets.MessageBox.Scripts;

    /// <summary>
    /// 重新开始
    /// </summary>
    public static class RestartItem
    {
        /// <summary>
        /// 重新开始
        /// </summary>
        public static void Restart()
        {
            Snake.instance.Clear();
            Snake.instance.TimeRestart();
            FoodMaker.instance.Destroy();
            FoodMaker.instance.CreateFood(false);
            EventDispatcher.TriggerEvent(ConfigEvent.kRestartChange);
        }

        /// <summary>
        /// 返回主界面
        /// </summary>
        public static void ReturnHome()
        {
            GameRunningView.instance.Close();
            HomeView.instance.Open();
            MessageBox.Close();
        }
    }
}
