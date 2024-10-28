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
            int n = cards.Count;
            while (n > 1)
            {
                int k = random.Next(n--);
                var temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }
        }

        /// <summary>
        /// Раздача карты из колоды
        /// </summary>
        /// <returns></returns>
        public Card Deal()
        {
            while (cards.Count == 0)
            {
                Console.WriteLine("Колода пуста. Инициализация и перемешивание...");
                InitializeDeck();
                Shuffle();
            }

            Card card = cards[0];
            cards.RemoveAt(0);
            Console.WriteLine($"Раздана карта: {card.Rank} {card.Suit}");
            return card;
        }


    }
}
