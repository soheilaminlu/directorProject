using NewsProject.Dto;
using NewsProject.Model;

namespace NewsProject.Repository
{
    public interface INewsRepository
    {
        Task<IEnumerable<NewsDto>> GetAllNews();
        Task<NewsModel> AddNews(AddNewsDto news);

        Task<NewsModel> UpdateNews(int id, EditNewsDto editNewsDto);

        Task DeleteNews(int id);
    }
}
