using System;
using System.Collections.Generic;

namespace PlayGroundModel
{
    public class PlayGround
    {
        /// <summary>
        /// Конструктор игровой площадки.
        /// </summary>
        /// <param name="numberOfParticipants">Количество участников</param>
        public PlayGround(int numberOfParticipants = 0)
        {
            if (numberOfParticipants != 0) SetRangeOfRandomPsychics(numberOfParticipants);
            else SetRandomPsychic();
            Iterations = 0;
        }

        public PlayGround() { } // конструктор для сериализации

        /// <summary>
        /// Количество попыток.
        /// </summary>
        public int Iterations { get; set; }

        /// <summary>
        /// Участники - экстрасенсы.
        /// </summary>
        public List<Psychic> Psychics { get; set; } = new List<Psychic>();

        /// <summary>
        /// Участник игры.
        /// </summary>
        public Participant User { get; set; } = new Participant();

        /// <summary>
        /// Добавить на площадку одного случайного участника-экстрасенса.
        /// </summary>
        public void SetRandomPsychic()
        {
            var psychic = new Psychic()
            {
                Name = GetUniqueRandomName()
            };
            Psychics.Add(psychic);
        }

        /// <summary>
        /// Добавить на площадку нескольких случайных участников-экстрасенсов.
        /// </summary>
        /// <param name="count">Количество участников.</param>
        public void SetRangeOfRandomPsychics(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var psychic = new Psychic()
                {
                    Name = GetUniqueRandomName()
                };
                Psychics.Add(psychic);
            }
        }

        /// <summary>
        /// Попытка отгадать значения участвующими экстрасенсами.
        /// </summary>
        public void RunNextIteration()
        {
            Iterations++;
            foreach (var psychics in Psychics)
            {
                psychics.AttepmtsCounter = Iterations;
                psychics.CurrentAnswer = psychics.GuessNumber();
                psychics.AnswerHistory.Add(psychics.CurrentAnswer);
            }
        }

        /// <summary>
        /// Вычисление уровня доверия в зависимости от результата.
        /// </summary>
        public void Result()
        {
            User.HistoryOfDesiredValue.Add(User.DesiredValue);

            foreach (var psychics in Psychics)
            {
                var num = Math.Abs(psychics.CurrentAnswer - User.DesiredValue);
                if (num == 0) // от 20 до 30 - значение уровня доверия не меняется
                {
                    psychics.SuccessfulAttempts++;
                    psychics.ConfidenceLevel += 50;
                }
                else if (num > 0 && num < 10) 
                {
                    psychics.ConfidenceLevel += 20;
                }
                else if (num >= 10 && num < 20)
                {
                    psychics.ConfidenceLevel += 10;
                }
                else if (num >= 30 && num < 40)
                {
                    psychics.ConfidenceLevel -= 10;
                }
                else if (num >= 40 && num < 60)
                {
                    psychics.ConfidenceLevel -= 20;
                }
                else psychics.ConfidenceLevel -= 30;

                if (psychics.ConfidenceLevel > 100) psychics.ConfidenceLevel = 100;
                if (psychics.ConfidenceLevel < 0) psychics.ConfidenceLevel = 0;
            }
        }

        private string GetUniqueRandomName()
        {
            string name;
            do
            {
                name = Tools.GetRandomName();
            }
            while (Psychics.Exists(e => e.Name == name));
            return name;
        }
    }
}
