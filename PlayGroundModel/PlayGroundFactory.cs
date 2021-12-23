namespace PlayGroundModel
{
    public static class PlayGroundFactory
    {
        public static IPlayGround GetPlayGround(byte[] playGroundBuffer)
        {
            return Memento.Deserialize(playGroundBuffer);
        }

        public static IPlayGround GetPlayGround(int numberOfPsychics)
        {
            return new PlayGround(numberOfPsychics);
        }
    }
}
