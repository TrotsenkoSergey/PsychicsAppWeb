using PlayGroundModel;

namespace Web.Services
{
    /// <summary>
    /// Интерфейс для взаимодействия с контроллером.
    /// </summary>
    public interface IPlayGroundService
    {
        void SetNewPlayGround(int numberOfPsychics);

        bool TryGetPlayGround(out IPlayGround playGround);

        IPlayGroundService SetNextDesiredValue(int desiredValue);

        IPlayGroundService Run();

        void UpdateSession();
    }
}
