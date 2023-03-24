namespace AdventOfCode2022
{
    /*
     * Each line of the input is a round in a long rock-paper-scissors tournament
     * 
     * The scoring system for the tournament is: 
     * - Wining gets you 6 points, a draw 3 points, loosing 0 points. 
     * - If you play a rock you get 1 point, paper 2 points, scissors 3 points 
     * 
     * (This is an iteresting scoring system - I wonder how it would impact play)
     * 
     * For part 1, the task is to determine your total score in the tournament, 
     * assuming that the second entry (x, y, and z) are your plays.
     * 
     * For part 2, the task is to determine your total score in the tournament 
     * assumign that the second entry is if you lost, drew, or won. 
     */
    public class Day02 : BaseDay
    {
        public override int DayNumber { get; set; } = 2;

        public override string PuzzlePart1(bool training)
        {
            int s = 0;
            GetInput(training).ToList().ForEach(l => s += Round1Score[l]);
            return s.ToString();
        }

        public override string PuzzlePart2(bool training)
        {
            int s = 0;
            GetInput(training).ToList().ForEach(l => s += Round2Score[l]);
            return s.ToString();
        }

        /*
         * A = Rock, B = Paper, C = Scissors 
         * X = Rock, Y = Paper, Z = Scissors 
         */
        public Dictionary<string, int> Round1Score = new Dictionary<string, int>()
        {
            {"A X", 4}, {"A Y", 8}, {"A Z", 3},
            {"B X", 1}, {"B Y", 5}, {"B Z", 9},
            {"C X", 7}, {"C Y", 2}, {"C Z", 6}
        };

        /*
         * A = Rock, B = Paper, C = Scissors 
         * X = Loose, Y = Draw, Z = Win
         */
        public Dictionary<string, int> Round2Score = new Dictionary<string, int>()
        {
            {"A X", 3}, {"A Y", 4}, {"A Z", 8},
            {"B X", 1}, {"B Y", 5}, {"B Z", 9},
            {"C X", 2}, {"C Y", 6}, {"C Z", 7}
        };
    }
}
