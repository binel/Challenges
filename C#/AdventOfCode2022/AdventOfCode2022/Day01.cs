namespace AdventOfCode2022
{
    /*
     * Every line of input represents the calorie content of a snack an elf has.
     * 
     * Each elf has their own snacks, and their inventories are separated in the
     * input by a blank line. 
     * 
     * For part 1 the goal of the puzzle is to determine the total number of 
     * calories in the inventory of the elf with the most calories
     * 
     * For part 2 of the puzzle the goal is to find the total calorie content of
     * the combined inventory of the three elves who hold the most calories. 
     */

    public class Day01 : BaseDay
    {
        public override int DayNumber { get; set; } = 1;


        public override string PuzzlePart1(bool training)
        {
            string[] lines = GetInput(training);

            List<Elf> elves = GetElves(lines);

            var sortedElves = elves.OrderByDescending(e => e.Calories).ToList();
            return $"{sortedElves[0].Calories}";
        }

        public override string PuzzlePart2(bool training)
        {
            string[] lines = GetInput(training);

            List<Elf> elves = GetElves(lines);
            var sortedElves = elves.OrderByDescending(e => e.Calories).ToList();
            var top3Sum = sortedElves.Take(3).Sum(e => e.Calories);
            return $"{top3Sum}";
        }


        public class Elf
        {
            public int Calories { get; set; }
        }

        public static List<Elf> GetElves(string[] input)
        {
            List<Elf> elves = new List<Elf>();
            Elf currentElf = new();
            foreach (var l in input)
            {
                if (string.IsNullOrWhiteSpace(l))
                {
                    elves.Add(currentElf);
                    currentElf = new Elf();
                }
                else {
                    currentElf.Calories += int.Parse(l);
                }
            }
            elves.Add(currentElf);

            return elves;
        }
    }
}
