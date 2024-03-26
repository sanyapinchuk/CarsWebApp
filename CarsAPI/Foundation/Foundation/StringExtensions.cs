namespace Foundation
{
    public static class StringExtensions
    {
        public static int? ParseIntWithNull(this string? value)
        {
            if(value == null)
                return null;

            if (int.TryParse(value, out var result))
            {
                return result;
            }

            return null;
        }

        public static double? ParseDoubleWithNull(this string? value)
        {
            if (value == null)
                return null;

            if (double.TryParse(value, out var result))
            {
                return result;
            }

            return null;
        }
    }
}
