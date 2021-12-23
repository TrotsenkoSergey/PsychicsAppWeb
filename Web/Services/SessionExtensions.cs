using Microsoft.AspNetCore.Http;
using PlayGroundModel;
using System.IO;
using System.Text;

namespace Web.Services
{
    public static class SessionExtensions
    {
        private const string PLAYGROUND = "PlayGround";

        /// <summary>
        /// Сохраняет PlayGround в Session.
        /// </summary>
        /// <param name="session">Текущая Session</param>
        /// <param name="playGround">PlayGround</param>
        public static void SetPlayGround(this ISession session, IPlayGround playGround)
        {
            if (playGround == null)
            { return; }

            var playGroundBuffer = playGround.Save();

            session.Set(PLAYGROUND, playGroundBuffer);
        }

        /// <summary>
        /// Возвращает PlayGround из Session.
        /// </summary>
        /// <param name="session">Текущая Session</param>
        /// <param name="playGround">Игровая площадка</param>
        /// <returns>True если значение PlayGround имелось в Session</returns>
        public static bool TryGetPlayGround(this ISession session, out IPlayGround playGround)
        {
            if (session.TryGetValue(PLAYGROUND, out byte[] buffer))
            {
                playGround = PlayGroundFactory.GetPlayGround(buffer);
                return true; 
            }

            playGround = null;
            return false;
        }
    }
}
