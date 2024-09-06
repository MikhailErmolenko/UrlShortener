using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Server.Data;
using UrlShortener.Server.Services;

namespace UrlShortener.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly UrlShortenerService _shortenerService;

        public ShortUrlController(ApplicationDbContext context)
        {
            _shortenerService = new UrlShortenerService(context);
        }

        [HttpGet]
        public async Task<string> GetShortUrl(string url)
        {
            return await _shortenerService.GenerateCode();
        }
    }
}
