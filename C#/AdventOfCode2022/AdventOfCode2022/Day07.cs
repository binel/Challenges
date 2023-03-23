using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day07 : BaseDay
    {
        public override int DayNumber { get; set; } = 7;

        public override string PuzzlePart1(bool training)
        {
           var root = MainLogic(training);

            return SumDirectory(root, 0).ToString();
        }

        public override string PuzzlePart2(bool training)
        {
            var root = MainLogic(training);
            int totalSpace = 70000000;
            int neededSpace = 30000000;
            int usedSpace = root.GetSize();
            int unusedSpace = totalSpace - usedSpace;

            int deleteSpaceRequired = neededSpace - unusedSpace;
            return FindSmallestDirectoryLargerThan(deleteSpaceRequired, null, root).GetSize().ToString();
        }

        public Directory MainLogic(bool training)
        {
            string[] lines = GetLinesOfInput(training);

            State s = new State { Input = lines };
            
            while (s.HasMoreLines)
            {
                s.AdvanceLine();
            }

            return s.RootDirectory;
        }

        public int SumDirectory(Directory d, int sum)
        {
            if (d.GetSize() <= 100000)
            {
                sum += d.GetSize();
            }

            foreach (var child in d.Children)
            {
                var childDir = child as Directory;
                if (childDir != null)
                {
                    sum = SumDirectory(childDir, sum);
                }
            }

            return sum;
        }

        public Directory FindSmallestDirectoryLargerThan(int size, Directory previousBest, Directory currentDirectory)
        {
            if (previousBest == null && currentDirectory.GetSize() > size)
            {
                previousBest = currentDirectory;
            }

            else if (currentDirectory.GetSize() > size && currentDirectory.GetSize() < previousBest.GetSize())
            {
                previousBest = currentDirectory;
            }

            foreach (var child in currentDirectory.Children)
            {
                var childDir = child as Directory;
                if (childDir != null)
                {
                    previousBest = FindSmallestDirectoryLargerThan(size, previousBest, childDir);
                }
            }

            return previousBest;
        }

        public class DirectoryItem 
        {
            public string Name { get; set; }

            public Directory Parent { get; set; }
        }

        public class File: DirectoryItem
        {
            public int Size { get; set; }

            public File(string line)
            {
                var split = line.Split(" ");
                Size = int.Parse(split[0]);
                Name = split[1];
            }
        }

        public class Directory: DirectoryItem
        {
            public List<DirectoryItem> Children = new List<DirectoryItem>();

            public Directory(string line)
            {
                var split = line.Split(" ");
                Name = split[1];
            }

            public Directory() { }

            public int GetSize()
            {
                int size = 0;
                foreach (var child in Children)
                {
                    File f = child as File;
                    if (f != null)
                    {
                        size += f.Size;
                    }
                    else {
                        size += (child as Directory).GetSize();
                    }
                }
                return size;
            }

            public bool HasSubDirectory(string name)
            {
                foreach (var child in Children)
                {
                    if (child.Name == name && child as Directory != null)
                    {
                        return true;
                    }
                }

                return false;
            }

            public static bool IsLineDirectory(string line)
            {
                if (line.StartsWith("dir"))
                {
                    return true;
                }

                return false;
            }

            public void PrintDirectory(int indent)
            {
                string prefix = new string(' ', indent);

                Console.WriteLine($"{prefix}- {Name} (dir)");
                foreach (var child in Children)
                {
                    var f = child as File;
                    if (f != null)
                    {
                        Console.WriteLine($"{prefix} - {f.Name} (file, size={f.Size})");
                    }
                    else
                    {
                        (child as Directory).PrintDirectory(indent + 1);
                    }
                }
            }
        }

        public enum Command 
        {
            CD,
            LS
        }

        public static class CommandParser
        {
            public static bool IsCdCommand(string line)
            {
                return line.StartsWith("$ cd");
            }

            public static string GetCdDirectory(string line)
            {
                return line.Split(" ")[2];
            }

            public static bool IsLsCommand(string line)
            {
                return line.StartsWith("$ ls");
            }
        }

        public class State
        {
            public Directory RootDirectory { get; } = new Directory { Name = "/" };

            public Directory CurrentDirectory { get; set; }

            public Command PriorCommand { get; set; }

            public string[] Input { get; set; }

            public string CurrentLine { get { return Input[InputPosition]; } }

            public int InputPosition { get; set; }

            public bool HasMoreLines => InputPosition < Input.Length - 1;

            public State()
            {
                CurrentDirectory = RootDirectory;
            }

            public void AdvanceLine()
            {
                InputPosition += 1;

                if (CommandParser.IsCdCommand(CurrentLine))
                {
                    PriorCommand = Command.CD;
                    var dest = CommandParser.GetCdDirectory(CurrentLine);

                    if (dest == "..")
                    {
                        CurrentDirectory = CurrentDirectory.Parent;
                    }
                    else  if (dest == "/")
                    {
                        CurrentDirectory = RootDirectory;
                    }
                    else
                    {
                        CurrentDirectory = (Directory)CurrentDirectory
                                            .Children
                                            .Find(d => d.Name == dest);
                    }
                }
                else if (CommandParser.IsLsCommand(CurrentLine))
                {
                    return;
                }
                else if (Directory.IsLineDirectory(CurrentLine))
                {
                    var newDir = new Directory(CurrentLine)
                    {
                        Parent = CurrentDirectory
                    };
                    CurrentDirectory.Children.Add(newDir);
                }
                else
                {
                    CurrentDirectory.Children.Add(new File(CurrentLine));    
                }
            }
        }
    }
}
