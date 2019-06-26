using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace des
{
    class DES
    {
        protected List<string> TextTo64BitBlocks(string enter_text)
        {
            List<string> output = new List<string>();

            string text = "";
            for (int i = 0; i < enter_text.Length; i++)
            {
                string ab = Convert.ToString(enter_text[i], 2);
                while (ab.Length < 16)
                    ab = '0' + ab;
                
                text += ab;
            }
            while (text.Length % 64 != 0)
                text += "0";

            
            for (int i = 0; i < text.Length; i = i + 64)
                output.Add(text.Substring(i, 64));

            return output;
        }

        protected string SymbolKeyTo56BitKey(string enter_key)
        {
            string key = "";
            for (int i = 0; i < enter_key.Length; i++)
            {
                string ab = Convert.ToString(enter_key[i], 2);
                while (ab.Length < 16)
                    ab = '0' + ab;
                key += ab;

            }
            while (key.Length != 56)
            {
                if (key.Length > 56)
                    key = key.Remove(key.Length - 1, 1);
                else if (key.Length < 56)
                    key += "0";
            }

            return key;
        }

        protected int BinToDec(string bin)
        {
            int output = 0;
            int count = 0;
            for (int i = bin.Length - 1; i >= 0; i--)
            {
                if (bin[i] == '1')
                    output += (int)Math.Pow(2.0, (double)count);
                count++;
            }

            return output;
        }





        protected string XOR(string first, string second)
        {
            string output = "";
            for (int i = 0; i < first.Length; i++)
            {
                if ((first[i] == '1' && second[i] == '1') || (first[i] == '0' && second[i] == '0'))
                    output += "0";
                else
                    output += "1";
            }
            return output;
        }

        protected string LeftShift(string text, int count)
        {
            for (int i = 0; i < count; i++)
            {
                text += text[0];
                text = text.Remove(0, 1);
            }
            return text;
        }

        protected string IP(string text)
        {
            string output = "";
            int[] Table = new int[]
            {
            58,  50,  42,  34,  26,  18,  10,  2,   60,  52,  44,  36,  28,  20,  12,  4,
            62,  54,  46,  38,  30,  22,  14,  6,   64,  56,  48,  40,  32,  24,  16,  8,
            57,  49,  41,  33,  25,  17,  9,   1,   59,  51,  43,  35,  27,  19,  11,  3,
            61,  53,  45,  37,  29,  21,  13,  5,   63,  55,  47,  39,  31,  23,  15,  7
            };

            for (int i = 0; i < Table.Length; i++)
                output += text[Table[i] - 1];

            return output;
        }

        protected string IP_1(string text)
        {
            string output = "";
            int[] Table = new int[]
            {
            40,  8,   48,  16,  56,  24,  64,  32,  39,  7,   47,  15,  55,  23,  63,  31,
            38,  6,   46,  14,  54,  22,  62,  30,  37,  5,   45,  13,  53,  21,  61,  29,
            36,  4,   44,  12,  52,  20,  60,  28,  35,  3,   43,  11,  51,  19,  59,  27,
            34,  2,   42,  10,  50,  18,  58,  26,  33,  1,   41,  9,   49,  17,  57,  25
            };

            for (int i = 0; i < Table.Length; i++)
                output += text[Table[i] - 1];

            return output;
        }

        protected List<string> KeysGeneration(string key)
        {
            List<string> Keys = new List<string>();
            int[] C_Table = new int[]
            {
                57,49,  41,  33,  25,  17,  9,   1,   58,  50,  42,  34,  26,  18,
                10,  2, 59,  51,  43,  35,  27,  19,  11,  3,   60,  52,  44,  36
            };
            int[] D_Table = new int[]
            {
                63,  55,  47,  39,  31,  23,  15,  7,   62,  54,  46,  38,  30,  22,
                14,  6,   61,  53,  45,  37,  29,  21,  13,  5,   28,  20,  12,  4
            };

            int[] shifts = new int[]
            {
                1, 1,   2,   2,   2,   2,   2,   2,   1,   2,   2,   2,   2,   2,   2,   1
            };

            int[] last_table = new int[]
            {
                4,  17,  11,  24,  1,   5,   3,   28,  15,  6,   21,  10,  23,  19,  12,  4,
                26,  8,   16,  7,   27,  20,  13,  2,   41,  52,  31,  37,  47,  55,  30,  40,
                51,  45,  33,  48,  44,  49,  39,  56,  34,  53,  46,  42,  50,  36,  29,  32
            };

            int count = 0;
            int c = 0;
            for (int i = 0; i < key.Length; i++)
            {
                c++;
                if (key[i] == '1')
                    count++;

                if (c == 7)
                {
                    if (count % 2 == 0)
                        key = key.Insert(i + 1, "1");
                    else
                        key = key.Insert(i + 1, "0");

                    count = 0;
                    c = 0;
                    i++;
                }

            }

            string C = "", D = "";

            for (int i = 0; i < D_Table.Length; i++)
            {
                C += key[C_Table[i] - 1];
                D += key[D_Table[i] - 1];
            }
            key = "";
            key = C + D;

            //key = LeftShift(key, shifts[0]);
            //Keys.Add(key);

            for (int i = 0; i < shifts.Length; i++)
            {
                key = LeftShift(key, shifts[0]);
                string help = "";
                for (int j = 0; j < last_table.Length; j++)
                    help += key[last_table[j] - 1];
                Keys.Add(help);
            }
            return Keys;
        }


        protected string f_function(string text, string key)
        {
            string result = "";
            string output = "";

            int[] E_table = new int[]
            {
                32,  1,   2,   3,   4,   5,
                4,   5,   6,   7,   8,   9,
                8,   9,   10,  11,  12,  13,
                12,  13,  14,  15,  16,  17,
                16,  17,  18,  19,  20,  21,
                20,  21,  22,  23,  24,  25,
                24,  25,  26,  27,  28,  29,
                28,  29,  30,  31,  32,  1
            };

            int[,] S1_table = new int[,]
            {
                { 4,   4,   13,  1,   2,   15,  11,  8,   3,   10,  6,   12,  5,   9,   0,   7 },
                { 0,   15,  7,   4,   14,  2,   13,  1,   10,  6,   12,  11,  9,   5,   3,   8 },
                { 4,   1,   14,  8,   13,  6,   2,   11,  15,  12,  9,   7,   3,   10,  5,   0},
                { 15,  12,  8,   2,   4,   9,   1,   7,   5,   11,  3,   14,  10,  0,   6,   13 }
            };

            int[,] S2_table = new int[,]
            {
                { 15,    1,   8,   14,  6,   11,  3,   4,   9,   7,   2,   13,  12,  0,   5,   10 },
                { 3,     13,  4,   7,   15,  2,   8,   14,  12,  0,   1,   10,  6,   9,   11,  5},
                { 0,     14,  7,   11,  10,  4,   13,  1,   5,   8,   12,  6,   9,   3,   2,   15},
                { 13,    8,   10,  1,   3,   15,  4,   2,   11,  6,   7,   12,  0,   5,   14,  9}
            };

            int[,] S3_table = new int[,]
            {
                { 10,    0,   9,   14,  6,   3,   15,  5,   1,   13,  12,  7,   11,  4,   2,   8 },
                { 13,    7,   0,   9,   3,   4,   6,   10,  2,   8,   5,   14,  12,  11,  15,  1 },
                { 13,    6,   4,   9,   8,   15,  3,   0,   11,  1,   2,   12,  5,   10,  14,  7},
                { 1,     10,  13,  0,   6,   9,   8,   7,   4,   15,  14,  3,   11,  5,   2,   12}
            };

            int[,] S4_table = new int[,]
            {
                { 7, 13,  14,  3,   0,   6,   9,   10,  1,   2,   8,   5,   11,  12,  4,   15 },
                { 13,    8,   11,  5,   6,   15,  0,   3,   4,   7,   2,   12,  1,   10,  14,  9},
                { 10,    6,   9,   0,   12,  11,  7,   13,  15,  1,   3,   14,  5,   2,   8,   4},
                { 3, 15,  0,   6,   10,  1,   13,  8,   9,   4,   5,   11,  12,  7,   2,   14 }
            };

            int[,] S5_table = new int[,]
            {
                { 2, 12,  4,   1,   7,   10,  11,  6,   8,   5,   3,   15,  13,  0,   14,  9 },
                { 14,    11,  2,   12,  4,   7,   13,  1,   5,   0,   15,  10,  3,   9,   8,   6},
                { 4, 2,   1,   11,  10,  13,  7,  8,   15,  9,   12,  5,   6,   3,   0,   14  },
                { 11,    8,   12,  7,   1,   14,  2,   13,  6,   15,  0,   9,   10,  4,   5,   3}
            };

            int[,] S6_table = new int[,]
            {
                { 12,    1,   10,  15,  9,   2,   6,   8,   0,   13,  3,   4,   14,  7,   5,   11  },
                { 10,    15,  4,   2,   7,   12,  9,   5,   6,   1,   13,  14,  0,   11,  3,   8},
                { 9, 14,  15,  5,   2,   8,   12,  3,   7,   0,   4,   10,  1,   13,  11,  6},
                { 4, 3,   2,   12,  9,   5,   15,  10,  11,  14,  1,   7,   6,   0,   8,   13}
            };

            int[,] S7_table = new int[,]
            {
                { 4, 11, 2,   14,  15,  0,   8,   13,  3,   12,  9,   7,   5,   10,  6,   1},
                { 13,    0,   11,  7,   4,   9,   1,   10,  14,  3,   5,   12,  2,   15,  8,   6},
                { 1, 4,   11,  13,  12,  3,   7,   14,  10,  15,  6,   8,   0,   5,   9,   2},
                { 6, 11,  13,  8,   1,   4,   10,  7,   9,   5,   0,   15,  14,  2,   3,   12}
            };

            int[,] S8_table = new int[,]
            {
                { 13,    2,   8,   4,   6,   15,  11,  1,   10,  9,   3,   14,  5,   0,   12,  7},
                { 1, 15,  13,  8,   10,  3,   7,   4,   12,  5,   6,   11,  0,   14,  9,   2},
                { 7, 11,  4,   1,   9,   12,  14,  2,   0,   6,   10,  13,  15,  3,   5,   8},
                { 2, 1,   14,  7,   4,   10,  8,  13,  15,  12,  9,   0,   3,   5,   6,   11}
            };

            int[] P_table = new int[]
            {
                16,  7,   20,  21,  29,  12,  28,  17,
                1,   15,  23,  26,  5,   18,  31,  10,
                2,   8,   24,  14,  32,  27,  3,   9,
                19,  13,  30,  6,   22,  11,  4,   25
            };

            string extended_text = "";
            for (int i = 0; i < E_table.Length; i++)
                extended_text += text[E_table[i] - 1];

            text = XOR(extended_text, key);
            List<string> s_blocks = new List<string>();

            for (int i = 0; i < text.Length; i = i + 6)
                s_blocks.Add(text.Substring(i, 6));

            for (int i = 0; i < s_blocks.Count; i++)
            {
                int line = BinToDec(s_blocks[i].Substring(0, 2));
                int column = BinToDec(s_blocks[i].Substring(2, 4));
                switch (i)
                {
                    case 0:
                        s_blocks[i] = Convert.ToString(S1_table[line, column], 2);
                        break;
                    case 1:
                        s_blocks[i] = Convert.ToString(S2_table[line, column], 2);
                        break;
                    case 2:
                        s_blocks[i] = Convert.ToString(S3_table[line, column], 2);
                        break;
                    case 3:
                        s_blocks[i] = Convert.ToString(S4_table[line, column], 2);
                        break;
                    case 4:
                        s_blocks[i] = Convert.ToString(S5_table[line, column], 2);
                        break;
                    case 5:
                        s_blocks[i] = Convert.ToString(S6_table[line, column], 2);
                        break;
                    case 6:
                        s_blocks[i] = Convert.ToString(S7_table[line, column], 2);
                        break;
                    case 7:
                        s_blocks[i] = Convert.ToString(S8_table[line, column], 2);
                        break;
                }
                while (s_blocks[i].Length != 4)
                    s_blocks[i] = s_blocks[i].Insert(0, "0");

                result += s_blocks[i];
            }

            for (int i = 0; i < P_table.Length; i++)
                output += result[P_table[i] - 1];

            return output;
        }
    }
}
