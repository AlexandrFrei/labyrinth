using System;
using System.Linq;
using System.IO;

namespace ЛР1
{
    class Program
    {
        public static bool North = false, South = true, West = false, East = false;
        public static bool fromNorth = true, fromSouth = false, fromWest = false, fromEast = false;
        public static int lX = 0, lY = 0;
        public struct Room
        {
            public bool North;
            public bool South;
            public bool West;
            public bool East;
        }

        public static void False()
        {
            fromEast = false;
            fromNorth = false;
            fromSouth = false;
            fromWest = false;
        }

        public static Room Move(Room room)
        {
            if (North == true)
            {
                room.North = true;
                lY--;
                False();
                fromSouth = true;
            }
            else if (South == true)
            {
                room.South = true;
                lY++;
                False();
                fromNorth = true;
            }
            else if (West == true)
            {
                room.West = true;
                lX--;
                False();
                fromEast = true;
            }
            else if (East == true)
            {
                room.East = true;
                lX++;
                False();
                fromWest = true;
            }
            return room;
        }

        public static void Reset()
        {
            North = false; South = true; West = false; East = false;
            fromNorth = true; fromSouth = false; fromWest = false; fromEast = false;
        }

        public static Room Turn(Room room)
        {
            room.North = room.North || fromNorth;
            room.South = room.South || fromSouth;
            room.West = room.West || fromWest;
            room.East = room.East || fromEast;
            return room;
        }

        public static void TurnLeft()
        {
            if (North == true)
            {
                West = true;
                North = false;
            }
            else if (West == true)
            {
                South = true;
                West = false;
            }
            else if (South == true)
            {
                East = true;
                South = false;
            }
            else if (East == true)
            {
                North = true;
                East = false;
            }
        }

        public static void TurnRight()
        {
            if (North == true)
            {
                East = true;
                North = false;
            }
            else if (West == true)
            {
                North = true;
                West = false;
            }
            else if (South == true)
            {
                West = true;
                South = false;
            }
            else if (East == true)
            {
                South = true;
                East = false;
            }
        }

        public static void TurnBack()
        {
            if (North == true)
            {
                South = true;
                North = false;
            }
            else if (West == true)
            {
                East = true;
                West = false;
            }
            else if (South == true)
            {
                North = true;
                South = false;
            }
            else if (East == true)
            {
                West = true;
                East = false;
            }
        }

        public static string Read(string[] lines)
        {
            int N = Convert.ToInt32(lines[0]);
            string listMap = "";
            for (int i = 1; i < lines.Length; i++)
            {
                Reset();
                listMap += "Case #" + i + ":\n";
                string[] path = lines[i].Split(' ');
                int count;
                if(path[0].Where(x => x == 'W').Count()>= path[1].Where(x => x == 'W').Count())
                {
                    count = path[0].Where(x => x == 'W').Count();
                }
                else
                {
                    count = path[1].Where(x => x == 'W').Count();
                }
                Room[,] Labyrinth = new Room[count+1, count * 2 + 1];
                lX = count;
                lY = 0;
                for (int j = 0; j < 2; j++)
                {
                    for (int q = 1; q < path[j].Length-1; q++)
                    {
                        if (path[j][q] == 'W')
                        {
                            Labyrinth[lY, lX] = Turn(Labyrinth[lY, lX]);
                            Labyrinth[lY, lX] = Move(Labyrinth[lY, lX]);
                        }
                        else if (path[j][q] == 'L')
                        {
                            Labyrinth[lY, lX] = Turn(Labyrinth[lY, lX]);
                            TurnLeft();
                        }
                        else if (path[j][q] == 'R')
                        {
                            if (path[j][q + 1] == 'R')
                            {
                                TurnBack();
                                q++;
                            }
                            else
                            {
                                Labyrinth[lY, lX] = Turn(Labyrinth[lY, lX]);
                                TurnRight();
                            }
                        }
                    }
                    Labyrinth[lY, lX] =  LastStep(Labyrinth[lY, lX]);
                    TurnBack();
                }
                listMap += Map(Labyrinth, count);
            }
            listMap = listMap.Replace("\n\n", "\n");
            return listMap;
        }

        public static string Map(Room[,] rooms, int count)
        {
            string text = "";
            for(int i = 0; i < count+1; i++)
            {
                for(int j = 0; j < count * 2 + 1; j++)
                {
                    if (rooms[i, j].North == true && rooms[i, j].South == false && rooms[i, j].West == false && rooms[i, j].East == false) text += "1";
                    else if (rooms[i, j].North == false && rooms[i, j].South == true && rooms[i, j].West == false && rooms[i, j].East == false) text += "2";
                    else if (rooms[i, j].North == true && rooms[i, j].South == true && rooms[i, j].West == false && rooms[i, j].East == false) text += "3";
                    else if (rooms[i, j].North == false && rooms[i, j].South == false && rooms[i, j].West == true && rooms[i, j].East == false) text += "4";
                    else if (rooms[i, j].North == true && rooms[i, j].South == false && rooms[i, j].West == true && rooms[i, j].East == false) text += "5";
                    else if (rooms[i, j].North == false && rooms[i, j].South == true && rooms[i, j].West == true && rooms[i, j].East == false) text += "6";
                    else if (rooms[i, j].North == true && rooms[i, j].South == true && rooms[i, j].West == true && rooms[i, j].East == false) text += "7";
                    else if (rooms[i, j].North == false && rooms[i, j].South == false && rooms[i, j].West == false && rooms[i, j].East == true) text += "8";
                    else if (rooms[i, j].North == true && rooms[i, j].South == false && rooms[i, j].West == false && rooms[i, j].East == true) text += "9";
                    else if (rooms[i, j].North == false && rooms[i, j].South == true && rooms[i, j].West == false && rooms[i, j].East == true) text += "a";
                    else if (rooms[i, j].North == true && rooms[i, j].South == true && rooms[i, j].West == false && rooms[i, j].East == true) text += "b";
                    else if (rooms[i, j].North == false && rooms[i, j].South == false && rooms[i, j].West == true && rooms[i, j].East == true) text += "c";
                    else if (rooms[i, j].North == true && rooms[i, j].South == false && rooms[i, j].West == true && rooms[i, j].East == true) text += "d";
                    else if (rooms[i, j].North == false && rooms[i, j].South == true && rooms[i, j].West == true && rooms[i, j].East == true) text += "e";
                    else if (rooms[i, j].North == true && rooms[i, j].South == true && rooms[i, j].West == true && rooms[i, j].East == true) text += "f";
                }
                text += "\n";
            }
            text = text.Replace("\n\n", "");
            text += "\n";
            return text;
        }

        public static Room LastStep(Room room)
        {
            room.North = room.North || fromNorth || North;
            room.South = room.South || fromSouth || South;
            room.West = room.West || fromWest || West;
            room.East = room.East || fromEast || East;
            return room;
        }

        public static void Write(string text, string filePath)
        {
            FileStream fileStream = null;
            if (!File.Exists(filePath))
                fileStream = File.Create(filePath);
            else
                fileStream = File.Open(filePath, FileMode.Create);
            StreamWriter output = new StreamWriter(fileStream);
            output.Write(text);
            output.Close();
        }

        static void Main(string[] args)
        {
            string[] lines1 = File.ReadAllLines("small-test.in.txt");
            string reslines1 = Read(lines1);
            Write(reslines1, @"small-test.out.txt");

            string[] lines2 = File.ReadAllLines("large-test.in.txt");
            string reslines2 = Read(lines2);
            Write(reslines2, @"large-test.out.txt");
        }
    }
}
