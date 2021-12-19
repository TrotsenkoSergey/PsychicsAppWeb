using System;
using System.Collections.Generic;

namespace PlayGroundModel
{
    public class PlayGround
    {
        /// <summary>
        /// Конструктор игровой площадки.
        /// </summary>
        /// <param name="numberOfPsychics">Количество участников</param>
        public PlayGround(int numberOfPsychics = 0)
        {
            if (numberOfPsychics != 0) SetRangeOfRandomPsychics(numberOfPsychics);
            else SetRandomPsychic();
        }

        public PlayGround() { } // конструктор для сериализации

        /// <summary>
        /// Количество попыток.
        /// </summary>
        public int Iterations { get; set; } = 0;

        /// <summary>
        /// Указывает чей сейчас ход (экстрасенсов или участника).
        /// </summary>
        public bool IsPsychicsMove { get; set; } = true;

        /// <summary>
        /// Экстрасенсы.
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
        private void RunNextIteration()
        {
            Iterations++;
            foreach (var psychics in Psychics)
            {
                psychics.AttepmtsCounter = Iterations;
                psychics.CurrentAnswer = psychics.GuessNumber();
                psychics.AnswerHistory.Add(psychics.CurrentAnswer);
            }
            IsPsychicsMove = false;
        }

        /// <summary>
        /// Вычисление уровня доверия в зависимости от результата.
        /// </summary>
        private void Result()
        {
            User.HistoryOfDesiredValue.Add(User.DesiredValue);

            foreach (var psychics in Psychics)
            {
                var num = Math.Abs(psychics.CurrentAnswer - User.DesiredValue);
                if (num == 0)
                {
                    psychics.SuccessfulAttempts++;
                    psychics.ConfidenceLevel += 50;
                }
                else if (num > 0 && num < 10)
                {
                    psychics.ConfidenceLevel += 20;
                }
                else if (num >= 10 && num < 20) // от 20 до 30 - значение уровня доверия не меняется
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

                IsPsychicsMove = true;
                //if (psychics.ConfidenceLevel > 100) psychics.ConfidenceLevel = 100;
                //if (psychics.ConfidenceLevel < 0) psychics.ConfidenceLevel = 0;
            }
        }

        /// <summary>
        /// Запуск следующего хода (программа сама решит кто это Экстрасенсы или участник).
        /// </summary>
        public void Run() 
        { if (IsPsychicsMove) RunNextIteration();
            else Result();
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
