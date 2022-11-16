using System.Text;

namespace P
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter text: ");
            var text = Console.ReadLine();
            var compressedText = Compress(text);
            Console.WriteLine(string.Join(", ", compressedText));

            var decompressedText = Decompress(compressedText);
            Console.WriteLine(decompressedText);
        }

        public static List<int> Compress(string text)
        {
            Dictionary<string, int> sozdik = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++)
                sozdik.Add(((char)i).ToString(), i);

            string w = string.Empty;
            List<int> compressed = new List<int>();

            foreach (char c in text)
            {
                string wc = w + c;
                if (sozdik.ContainsKey(wc))
                    w = wc;
                else
                {
                    compressed.Add(sozdik[w]);
                    sozdik.Add(wc, sozdik.Count);
                    w = c.ToString();
                }
            }

            if (!string.IsNullOrEmpty(w))
                compressed.Add(sozdik[w]);

            return compressed;
        }


        public static string Decompress(List<int> compressed)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)  
                dictionary.Add(i, ((char)i).ToString());
            string w = dictionary[compressed[0]];
            compressed.RemoveAt(0);
            StringBuilder decompressed = new StringBuilder(w);
            foreach (int k in compressed)
            {
                string entry = null;
                if (dictionary.ContainsKey(k))
                    entry = dictionary[k];
                else if (k == dictionary.Count)
                    entry = w + w[0];
                decompressed.Append(entry);
                dictionary.Add(dictionary.Count, w + entry[0]);
                w = entry;
            }
            return decompressed.ToString();
        }

    }
}