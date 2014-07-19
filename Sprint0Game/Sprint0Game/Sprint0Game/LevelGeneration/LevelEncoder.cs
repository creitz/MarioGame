using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sprint0Game
{
    public static class LevelEncoder
    {
        public static void Encode(String Name, Creatable[,] Level, Game game)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("Content/" + Name + ".txt"))
            {
                for (int i = 0; i < Level.GetLength(1); i++)
                {
                    String line = "";
                    for (int j = 0; j < Level.GetLength(0); j++)
                    {
                        if (Level[j,i] == null)
                            line += "   ";
                        else
                            line += Level[j,i].EncodingString;
                        line += ",";
                    }
                    if(i ==0)
                        line += "JXX, END";
                    writer.WriteLine(line);
                    
                }
                game.Levels.Add(Name);

            }
        }
    }
}
