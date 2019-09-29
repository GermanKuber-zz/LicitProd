namespace LicitProd.Infrastructure
{
    public static class VerificableService
    {
        public static string GetCheckDigit(string number)
        {
            int sum = 0;
            for (int i = number.Length - 1, multiplier = 2; i >= 0; i--)
            {
                sum += (int)char.GetNumericValue(number[i]) * multiplier;
                if (++multiplier > 7) multiplier = 2;
            }
            int mod = (sum % 11);
            if (mod == 0 || mod == 1) return "0";
            return (11 - mod).ToString();
        }
    }
}