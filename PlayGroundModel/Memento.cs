using System.Collections.Generic;
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
                writer.Write(playGround.User.GetCountOfHistoryValues());
                foreach (var concreteDesiredValue in playGround.User.GetHistoryOfDesiredValue())
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
                    writer.Write(concretePsychic.GetCountOfAnswerHistory());
                    foreach (var concreteAnswer in concretePsychic.GetAnswerHistory())
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
                // десериализуем все в обратном порядке для объекта PlayGround
                var iterations = reader.ReadInt32();
                var isPsychicsMove = reader.ReadBoolean();

                // Participant
                var desiredValue = reader.ReadInt32();
                int lengthHistoryOfDesiredValue = reader.ReadInt32();
                var historyOfDesiredValue = new List<int>();
                for (int i = 0; i < lengthHistoryOfDesiredValue; i++)
                {
                    historyOfDesiredValue.Add(reader.ReadInt32());
                }
                var user = new Participant(desiredValue, historyOfDesiredValue);

                // Psychics
                var psychics = new List<Psychic>();
                int lengthPsychics = reader.ReadInt32();
                for (int i = 0; i < lengthPsychics; i++)
                {
                    var name = reader.ReadString();
                    var successfulAttempts = reader.ReadInt32();
                    var confidenceLevel = reader.ReadInt32();
                    var previousConfidenceLevel = reader.ReadInt32();
                    var currentAnswer = reader.ReadInt32();

                    var answerHistory = new List<int>();
                    int lengthAnswerHistory = reader.ReadInt32();
                    for (int j = 0; j < lengthAnswerHistory; j++)
                    {
                        answerHistory.Add(reader.ReadInt32());
                    }

                    var psychic = new Psychic(name, successfulAttempts, confidenceLevel, previousConfidenceLevel, currentAnswer, answerHistory);
                    psychics.Add(psychic);
                }

                var playGround = new PlayGround(iterations, isPsychicsMove, user, psychics);
                return playGround;
            }
        }
    }
}
