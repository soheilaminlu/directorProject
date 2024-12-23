using NewsProject.Dto;
using NewsProject.Model;
using NewsProject.Repository;

namespace NewsProject.Service
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;

        public NewsService(INewsRepository repository)
        {
            _repository = repository;
        }
        public Task<NewsModel> AddNewsService(AddNewsDto news)
        {
            var newsModel = _repository.AddNews(news);
            return newsModel;
        }

        public async Task DeleteNewsService(int id)
        {
            await _repository.DeleteNews(id);
        }

        public async Task<IEnumerable<NewsDto>> GetAllNewsService()
        {
           var news = await _repository.GetAllNews();
            return news;
        }

        public async Task<NewsModel> UpdateNewsService(int id , EditNewsDto editNewsDto)
        {
            var newsModel = await _repository.UpdateNews(id , editNewsDto);
            return newsModel;
        }
    }
}
