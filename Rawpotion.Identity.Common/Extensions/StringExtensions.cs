namespace Rawpotion.Identity.Common.Extensions
{
    public static class StringExtensions
    {
        public static string EnsureLeadingSlash(this string url)
        {
            if (url != null && !url.StartsWith("/"))
            {
                return "/" + url;
            }

            return url;
        }
    }
}