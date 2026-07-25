using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

record Hero(string Name, int Health, int Power);
record Quest(string Title, int DifficultyLevel, int Bonus, TimeSpan Duration);

class Program
{
    static async Task Main()
    {
        var heroes = new List<Hero>
        {
            new Hero("Bob", 100, 50),
            new Hero("Alice", 80, 90)
        };

        var quests = new List<Quest>
        {
            new Quest("Dragon Cave", 70, 200, TimeSpan.FromSeconds(5)),
            new Quest("Mage Tower", 60, 150, TimeSpan.FromSeconds(2))
        };

        var tasks = new List<Task<string>>();

        for (int i = 0; i < heroes.Count; i++)
        {
            var hero = heroes[i];
            var quest = quests[i];

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

            tasks.Add(RunQuestAsync(hero, quest, cts.Token));
        }

        while (tasks.Count > 0)
        {
            var finishedTask = await Task.WhenAny(tasks);
            tasks.Remove(finishedTask);

            try
            {
                var result = await finishedTask;
                Console.WriteLine(result);
            }
            catch (OperationCanceledException)
            {
            }
        }

        Console.WriteLine("All quests finished.");
    }

    static async Task<string> RunQuestAsync(Hero hero, Quest quest, CancellationToken ct)
    {
        var startTime = DateTime.Now;
        Console.WriteLine($"[{Timestamp()}] {hero.Name} started \"{quest.Title}\"");

        try
        {
            await Task.Delay(quest.Duration, ct);

            var random = new Random();
            int luck = random.Next(-10, 20);

            bool success = hero.Power + luck >= quest.DifficultyLevel;

            if (success)
            {
                return $"[{Timestamp()}] {hero.Name} finished \"{quest.Title}\" — VICTORY! (+{quest.Bonus} gold)";
            }
            else
            {
                return $"[{Timestamp()}] {hero.Name} failed \"{quest.Title}\" — DEFEAT...";
            }
        }
        catch (OperationCanceledException)
        {
            return $"[{Timestamp()}] {hero.Name} retreated from \"{quest.Title}\" (timeout)";
        }
    }

    static string Timestamp()
    {
        return DateTime.Now.ToString("mm:ss.fff");
    }
}