using Blackjack_MonteCarlo;

// Класс Игрок-человек
public class HumanPlayer : Player
{
    public override bool ShouldHit()
    {
        // Спрашиваем у пользователя, хочет ли он взять еще одну карту
        Console.WriteLine("Ваша текущая рука: ");
        foreach (Card card in hand)
        {
            Console.WriteLine($"{card.Rank} {card.Suit}");
        }
        Console.WriteLine($"Ваш текущий результат: {CalculateScore()}");
        Console.Write("Вы хотите побить ставку? (y/n) ");
        string? input = Console.ReadLine();
        return input.ToLower() == "y";
    }
}

// Класс ИИ-игрока
public class AIPlayer : Player
{
    private Random random;

    public AIPlayer()
    {
        random = new Random();
    }

    public override bool ShouldHit()
    {
        // Простая стратегия ИИ-игрока: брать карту, если счет меньше 17
        return CalculateScore() < 17;
    }
}

// Класс Игра
public class Game
{
    private Deck deck;
    private HumanPlayer humanPlayer;
    private List<AIPlayer> aiPlayers;

    public Game()
    {
        // Инициализация колоды, игрока-человека и ИИ-игроков
        deck = new Deck();
        deck.Shuffle();
        humanPlayer = new HumanPlayer();
        aiPlayers = new List<AIPlayer>
        {
            new AIPlayer(),
            new AIPlayer(),
            new AIPlayer()
        };
    }

    public void Play()
    {
        // Раздача карт игрокам
        DealInitialCards();

        // Игровой цикл:
        PlayHumanTurn();
        PlayAITurns();
        DetermineWinner();
    }

    private void DealInitialCards()
    {
        // Раздача двух карт каждому игроку
        humanPlayer.AddCard(deck.Deal());
        humanPlayer.AddCard(deck.Deal());
        foreach (AIPlayer player in aiPlayers)
        {
            player.AddCard(deck.Deal());
            player.AddCard(deck.Deal());
        }
    }

    private void PlayHumanTurn()
    {
        // Ход игрока-человека
        while (humanPlayer.ShouldHit())
        {
            humanPlayer.AddCard(deck.Deal());
        }
    }

    private void PlayAITurns()
    {
        // Ход ИИ-игроков
        foreach (AIPlayer player in aiPlayers)
        {
            while (player.ShouldHit())
            {
                player.AddCard(deck.Deal());
            }
        }
    }

    private void DetermineWinner()
    {
        // Определение победителя
        int humanScore = humanPlayer.CalculateScore();
        Console.WriteLine($"Ваш итоговый счет: {humanScore}");

        bool playerBusted = humanScore > 21;
        if (playerBusted)
        {
            Console.WriteLine("Вы перебрали! Дом выиграл.");
            return;
        }

        int highestScore = humanScore;
        Player winner = humanPlayer;

        foreach (AIPlayer player in aiPlayers)
        {
            int playerScore = player.CalculateScore();
            Console.WriteLine($"Счет ИИ-игрока: {playerScore}");

            if (playerScore > 21)
            {
                continue;
            }

            if (playerScore > highestScore)
            {
                highestScore = playerScore;
                winner = player;
            }
        }

        if (winner == humanPlayer)
        {
            Console.WriteLine("Вы выиграли!");
        }
        else
        {
            Console.WriteLine($"ИИ-игрок выиграл!");
        }
    }

    /// <summary>
    /// Точка входа
    /// </summary>
    public static void Main()
    {
        Game game = new Game();
        game.Play();
    }
}

