namespace AdventOfCode2022
{
    public class Day03 : BaseDay
    {
        public override int DayNumber { get; set; } = 3;

        public override string PuzzlePart1(bool training)
        {
            string[] lines = GetInput(training);
            int totalPriority = 0;
            foreach(var line in lines)
            {
                char dupe = FindDuplicateItem(line);
                totalPriority += _priorityMap[dupe];
            }
            Console.WriteLine(totalPriority);
            return totalPriority.ToString();
        }

        public override string PuzzlePart2(bool training)
        {
            string[] lines = GetInput(training);
            int totalPriority = 0;
            for(int groups = 0; groups < lines.Length / 3; groups++)
            {
                var startingPoint = groups * 3;
                var r1 = lines[startingPoint];
                var r2 = lines[startingPoint + 1];
                var r3 = lines[startingPoint + 2];

                List<string> rucksacks = new List<string> { r1, r2, r3 };
                List<string> orderedRucksacks = rucksacks.OrderByDescending(s => s.Length).ToList();

                for(int i = 0; i < orderedRucksacks[0].Length; i++)
                {
                    char item = orderedRucksacks[0][i];
                    if (orderedRucksacks[1].Contains(item) && orderedRucksacks[2].Contains(item))
                    {
                        totalPriority += _priorityMap[item];
                        break;
                    }
                }
            }
            Console.WriteLine(totalPriority);
            return totalPriority.ToString();
        }

        private Dictionary<char, int> _priorityMap = new Dictionary<char, int>
        {
            {'a',1},{'b',2},{'c',3},{'d',4},{'e',5},
            {'f',6},{'g',7},{'h',8},{'i',9},{'j',10},
            {'k',11},{'l',12},{'m',13},{'n',14},{'o',15},
            {'p',16},{'q',17},{'r',18},{'s',19},{'t',20},
            {'u',21},{'v',22},{'w',23},{'x',24},{'y',25},
            {'z',26},{'A',27},{'B',28},{'C',29},{'D',30},
            {'E',31},{'F',32},{'G',33},{'H',34},{'I',35},
            {'J',36},{'K',37},{'L',38},{'M',39},{'N',40},
            {'O',41},{'P',42},{'Q',43},{'R',44},{'S',45},
            {'T',46},{'U',47},{'V',48},{'W',49},{'X',50},
            {'Y',51},{'Z',52}
        };

        private char FindDuplicateItem(string rucksack)
        {
            var dividingLine = (rucksack.Length) / 2;
            var firstCompartment = rucksack.Substring(0, dividingLine);
            var secondCompartment = rucksack.Substring(dividingLine);

            foreach (char c in firstCompartment)
            {
                if (secondCompartment.Contains(c))
                {
                    return c;
                }
            }

            throw new ArgumentException($"Invalid rucksack {rucksack}");
        }
    }
}
