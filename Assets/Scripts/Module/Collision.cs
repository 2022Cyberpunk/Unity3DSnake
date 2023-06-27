namespace Assets.Scripts.Module
{
    using UnityEngine;

    /// <summary>
    /// 碰撞检测,挂载在小蛇头部
    /// </summary>
    public class Collision : MonoBehaviour
    {
        /// <summary>
        /// The on trigger enter 2 d.
        /// </summary>  
        /// <param name="collision">
        /// The collision.
        /// </param>
        void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            Snake.Snake.instance.OnTriggerEnter2D(collision);
        }
    }
}
