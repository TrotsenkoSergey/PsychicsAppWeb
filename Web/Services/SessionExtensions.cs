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
        /// Сериализует значения PlayGround в Session с использованием BinaryWriter.
        /// </summary>
        /// <param name="session">Текущая Session</param>
        /// <param name="playGround">Игровая площадка</param>
        public static void SetPlayGround(this ISession session, PlayGround playGround)
        {
            if (playGround == null) return;

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
                    writer.Write(concretePsychic.AttepmtsCounter);
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

                // после сериализации отправляем значение в сессию
                session.Set(PLAYGROUND, stream.ToArray());
            }
        }

        /// <summary>
        /// Десериализует значения PlayGround в Session с использованием BinaryWriter.
        /// </summary>
        /// <param name="session">Текущая Session</param>
        /// <param name="playGround">Игровая площадка</param>
        /// <returns>True если значение PlayGround имелось в Session</returns>
        public static bool TryGetPlayGround(this ISession session, out PlayGround playGround)
        {
            if (session.TryGetValue(PLAYGROUND, out byte[] buffer))
            {
                using (var stream = new MemoryStream(buffer))
                using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    // десериализуем все в обратном порядке
                    playGround = new PlayGround();

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
                            AttepmtsCounter = reader.ReadInt32(),
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

                    // в случае успешной десериализации возвращаем true
                    return true;
                }
            }

            playGround = null;
            return false;
        }
    }
}
