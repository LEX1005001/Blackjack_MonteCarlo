using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_MonteCarlo
{
    // Класс ИИ-игрока
    public class AIPlayer : Player
    {
        private AIStrategy strategy;  // Класс стратегии
        private string strategyName;  // Имя стратегии

        public AIPlayer(AIStrategy strategy, string strategyName)
        {
            this.strategy = strategy;
            this.strategyName = strategyName;
        }

        public override bool ShouldHit()
        {

            return strategy.ShouldHit(CalculateScore());
        }

        /// <summary>
        /// Получение имени стратегии
        /// </summary>
        /// <returns></returns>
        public string GetStrategyName()
        {
            return strategyName;
        }
    }
}
