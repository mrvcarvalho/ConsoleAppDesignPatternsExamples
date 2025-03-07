using System;
using System.Linq;
using System.Text;


namespace ConsoleAppDesignPatternsExamples
{
    // Commento: Questa classe di estensione aggiunge il metodo Repeat a tutti gli oggetti string.

    public static class StringExtensions
    {
        public static string Repeat(this string str, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Il conteggio deve essere non negativo.");

            if (count == 0 || string.IsNullOrEmpty(str))
                return string.Empty;

            if (count == 1 || str.Length == 1)
                return string.Concat(Enumerable.Repeat(str, count));

            // Per stringhe più lunghe e conteggi maggiori, usiamo StringBuilder per efficienza
            StringBuilder result = new StringBuilder(str.Length * count);
            for (int i = 0; i < count; i++)
            {
                result.Append(str);
            }
            return result.ToString();
        }
    }
}




