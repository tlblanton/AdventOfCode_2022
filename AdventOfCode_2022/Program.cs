using System.Collections.Generic;
using System.Data;

internal class Program
{
    public static Dictionary<string, int> day3CharPriorities = new Dictionary<string, int>();
    


    private static void Main(string[] args)
    {
        const string directory = "/Projects/AdventOfCode_2022/AdventOfCode_2022";
        const string day1_inputFile1 = "day1_input1.txt";
        const string day2_inputFile1 = "day2_input1.txt";
        const string day2_test = "day2_test.txt";
        const string day3_inputFile1 = "day3_input1.txt";
        const string day3_test = "day3_test.txt";
        const string day4_inputFile1 = "day4_input1.txt";
        const string day4_test = "day4_test.txt";


        //Day1_ElfCalories(Path.Combine(directory, day1_inputFile1));
        //Day2_RockPaperScissors(Path.Combine(directory, day2_inputFile1));
        //Day3_Rucksacks(Path.Combine(directory, day3_inputFile1));
        Day4_Cleaning(Path.Combine(directory, day4_inputFile1));


        Console.ReadKey();
    }

    public static void Day4_Cleaning(string filePath)
    {
        string line;
        var overlappingRangeCount = 0;

        using(var reader = new StreamReader(filePath))
        {
            while((line = reader.ReadLine()) != null)
            {
                var ranges = line.Split(',');

                var range1 = ranges[0].Split('-').Select(x => Convert.ToInt32(x)).ToList();
                var range2 = ranges[1].Split('-').Select(x => Convert.ToInt32(x)).ToList();


                if (range1[1] < range2[0] || range2[1] < range1[0])
                {
                    continue;
                }
                else
                {
                    overlappingRangeCount++;
                }


                //if (range1[0] <= range2[0] && range1[1] >= range2[1])
                //{
                //    overlappingRangeCount++;
                //}
                //else if (range2[0] <= range1[0] && range2[1] >= range1[1])
                //{
                //    overlappingRangeCount++;
                //}
            }
        }

        Console.WriteLine($"The number of shifts with any overlap is {overlappingRangeCount}");
    }




    public static void Day3_Rucksacks(string filePath)
    {
        
        // populate char priorities
        for(int i = 0; i < 26; ++i)
        {
            day3CharPriorities.Add(Convert.ToChar(97+i).ToString(), i+1);
        }
        for (int i = 0; i < 26; ++i)
        {
            day3CharPriorities.Add(Convert.ToChar(65 + i).ToString(), (i+1)+26);
        }



        string line;
        int totalPriorityScore = 0;

        using (var reader = new StreamReader(filePath))
        {
            while ((line = reader.ReadLine()) != null)
            {
                //var line1 = line.Substring(0, line.Length / 2);
                //var line2 = line.Substring(line.Length / 2, line.Length/2);

                var line1 = line;
                line = reader.ReadLine();
                var line2 = line;
                line = reader.ReadLine();
                var line3 = line;

                List<char> valsInCommon = line1.Where(x => line2.Contains(x) && line3.Contains(x)).Distinct().ToList();
                
                foreach(var val in valsInCommon)
                {
                    totalPriorityScore += day3CharPriorities[val.ToString()];
                }
            }
        }

        Console.WriteLine($"The sum of the day 3 priorities is {totalPriorityScore}");
    }


    public static List<KeyValuePair<string, string>> PreProcessStrategyGuideValues(List<KeyValuePair<string, string>> expectationList)
    {
        List<KeyValuePair<string, string>> realStrategyGuide = new List<KeyValuePair<string, string>>();

        foreach(var entry in expectationList)
        {
            switch(entry.Key)
            {
                case "A": //rock
                    switch(entry.Value)
                    {
                        case "X":
                            // need to lose
                            realStrategyGuide.Add(new KeyValuePair<string, string>(entry.Key, "Z"));
                            break;
                        case "Y":
                            // need to draw
                            realStrategyGuide.Add(new KeyValuePair<string, string>(entry.Key, "X"));
                            break;
                        case "Z":
                            //need to win
                            realStrategyGuide.Add(new KeyValuePair<string, string>(entry.Key, "Y"));
                            break;
                    }
                    break;
                case "B": //paper
                    switch (entry.Value)
                    {
                        case "X":
                            // need to lose
                            realStrategyGuide.Add(new KeyValuePair<string, string>(entry.Key, "X"));
                            break;
                        case "Y":
                            // need to draw
                            realStrategyGuide.Add(new KeyValuePair<string, string>(entry.Key, "Y"));
                            break;
                        case "Z":
                            //need to win
                            realStrategyGuide.Add(new KeyValuePair<string, string>(entry.Key, "Z"));
                            break;
                    }
                    break;
                case "C":
                    switch (entry.Value)
                    {
                        case "X":
                            // need to lose
                            realStrategyGuide.Add(new KeyValuePair<string, string>(entry.Key, "Y"));
                            break;
                        case "Y":
                            // need to draw
                            realStrategyGuide.Add(new KeyValuePair<string, string>(entry.Key, "Z"));
                            break;
                        case "Z":
                            //need to win
                            realStrategyGuide.Add(new KeyValuePair<string, string>(entry.Key, "X"));
                            break;
                    }
                    break;
            }
        }

        return realStrategyGuide;
    }

    public static void Day2_RockPaperScissors(string filePath)
    {
        int rockVal = 1, paperVal = 2, scossorVal = 3, drawVal = 3, winVal = 6;

        Dictionary<string, int> selectionScore = new Dictionary<string, int>();
        selectionScore.Add("X", 1);
        selectionScore.Add("Y", 2);
        selectionScore.Add("Z", 3);


        List<KeyValuePair<string, string>> StrategyGuideWinRequirements = new List<KeyValuePair<string, string>>();

        

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var entries = line.Split(" ");
                StrategyGuideWinRequirements.Add(new KeyValuePair<string, string>(entries[0], entries[1]));
            }
        }

        var strategyGuide = PreProcessStrategyGuideValues(StrategyGuideWinRequirements);

        var totalScore = 0;

        foreach(var entry in strategyGuide)
        {
            var roundScore = 0;

            switch(entry.Key)
            {
                case "A":
                    roundScore += selectionScore[entry.Value];
                    switch (entry.Value)
                    {
                        case "X":
                            //draw
                            roundScore += drawVal;
                            break;
                        case "Y":
                            //win
                            roundScore += winVal;
                            break;
                        case "Z":
                            //loss
                            break;
                    }
                    break;
                case "B": // paper
                    roundScore += selectionScore[entry.Value];
                    switch (entry.Value)
                    {
                        case "X":
                            //loss
                            break;
                        case "Y":
                            //draw
                            roundScore += drawVal;
                            break;
                        case "Z":
                            //win
                            roundScore += winVal;
                            break;
                    }
                    break;
                case "C": //scissors
                    roundScore += selectionScore[entry.Value];
                    switch (entry.Value)
                    {
                        case "X":
                            //win
                            roundScore += winVal;
                            break;
                        case "Y":
                            //loss
                            break;
                        case "Z":
                            //draw
                            roundScore += drawVal;
                            break;
                    }
                    break;
            }

            totalScore += roundScore;
        }

        Console.WriteLine($"The total score is {totalScore}");

    }

    public static void Day1_ElfCalories(string filePath)
    {
        Dictionary<int, int> ElfCalories = new Dictionary<int, int>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            int count = 0;
            var currentElf = 1;
            var calorieCount = 0;

            while ((line = reader.ReadLine()) != null)
            {
                if (line != "")
                {
                    calorieCount += int.Parse(line);
                }
                else
                {
                    ElfCalories.Add(currentElf, calorieCount);
                    calorieCount = 0;
                    currentElf++;
                }
            }
        }

        var mostCalorieCarryingElf = ElfCalories.MaxBy(x => x.Value);
        Console.WriteLine($"The elf carrying the most calories is Elf #{mostCalorieCarryingElf.Key} with {mostCalorieCarryingElf.Value} calories on board.");

        var otherList = ElfCalories;
        otherList.Remove(mostCalorieCarryingElf.Key);
        var secondMostCalorieCarryingElf = otherList.MaxBy(x => x.Value);

        otherList.Remove(secondMostCalorieCarryingElf.Key);

        var thirdMostCalorieCarryingElf = otherList.MaxBy(x => x.Value);

        Console.WriteLine($"The three elves carrying the most calories are Elves " +
            $"{mostCalorieCarryingElf.Key}, " +
            $"{secondMostCalorieCarryingElf.Key}, " +
            $"and {thirdMostCalorieCarryingElf.Key} with {mostCalorieCarryingElf.Value + secondMostCalorieCarryingElf.Value + thirdMostCalorieCarryingElf.Value} calories on board.");


    }
}