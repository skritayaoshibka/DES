using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace des
{
    class EncryptDecrypt:DES
    {
        private List<string> Blocks;
        private List<string> Keys;
        private string Key;
        private string IV = "0101010101010101010101011111100011111100101010101010101111111000";

        public EncryptDecrypt(string text, string key)
        {
            Blocks = TextTo64BitBlocks(text);
            Key = SymbolKeyTo56BitKey(key);
            Keys = KeysGeneration(Key);
        }

        public string encrypt()
        {
            string result = "";
            for (int i = 0; i < Blocks.Count; i++)
            {
                IV = IP(IV);
                
                string L = IV.Substring(0, 32);
                string R = IV.Substring(32, 32);

                for (int j = 0; j < 16; j++)
                {
                    string help = R;
                    R = XOR(L, f_function(R, Keys[j]));
                    L = help;
                }
                IV = L + R;
                IV = IP_1(IV);

                IV = XOR(IV, Blocks[i]);

                int start_index = 0;
                while (start_index < IV.Length)
                {
                    char c = (char)Convert.ToInt16(IV.Substring(start_index, 16), 2);
                    result += c;
                    start_index += 16;
                }
            }

            return result;
        }

        public string decrypt()
        {
            string result = "";
            for (int i = 0; i < Blocks.Count; i++)
            {
                IV = IP(IV);

                string L = IV.Substring(0, 32);
                string R = IV.Substring(32, 32);

                for (int j = 0; j < 16; j++)
                {
                    string help = R;
                    R = XOR(L, f_function(R, Keys[j]));
                    L = help;
                }
                IV = L + R;
                IV = IP_1(IV);
                IV = XOR(IV, Blocks[i]);

                int start_index = 0;
                while (start_index < IV.Length)
                {
                    char c = (char)Convert.ToInt16(IV.Substring(start_index, 16), 2);
                    result += c;
                    start_index += 16;
                }

                IV = Blocks[i];
            }

            return result;
        }
    }
}
