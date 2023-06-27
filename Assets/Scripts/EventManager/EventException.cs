using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.EventManager
{
    /// <summary>
    /// 事件异常类
    /// </summary>
    [Serializable]
    internal class EventException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public EventException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public EventException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
