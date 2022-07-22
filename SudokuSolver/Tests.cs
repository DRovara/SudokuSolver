using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SudokuSolver
{
    public static class Tests
    {
        private static bool strict = true;
        private static bool CheckSolution(Game game, Game expectedOutcome)
        {
            var solver = new Solver(game);
            game = solver.Solve();
            if (game == null)
                return false;
            return game.Contains(expectedOutcome);
        }

        public static bool TestAll()
        {
            if (!BasicSolverTest())
            {
                if (strict)
                    throw new Exception();
                return false;
            }
            if (!HiddenPairsSolverTest())
            {
                if (strict)
                    throw new Exception(); 
                return false;
            }
            if (!HiddenSinglesSolverTest())
            {
                if (strict)
                    throw new Exception();
                return false;
            }
            if (!NakedPairsSolverTest())
            {
                if (strict)
                    throw new Exception();
                return false;
            }
            if (!IntersectionRemovalSolverTest())
            {
                if (strict)
                    throw new Exception();
                return false;
            }
            if (!XWingsSolverTest())
            {
                if (strict)
                    throw new Exception();
                return false;
            }
            if (!SimpleColouringSolverTest())
            {
                if (strict)
                    throw new Exception();
                return false;
            }
            if (!YWingsSolverTest())
            {
                if (strict)
                    throw new Exception();
                return false;
            }
            return true;
        }

        public static bool BasicSolverTest()
        {
            return CheckSolution(Game.Parse(File.ReadAllText("tests\\simple.txt")), Game.Parse(File.ReadAllText("tests\\simple-output.txt")));
        }

        public static bool HiddenSinglesSolverTest()
        {
            return CheckSolution(Game.Parse(File.ReadAllText("tests\\hidden-singles.txt")), Game.Parse(File.ReadAllText("tests\\hidden-singles-output.txt")));
        }

        public static bool NakedPairsSolverTest()
        {
            return CheckSolution(Game.Parse(File.ReadAllText("tests\\naked-pairs.txt")), Game.Parse(File.ReadAllText("tests\\naked-pairs-output.txt")));
        }

        public static bool HiddenPairsSolverTest()
        {
            return CheckSolution(Game.Parse(File.ReadAllText("tests\\hidden-pairs.txt")), Game.Parse(File.ReadAllText("tests\\hidden-pairs-output.txt")));
        }
        public static bool IntersectionRemovalSolverTest()
        {
            return CheckSolution(Game.Parse(File.ReadAllText("tests\\intersection-removal.txt")), Game.Parse(File.ReadAllText("tests\\intersection-removal-output.txt")));
        }

        public static bool XWingsSolverTest()
        {
            return CheckSolution(Game.Parse(File.ReadAllText("tests\\x-wings.txt")), Game.Parse(File.ReadAllText("tests\\x-wings-output.txt")));

        }

        public static bool SimpleColouringSolverTest()
        {
            return CheckSolution(Game.Parse(File.ReadAllText("tests\\simple-colouring.txt")), Game.Parse(File.ReadAllText("tests\\simple-colouring-output.txt")));
        }

        public static bool YWingsSolverTest()
        {
            return CheckSolution(Game.Parse(File.ReadAllText("tests\\y-wings.txt")), Game.Parse(File.ReadAllText("tests\\y-wings-output.txt")));
        }
    }
}
