using System.IO;
using System.Text;

namespace PlayGroundModel
{
    internal static class Memento
    {
        public static byte[] Serialize(PlayGround playGround)
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                // сериализуем объект PlayGround
                writer.Write(playGround.Iterations);
                writer.Write(playGround.IsPsychicsMove);
                // сериализуем объект Participant
                writer.Write(playGround.User.DesiredValue);
                writer.Write(playGround.User.HistoryOfDesiredValue.Count);
                foreach (var concreteDesiredValue in playGround.User.HistoryOfDesiredValue)
                {
                    writer.Write(concreteDesiredValue);
                }
                // сериализуем объекты Psychic
                writer.Write(playGround.Psychics.Count);
                foreach (var concretePsychic in playGround.Psychics)
                {
                    writer.Write(concretePsychic.Name);
                    writer.Write(concretePsychic.SuccessfulAttempts);
                    writer.Write(concretePsychic.ConfidenceLevel);
                    writer.Write(concretePsychic.PreviousConfidenceLevel);
                    writer.Write(concretePsychic.CurrentAnswer);
                    writer.Write(concretePsychic.AnswerHistory.Count);
                    foreach (var concreteAnswer in concretePsychic.AnswerHistory)
                    {
                        writer.Write(concreteAnswer);
                    }
                }

                return stream.ToArray();
            }
        }

        public static PlayGround Deserialize(byte[] playGroundBuffer)
        {

            using (var stream = new MemoryStream(playGroundBuffer))
            using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                // десериализуем все в обратном порядке
                var playGround = new PlayGround();

                playGround.Iterations = reader.ReadInt32();
                playGround.IsPsychicsMove = reader.ReadBoolean();

                playGround.User.DesiredValue = reader.ReadInt32();
                int lengthHistoryOfDesiredValue = reader.ReadInt32();
                for (int i = 0; i < lengthHistoryOfDesiredValue; i++)
                {
                    playGround.User.HistoryOfDesiredValue.Add(reader.ReadInt32());
                }

                int lengthPsychics = reader.ReadInt32();
                for (int i = 0; i < lengthPsychics; i++)
                {
                    var psychic = new Psychic()
                    {
                        Name = reader.ReadString(),
                        SuccessfulAttempts = reader.ReadInt32(),
                        ConfidenceLevel = reader.ReadInt32(),
                        PreviousConfidenceLevel = reader.ReadInt32(),
                        CurrentAnswer = reader.ReadInt32()
                    };
                    int lengthAnswerHistory = reader.ReadInt32();
                    for (int j = 0; j < lengthAnswerHistory; j++)
                    {
                        psychic.AnswerHistory.Add(reader.ReadInt32());
                    }
                    playGround.Psychics.Add(psychic);
                }

                return playGround;
            }
        }
    }
}
