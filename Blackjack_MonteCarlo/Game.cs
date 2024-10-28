using Blackjack_MonteCarlo;


/// <summary>
/// Класс Игра
/// </summary>
public class Game
{
    private Deck deck;
    private List<AIPlayer> aiPlayers;
    private Dictionary<string, int> winsCount;
    private int totalGames;

    public Game()
    {
        deck = new Deck();
        aiPlayers = new List<AIPlayer>
        {
        new AIPlayer(new AggressiveStrategy(), "AggressiveStrategy"),
        new AIPlayer(new ConservativeStrategy(), "ConservativeStrategy"),
        new AIPlayer(new MonteCarloStrategy(), "MonteCarloStrategy")
        };
        winsCount = new Dictionary<string, int>();
        foreach (AIPlayer player in aiPlayers)
        {
            winsCount[player.GetStrategyName()] = 0;
        }
    }

    /// <summary>
    /// Симуляция игр
    /// </summary>
    /// <param name="numberOfGames">Кол-во игр</param>
    public void SimulateMultipleGames(int numberOfGames)
    {
        totalGames = numberOfGames;
        for (int i = 0; i < numberOfGames; i++)
        {
            deck.Shuffle();
            DealInitialCards();
            PlayAITurns();
            DetermineWinner();
            ResetGame();
        }
        DisplayStatistics();
    }

    /// <summary>
    /// Показать финальную статистику
    /// </summary>
    private void DisplayStatistics()
    {
        Console.WriteLine("Итоговая статистика:");
        foreach (var entry in winsCount)
        {
            Console.WriteLine($"{entry.Key}: выиграл {entry.Value} игр, Win Rate: {(double)entry.Value / totalGames * 100:F2}%");
        }
    }

    /// <summary>
    /// Выдаём в начале игры по 2 карты из колоды 
    /// </summary>
    private void DealInitialCards()
    {
        foreach (AIPlayer player in aiPlayers)
        {
            player.AddCard(deck.Deal());
            player.AddCard(deck.Deal());
        }
    }

    /// <summary>
    /// Проигрование AI-логики и в случае Hit даём карту из колоды
    /// </summary>
    private void PlayAITurns()
    {
        foreach (AIPlayer player in aiPlayers)
        {
            while (player.ShouldHit())
            {
                player.AddCard(deck.Deal());
            }
        }
    }

    /// <summary>
    /// Определение победителя
    /// </summary>
    private void DetermineWinner()
    {
        int highestScore = 0;
        AIPlayer winner = null;

        foreach (AIPlayer player in aiPlayers)
        {
            int playerScore = player.CalculateScore();
            if (playerScore <= 21 && playerScore > highestScore)
            {
                highestScore = playerScore;
                winner = player;
            }
        }

        if (winner != null)
        {
            winsCount[winner.GetStrategyName()]++;
            Console.WriteLine($"Выграла тактика {winner.GetStrategyName()}");
        }
    }

    private void ResetGame()
    {
        foreach (AIPlayer player in aiPlayers)
        {
            player.ResetCards();
        }
    }

    /// <summary>
    /// Запуск игр
    /// </summary>
    public static void Main()
    {
        Game game = new Game();
        game.SimulateMultipleGames(3);
    }
}

