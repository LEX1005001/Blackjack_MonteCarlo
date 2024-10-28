using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_MonteCarlo
{
    // Абстрактный класс стратегии ИИ-игрока
    public abstract class AIStrategy
    {
        public abstract bool ShouldHit(int currentScore);
    }

    // Атакующая/рисковая стратегия
    public class AggressiveStrategy : AIStrategy
    {
        private Random random = new Random();

        public override bool ShouldHit(int currentScore)
        {
            // Увеличиваем вероятность хита, если счёт меньше 20
            int threshold = 20;
            if (currentScore == 19)
            {
                return random.NextDouble() < 0.25; // 25% вероятность риска при счете 19
            }
            return currentScore < threshold;
        }
    }

    // Защитная/пассивная стратегия
    public class ConservativeStrategy : AIStrategy
    {
        private Random random = new Random();

        public override bool ShouldHit(int currentScore)
        {
            // Добавляем случайность к порогу хита
            int threshold = 14 + random.Next(0, 3); // Случайно изменяем порог на 0, 1 или 2 пункта
            return currentScore < threshold;
        }
    }


    // Стратегия Монте-Карло
    public class MonteCarloStrategy : AIStrategy
    {
        private Random random;

        public MonteCarloStrategy()
        {
            random = new Random();
        }

        public override bool ShouldHit(int currentScore)
        {
            const int simulations = 1000; // Кол-во симуляций для принятия решения
            int wins = 0;                 //Кол-во побед в симуляции против дилера

            for (int i = 0; i < simulations; i++)
            {
                int simulatedScore = currentScore;
                bool playerBusted = false; // Перебрал карты

                // Имитация действий игрока
                while (simulatedScore < 21 && ShouldHitInSimulation(simulatedScore))
                {
                    simulatedScore += random.Next(1, 11); // Имитация получения случайной карты
                    if (simulatedScore > 21)
                    {
                        playerBusted = true;
                        break;
                    }
                }

                // Если игрок перебрал, пропускаем симуляцию
                if (playerBusted) continue;

                // Имитация действий дилера
                int dealerScore = 0;
                while (dealerScore < 17)
                {
                    dealerScore += random.Next(1, 11); // Имитация получения случайной карты
                    if (dealerScore > 21) break; // Если дилер перебрал, выходим из цикла
                }

                // Определение результата
                if (simulatedScore > dealerScore || dealerScore > 21)
                {
                    wins++;
                }
            }

            return (double)wins / simulations > 0.5; // Если вероятность выигрыша больше 50%, то "бить"
        }


        private bool ShouldHitInSimulation(int currentScore)
        {
            return currentScore < 17; // Например, игрок бьет, если у него меньше 17
        }
    }
}
