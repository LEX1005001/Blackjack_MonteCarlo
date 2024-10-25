using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_MonteCarlo
{
    /// <summary>
    /// Класс Колода
    /// </summary>
    public class Deck
    {
        private List<Card> cards;

        /// <summary>
        ///Инициализация колоды
        /// </summary>
        public Deck()
        {
            cards = new List<Card>();
            InitializeDeck();

        }

        /// <summary>
        /// функция для инициализация колоды
        /// </summary>
        public void InitializeDeck()
        {
            cards.Clear();
            for (int rank = 1; rank <= 13; rank++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    cards.Add(new Card { Rank = rank, Suit = suit });
                }
            }
        }

        /// <summary>
        ///Перемешивание колоды
        /// </summary>
        public void Shuffle()
        {
            Random random = new Random();
            cards = cards.OrderBy(x => random.Next()).ToList();
        }

        /// <summary>
        /// Раздача карты из колоды
        /// </summary>
        /// <returns></returns>
        public Card Deal()
        {
            if (cards.Count > 0)
            {
                Card card = cards[0];
                cards.RemoveAt(0);
                return card;
            }
            // Логика для случая, когда карты в колоде закончились.
            else
            {
                // Повторно инициализироваем и перемешать колоду.
                InitializeDeck();
                Shuffle();
                return Deal(); // Рекурсивный вызов Deal для получения карты после переинициализации колоды.
            }
        }

    }
}
