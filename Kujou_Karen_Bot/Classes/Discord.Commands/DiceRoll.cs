using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Kujou_Karen_Bot.Classes.Commands.Helpers;

namespace Kujou_Karen_Bot.Classes.Commands
{
    [Name("Dice Rolling"), Summary("Rolls a number of dice, a specified amount of times.")]
    public class DiceRoll : ModuleBase<SocketCommandContext>
    {
        List<Die> dice;

        Random rand = new Random();
        Regex regex;
        Match match;
        string patternCount = "\\d+[dD]", patternSize = "[dD]\\d+", patternAddition = "\\+\\d+", patternSubtraction = "\\-\\d+", patternMultiplication = "\\*\\d+", patternDivision = "\\/\\d+";

        [Command("roll", RunMode = RunMode.Async), Remarks("[amount]d<size>[+modifier][-modifier][⋅modifier][÷modifier]"), Summary("!roll 2d20+3-1*4/2")]
        public async Task RollDie(params string[] command)
        {
            dice = new List<Die>();

            foreach (string input in command)
            {
                dice.Add(Convert(input));
            }

            var result = Roll(dice);

            await ReplyAsync(getRoll(result));
        }

        private List<String> Roll(List<Die> dice)
        {
            List<String> builds = new List<String>();

            // Total for all dice rolled
            int cumulative = 0, numOfDie = 0;

            foreach (Die die in dice)
            {
                for (int i = 0; i < die.count; i++)
                {
                    numOfDie += 1;

                    int num = 1 + rand.Next(die.size);
                    String temp = "Rolling a D" + die.size + "; Got: " + num;

                    if (die.addition > 0)
                    {
                        temp += "; (+" + die.addition + ")";
                    }

                    if (die.subtraction > 0)
                    {
                        temp += "; (-" + die.subtraction + ")";
                    }

                    if (die.multiplication > 0)
                    {
                        temp += "; (*" + die.multiplication + ")";
                    }

                    if (die.division > 0)
                    {
                        temp += "; (/" + die.division + ")";
                    }

                    int total = num;
                    temp += "; Total: ";

                    // PEMDAS here
                    if (die.multiplication > 0)
                    {
                        total *= die.multiplication;
                    }

                    if (die.division > 0)
                    {
                        total /= die.division;
                    }

                    if (die.addition > 0)
                    {
                        total += die.addition;
                    }

                    if (die.subtraction > 0)
                    {
                        total -= die.subtraction;
                    }

                    temp += total;

                    builds.Add(temp);

                    cumulative += total;
                }
            }

            if (numOfDie > 1) 
            {
                builds.Add($"Cumulative total: {cumulative}");
            }

            dice.Clear();
            cumulative = 0;
            numOfDie = 0;

            return builds;
        }

        private string getRoll(List<string> results)
        {
            string output = "";

            foreach (string temp in results)
            {
                output += temp + "\n";
            }

            return output;
        }

        private Die Convert(string input)
        {
            int count = 1, size = 1, addition = 0, subtraction = 0, multiplication = 0, division = 0;

            // Number of die, first
            regex = new Regex(@patternCount);
            match = regex.Match(input);

            if (match.Success)
            {
                count = int.Parse(match.Value.Substring(0, match.Value.Length - 1));
            }

            // Size of die second.
            regex = new Regex(@patternSize);
            match = regex.Match(input);

            if (match.Success)
            {
                size = int.Parse(match.Value.Substring(1));
            }

            // Addition.
            regex = new Regex(@patternAddition);
            match = regex.Match(input);

            if (match.Success)
            {
                addition = int.Parse(match.Value.Substring(1));
            }

            // Subtraction.
            regex = new Regex(@patternSubtraction);
            match = regex.Match(input);

            if (match.Success)
            {
                subtraction = int.Parse(match.Value.Substring(1));
            }

            // Multiplication.
            regex = new Regex(@patternMultiplication);
            match = regex.Match(input);

            if (match.Success)
            {
                multiplication = int.Parse(match.Value.Substring(1));
            }

            // Division.
            regex = new Regex(@patternDivision);
            match = regex.Match(input);

            if (match.Success)
            {
                division = int.Parse(match.Value.Substring(1));
            }

            return new Die(count, size, addition, subtraction, multiplication, division);
        }
    }
}
