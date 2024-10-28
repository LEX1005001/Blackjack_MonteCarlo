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
        private AIStrategy strategy;
        private string strategyName;

        public AIPlayer(AIStrategy strategy, string strategyName)
        {
            this.strategy = strategy;
            this.strategyName = strategyName;
        }

        public override bool ShouldHit()
        {

            return strategy.ShouldHit(CalculateScore());
        }

        public string GetStrategyName()
        {
            return strategyName;
        }
    }
}
