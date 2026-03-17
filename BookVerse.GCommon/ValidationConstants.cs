namespace BookVerse.GCommon
{
    public class ValidationConstants
    {
        public static class Book
        {
            public const int IsbnMin = 10;
            public const int IsbnMax = 13;
            public const int TitleMin = 3;
            public const int TitleMax = 80;
            public const int DescriptionMin = 10;
            public const int DescriptionMax = 250;
            public const int CoverImageUrlMax = 2048;
        }

        public static class Genre
        {
            public const int NameMin = 3;
            public const int NameMax = 20;
        }
    }
}
