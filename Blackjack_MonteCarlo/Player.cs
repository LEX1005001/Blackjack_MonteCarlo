using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_MonteCarlo
{
    /// <summary>
    /// Абстрактный класс Игрок
    /// </summary>
    public abstract class Player
    {
        protected List<Card> hand;

        public Player()
        {
            hand = new List<Card>();
        }

        /// <summary>
        /// Подсчет очков игрока
        /// </summary>
        /// <returns>очки/(score)</returns>
        public int CalculateScore()
        {
            int score = 0;
            int numAces = 0;
            foreach (Card card in hand)
            {
                if (card.Rank == 1) // Туз
                {
                    numAces++;
                    score += 11;
                }
                else if (card.Rank >= 10) // Фигурные карты (с картинками)
                {
                    score += 10;
                }
                else // Обычные карты
                {
                    score += card.Rank;
                }
            }

            // Если счет превышает 21 и у игрока есть тузы, уменьшаем значение тузов до 1
            while (score > 21 && numAces > 0)
            {
                score -= 10;
                numAces--;
            }

            return score;
        }

        public abstract bool ShouldHit();

        public void AddCard(Card card)
        {
            // Добавление карты в руку игрока
            hand.Add(card);
        }
    }
}
