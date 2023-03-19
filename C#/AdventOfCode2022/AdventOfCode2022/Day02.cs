using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day02 : BaseDay
    {
        private const string TRAINING_FILE_PATH = "Input/input_02_0.txt";
        private const string QUESTION_FILE_PATH = "Input/input_02_1.txt";

        private const string OPPONENT_ROCK     = "A";
        private const string OPPONENT_PAPER    = "B";
        private const string OPPONENT_SCISSORS = "C";

        private const string PLAYER_ROCK       = "X";
        private const string PLAYER_PAPER      = "Y";
        private const string PLAYER_SCISSORS   = "Z";

        private const string NEED_TO_LOOSE = "X";
        private const string NEED_TO_DRAW  = "Y";
        private const string NEED_TO_WIN   = "Z";

        private const int LOOSE_POINTS = 0;
        private const int DRAW_POINTS = 3;
        private const int WIN_POINTS = 6;

        private const int ROCK_POINTS = 1;
        private const int PAPER_POINTS = 2;
        private const int SCISSORS_POINTS = 3;

        private enum Throw
        {
            ROCK = ROCK_POINTS,
            PAPER = PAPER_POINTS,
            SCISSORS = SCISSORS_POINTS
        }

        private enum GameOutcome
        {
            WIN = WIN_POINTS,
            DRAW = DRAW_POINTS,
            LOOSE = LOOSE_POINTS
        }

        public override int DayNumber { get; set; } = 2;

        public override void PuzzlePart1(bool training)
        {
            string[] lines = GetLinesOfInput(training);
            int score = 0;
            foreach (var line in lines)
            {
                var split = line.Split(" ");
                var opponentPlay = ParseThrow(split[0]);
                var playerPlay = ParseThrow(split[1]);
                var outcome = DetermineOutcome(opponentPlay, playerPlay);

                score += (int)outcome;

                switch (playerPlay)
                {
                    case Throw.ROCK:
                        score += ROCK_POINTS;
                        break;
                    case Throw.PAPER:
                        score += PAPER_POINTS;
                        break;
                    case Throw.SCISSORS:
                        score += SCISSORS_POINTS;
                        break;
                    default:
                        throw new ArgumentException($"Unsupported player play {playerPlay}");
                }

                //Console.WriteLine($"Player {outcome} - score {score}");
            }

            Console.WriteLine($"Score: {score}");
        }

        public override void PuzzlePart2(bool training)
        {
            string[] lines = GetLinesOfInput(training);
            int score = 0;
            foreach (var line in lines)
            {
                var split = line.Split(" ");
                var opponentPlay = ParseThrow(split[0]);
                var neededOutcome = ParseOutcome(split[1]);
                var play = DeterminePlay(opponentPlay, neededOutcome);

                score += (int)neededOutcome;

                switch (play)
                {
                    case Throw.ROCK:
                        score += ROCK_POINTS;
                        break;
                    case Throw.PAPER:
                        score += PAPER_POINTS;
                        break;
                    case Throw.SCISSORS:
                        score += SCISSORS_POINTS;
                        break;
                    default:
                        throw new ArgumentException($"Unsupported player play {play}");
                }
            }

            Console.WriteLine($"Score: {score}");
        }

        private static Throw ParseThrow(string play)
        {
            switch (play)
            {
                case OPPONENT_ROCK:
                case PLAYER_ROCK:
                    return Throw.ROCK;
                case OPPONENT_PAPER:
                case PLAYER_PAPER:
                    return Throw.PAPER;
                case OPPONENT_SCISSORS:
                case PLAYER_SCISSORS:
                    return Throw.SCISSORS;
                default:
                    throw new ArgumentException($"Unsupported play: {play}");
            }
        }

        private static GameOutcome ParseOutcome(string outcome)
        {
            switch (outcome)
            {
                case NEED_TO_LOOSE:
                    return GameOutcome.LOOSE;
                case NEED_TO_DRAW:
                    return GameOutcome.DRAW;
                case NEED_TO_WIN:
                    return GameOutcome.WIN;
                default:
                    throw new ArgumentException($"Unsupported outcome: {outcome}");
            }
        }

        private static Throw DeterminePlay(Throw opponentPlay, GameOutcome neededOutcome)
        {
            if (neededOutcome == GameOutcome.DRAW)
            {
                return opponentPlay;
            }

            switch (opponentPlay)
            {
                case Throw.ROCK:
                    switch(neededOutcome)
                    {
                        case GameOutcome.WIN:
                            return Throw.PAPER;
                        case GameOutcome.LOOSE:
                            return Throw.SCISSORS;
                    }
                    break;
                case Throw.PAPER:
                    switch (neededOutcome)
                    {
                        case GameOutcome.WIN:
                            return Throw.SCISSORS;
                        case GameOutcome.LOOSE:
                            return Throw.ROCK;
                    }
                    break;
                case Throw.SCISSORS:
                    switch (neededOutcome)
                    {
                        case GameOutcome.WIN:
                            return Throw.ROCK;
                        case GameOutcome.LOOSE:
                            return Throw.PAPER;
                    }
                    break;
            }
            throw new InvalidOperationException($"bad");
        }

        private static GameOutcome DetermineOutcome(Throw opponentPlay, Throw playerPlay)
        {
            if (opponentPlay == playerPlay)
            {
                return GameOutcome.DRAW;
            }

            switch (opponentPlay)
            {
                case Throw.ROCK:
                    return playerPlay == Throw.PAPER ? GameOutcome.WIN : GameOutcome.LOOSE;
                case Throw.PAPER:
                    return playerPlay == Throw.SCISSORS ? GameOutcome.WIN : GameOutcome.LOOSE;
                case Throw.SCISSORS:
                    return playerPlay == Throw.ROCK ? GameOutcome.WIN : GameOutcome.LOOSE;
                default:
                    throw new ArgumentNullException($"Unsupported play {opponentPlay}");
            }
        }


    }
}
