namespace PlayGroundModel
{
    public static class PlayGroundFactory
    {
        /// <summary>
        /// Создает игровую площадку на основе сериализованных данных.
        /// </summary>
        /// <param name="playGroundBuffer">Сериализованные данные игровой площадки</param>
        /// <returns>Интерфейс игровой площадки</returns>
        public static IPlayGround GetPlayGround(byte[] playGroundBuffer)
        {
            return Memento.Deserialize(playGroundBuffer);
        }

        /// <summary>
        /// Создает новую игровую площадку.
        /// </summary>
        /// <param name="numberOfPsychics">Количество участвующих экстрасенсов</param>
        /// <returns>Интерфейс игровой площадки</returns>
        public static IPlayGround GetPlayGround(int numberOfPsychics)
        {
            return new PlayGround(numberOfPsychics);
        }
    }
}
