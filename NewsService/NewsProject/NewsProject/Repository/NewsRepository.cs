using Microsoft.EntityFrameworkCore;
using NewsProject.Data;
using NewsProject.Dto;
using NewsProject.Mapper;
using NewsProject.Model;

namespace NewsProject.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsMapper _mapper;
        private readonly ApplicationDbContext _applicationDb;

        public NewsRepository(NewsMapper mapper, ApplicationDbContext applicationDb)
        {
            _mapper = mapper;   
            _applicationDb = applicationDb;
        }
        public async Task<NewsModel> AddNews(AddNewsDto news)
        {
            var newsDto = _mapper.MapAddNewsToDto(news);
            var newsModel = _mapper.MapNewsDtoToModel(newsDto);
            await _applicationDb.News.AddAsync(newsModel);
            await _applicationDb.SaveChangesAsync();
            return newsModel;
        }

        public async Task DeleteNews(int id)
        {
            var news = await _applicationDb.News.FindAsync(id);
            if (news != null)
            {
                _applicationDb.News.Remove(news);
            }
            await _applicationDb.SaveChangesAsync();
        }

        public async Task<IEnumerable<NewsDto>> GetAllNews()
        {
            var news = await _applicationDb.News.Select(x => new NewsDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreatedDate = x.CreateDate,
            }
            ).ToListAsync();
            return news;
        }

        public async Task<NewsModel> UpdateNews(int id , EditNewsDto editNewsDto)
        {
            var news = await _applicationDb.News.FirstOrDefaultAsync(x => x.Id == id);
            var updateNews = new NewsDto
            {
                Title = editNewsDto.Title, 
                Description = editNewsDto.Description
            };
            var newsModel = _mapper.MapNewsDtoToModel(updateNews);
            await _applicationDb.SaveChangesAsync();
            return newsModel;
        }
    }
}
