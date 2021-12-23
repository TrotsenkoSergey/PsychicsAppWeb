using Microsoft.AspNetCore.Http;
using PlayGroundModel;

namespace Web.Services
{
    /// <summary>
    /// Сервис для взаимодействия Session и PlayGroundModel.
    /// </summary>
    public class DataService : IDataService
    {
        private const string PLAYGROUND = "PlayGround";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Получает текущую Session.
        /// </summary>
        public ISession Session => _httpContextAccessor.HttpContext.Session;

        /// <summary>
        /// Пытается получить PlayGround из Session.
        /// </summary>
        /// <param name="playGround">Интерфейс игровой площадки</param>
        /// <returns>True если значение в Session имеется</returns>
        public bool TryGetPlayGround(out IPlayGround playGround)
        {
            if (Session.TryGetValue(PLAYGROUND, out byte[] buffer))
            {
                playGround = PlayGroundFactory.GetPlayGround(buffer);
                return true;
            }

            playGround = null;
            return false;
        }
       
        /// <summary>
        /// Сохраняем PlayGround в Session.
        /// </summary>
        public void SetPlayGround(IPlayGround playGround)
        {
            if (playGround == null)
            { return; }

            var playGroundBuffer = playGround.Save();

            Session.Set(PLAYGROUND, playGroundBuffer);
        }
    }
}
