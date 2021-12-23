using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayGroundModel
{
    internal class PlayGround : IPlayGround
    {
        /// <summary>
        /// Конструктор новой игровой площадки.
        /// </summary>
        /// <param name="numberOfPsychics">Количество участников</param>
        public PlayGround(int numberOfPsychics = 0)
        {
            if (numberOfPsychics != 0)
            { SetRangeOfRandomPsychics(numberOfPsychics); }
            else
            { SetRandomPsychic(); }
        }

        public PlayGround() { } // для сериализации

        /// <summary>
        /// Количество попыток.
        /// </summary>
        public int Iterations { get; set; } = 0;

        /// <summary>
        /// Указатель чей сейчас ход (экстрасенсов или участника).
        /// </summary>
        public bool IsPsychicsMove { get; set; } = true;

        /// <summary>
        /// Участник игры.
        /// </summary>
        public Participant User { get; set; } = new Participant();

        /// <summary>
        /// Экстрасенсы.
        /// </summary>
        public List<Psychic> Psychics { get; set; } = new List<Psychic>();
        
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
        /// Ход экстрасенсов и попытка отгадать значение.
        /// </summary>
        private PlayGround PsychicsMove()
        {
            foreach (var psychics in Psychics)
            {
                psychics.CurrentAnswer = psychics.GuessNumber();
                psychics.AnswerHistory.Add(psychics.CurrentAnswer);
            }
            return this;
        }

        /// <summary>
        /// Переключает ход экстрасенсов и участника.
        /// </summary>
        private void Switch()
        {
            if (IsPsychicsMove)
                IsPsychicsMove = false;
            else
                IsPsychicsMove = true;
        }

        /// <summary>
        /// Записывает последующее загаданное значение Участнику.
        /// </summary>
        /// <param name="desiredValue">Загаданное значение</param>
        public IPlayGround SetNextDesiredValue(int desiredValue)
        {
            User.DesiredValue = desiredValue;
            User.HistoryOfDesiredValue.Add(desiredValue);

            return this;
        }

        /// <summary>
        /// Вычисление уровня достоверности в зависимости от загаданного значения.
        /// </summary>
        private PlayGround Result()
        {
            foreach (var psychic in Psychics)
            {
                // фиксируем разницу между предполагаем значением и загаданным пользователем
                int oddsBtwUser = (psychic.CurrentAnswer >= User.DesiredValue)
                    ? psychic.CurrentAnswer - User.DesiredValue
                    : User.DesiredValue - psychic.CurrentAnswer;

                psychic.PreviousConfidenceLevel = psychic.ConfidenceLevel;

                // перебираем и в зависимости от результата определяем уровень достоверности
                if (oddsBtwUser == 0)
                {
                    psychic.SuccessfulAttempts++;
                    psychic.ConfidenceLevel += 50;
                }
                else if (oddsBtwUser > 0 && oddsBtwUser < 10)
                {
                    psychic.ConfidenceLevel += 20;
                }
                else if (oddsBtwUser >= 10 && oddsBtwUser < 20)
                {
                    psychic.ConfidenceLevel += 10;
                }
                else if (oddsBtwUser >= 20 && oddsBtwUser < 30)
                {
                    continue;
                }
                else if (oddsBtwUser >= 30 && oddsBtwUser < 40)
                {
                    psychic.ConfidenceLevel -= 10;
                }
                else if (oddsBtwUser >= 40 && oddsBtwUser < 60)
                {
                    psychic.ConfidenceLevel -= 20;
                }
                else // oddsBtwUser >= 60
                {
                    psychic.ConfidenceLevel -= 30;
                }
            }
            return this;
        }

        /// <summary>
        /// Запуск следующего хода (программа сама решит кто это Экстрасенсы или Участник).
        /// </summary>
        public void Run()
        {
            if (IsPsychicsMove)
            {
                Iterations++;
                PsychicsMove().Switch();
            }
            else
            { Result().Switch(); }
        }

        /// <summary>
        /// Сохраняет значение PlayGround.
        /// </summary>
        /// <returns>Значение PlayGround в byte[]</returns>
        public byte[] Save()
        {
            return Memento.Serialize(this);
        }

        /// <summary>
        /// Получить уникальное имя.
        /// </summary>
        /// <returns>Имя</returns>
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

        /// <summary>
        /// Получить участника.
        /// </summary>
        /// <returns>Участник</returns>
        public IParticipant GetUser()
        {
            return User;
        }

        /// <summary>
        /// Получить экстрасенсов.
        /// </summary>
        /// <returns>Поддерживает ожидание, возвращает экстрасенсов</returns>
        public async Task<IEnumerable<IPsychic>> GetPsychicsAsync()
        {
            return await Task.Run(() => Psychics);
        }
    }
}
