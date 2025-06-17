using System.Text;

public class OldPhonePad
{
    
    private static readonly Dictionary<char, string> Keypad = new(){
        { '1', "" },
        { '2', "ABC" },
        { '3', "DEF" },
        { '4', "GHI" },
        { '5', "JKL" },
        { '6', "MNO" },
        { '7', "PQRS" },
        { '8', "TUV" },
        { '9', "WXYZ" },
        { '0', " " }
    };

    public static string ConvertInput(string input){
        if (string.IsNullOrEmpty(input) || !input.EndsWith("#")) return "";

        var output = new StringBuilder();
        var buffer = new StringBuilder();
        
        char lastChar = '\0';

        foreach (char c in input)
        {
            if (c == '#')
            {
                ProcessBuffer(buffer.ToString(), output);
                break;
            }
            else if (c == ' ')
            {
                ProcessBuffer(buffer.ToString(), output);
                buffer.Clear();
                lastChar = '\0';
            }
            else if (c == '*')
            {
                if (output.Length > 0)
                    output.Remove(output.Length - 1, 1);
            }
            else
            {
                if (buffer.Length > 0 && c != lastChar)
                {
                    ProcessBuffer(buffer.ToString(), output);
                    buffer.Clear();
                }
                buffer.Append(c);
                lastChar = c;
            }
        }
        
        return output.ToString();
    }

    private static void ProcessBuffer(string sequence, StringBuilder output){

        if (string.IsNullOrEmpty(sequence))return;

        char key = sequence[0];

        if (!Keypad.ContainsKey(key) || Keypad[key].Length == 0) return;

        int index = (sequence.Length - 1) % Keypad[key].Length;

        output.Append(Keypad[key][index]);
    }

}
