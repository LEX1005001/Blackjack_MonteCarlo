using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_MonteCarlo
{
    /// <summary>
    /// Класс Карта
    /// </summary>
    public class Card
    {
        public int Rank { get; set; }
        public Suit Suit { get; set; }

    }

    /// <summary>
    /// Перечисление мастей
    /// </summary>
    public enum Suit
    {
        Трефы, Бубны, Червы, Пики
    }

}
