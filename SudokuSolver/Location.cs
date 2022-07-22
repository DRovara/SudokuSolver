using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Location
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public int Box => Row / 3 * 3 + Column / 3;
        public int BoxRow => Row - (Box / 3) * 3;
        public int BoxColumn => Column - (Box % 3) * 3;
        public int BoxIndex => BoxRow * 3 + BoxColumn;

        public Location(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Location)
                return false;
            var l = (Location)obj;
            return l.Row == Row && l.Column == Column;
        }

        public static Location FromBox(int box, int index)
        {
            return new Location(box / 3 * 3 + index / 3, box % 3 * 3 + index % 3);
        }

        public static (Location, Location) Intersections(Location a, Location b)
        {
            var l1 = new Location(a.Row, b.Column);
            var l2 = new Location(b.Row, a.Column);
            return (l1, l2);
        }

        public static HashSet<Location> SeenByBoth(Location a, Location b)
        {
            var locations = new HashSet<Location>();
            for(var i = 0; i < 9; i++)
            {
                var rowElement = new Location(a.Row, i);
                var columnElement = new Location(i, a.Column);
                var boxElement = Location.FromBox(a.Box, i);
                if (!rowElement.Equals(a) && !rowElement.Equals(b) && b.Sees(rowElement))
                    locations.Add(rowElement);
                if (!columnElement.Equals(a) && !columnElement.Equals(b) && b.Sees(columnElement))
                    locations.Add(columnElement);
                if (!boxElement.Equals(a) && !boxElement.Equals(b) && b.Sees(boxElement))
                    locations.Add(boxElement);
            }
            return locations;
            /*if(a.Box == b.Box)
            {
                for(var i = 0; i < 9; i++)
                {
                    var l = Location.FromBox(a.Box, i);
                    if(!l.Equals(a) && !l.Equals(b))
                    {
                        locations.Add(l);
                    }
                }
            }
            if(a.Row == b.Row)
            {
                for(var i = 0; i < 9; i++)
                {
                    var l = new Location(a.Row, i);
                    if (!l.Equals(a) && !l.Equals(b))
                    {
                        locations.Add(l);
                    }
                }
            }
            if (a.Column == b.Column)
            {
                for (var i = 0; i < 9; i++)
                {
                    var l = new Location(i, a.Column);
                    if (!l.Equals(a) && !l.Equals(b))
                    {
                        locations.Add(l);
                    }
                }
            }
            var (l1, l2) = Intersections(a, b);
            if (!l1.Equals(a) && !l1.Equals(b))
            {
                locations.Add(l1);
            }
            if (!l2.Equals(a) && !l2.Equals(b))
            {
                locations.Add(l2);
            }
            return locations;*/
        }

        public static HashSet<Location> SeenByAll(params Location[] locations)
        {
            var seenByBoth = SeenByBoth(locations[0], locations[1]);
            for(var i = 2; i < locations.Length; i++)
            {
                var loc = locations[i];
                seenByBoth.Remove(loc);
                var toRemove = new HashSet<Location>();
                foreach(var seen in seenByBoth)
                {
                    if (!seen.Sees(loc))
                        toRemove.Add(seen);
                }
                foreach(var seen in toRemove)
                {
                    seenByBoth.Remove(seen);
                }
            }
            return seenByBoth;
        }

        public bool Sees(Location l)
        {
            if (l.Row == Row && l.Column == Column)
                return false;
            if (l.Row == Row)
                return true;
            if (l.Column == Column)
                return true;
            if (l.Box == Box)
                return true;
            return false;
        }

        public override string ToString()
        {
            return "R" + (Row + 1) + "C" + (Column + 1);
        }
    }
}
