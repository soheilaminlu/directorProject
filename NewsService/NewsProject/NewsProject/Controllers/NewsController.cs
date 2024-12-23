using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsProject.Dto;
using NewsProject.Service;
using System.Net.WebSockets;

namespace NewsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpGet("allNews")]
        public async Task<IActionResult> GetAllNews()
        {
            var news = await _newsService.GetAllNewsService();
            if (news == null)
            {
                return NotFound();
            }
            return Ok(news);
        }
        [HttpPost("addNews")]
        public async Task<IActionResult> AddNews([FromBody] AddNewsDto addNewsDto)
        {
            var addnews = await _newsService.AddNewsService(addNewsDto);
            if (addnews == null)
            {
                return BadRequest();
            }
            return Ok(addnews);
        }

        [HttpPatch("updateNews")]
        public async Task<IActionResult> UpdateNews(int id, EditNewsDto editNewsDto)
        {
            var updateNews = await _newsService.UpdateNewsService(id, editNewsDto);
            if (updateNews == null)
            {
                return BadRequest();
            }
            return Ok(updateNews);
        }
        [HttpDelete("deleteNews")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            await _newsService.DeleteNewsService(id);
            return Ok();
        }

    }
}
