using Microsoft.EntityFrameworkCore;
using UrlShortener.Server.Data;

namespace UrlShortener.Server.Services
{
    public class UrlShortenerService
    {
        private const int StortLinkLength = 7;
        private const string Symbols = "qwertyuiopasdfghjklzxcvbnm";

        private readonly Random _random = new();
        private readonly ApplicationDbContext _dbContext;

        public UrlShortenerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GenerateCode()
        {
            var codeChars = new char[StortLinkLength];

            while (true)
            {
                for (int i = 0; i < StortLinkLength; i++)
                {
                    var randomIndex = _random.Next(Symbols.Length - 1);

                    codeChars[i] = Symbols[randomIndex];
                }

                var code = new string(codeChars);

                if (!await _dbContext.ShortenedUrls.AnyAsync(s => s.Code == code))
                {
                    return code;
                }
            }
        }
    }
}
