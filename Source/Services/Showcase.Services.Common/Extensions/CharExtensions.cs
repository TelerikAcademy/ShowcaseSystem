namespace Showcase.Services.Common.Extensions
{
    public static class CharExtensions
    {
        public static bool IsEnglishLetter(this char symbol)
        {
            return !((symbol < 65 || 90 < symbol) && (symbol < 97 || 122 < symbol));
        }
    }
}
