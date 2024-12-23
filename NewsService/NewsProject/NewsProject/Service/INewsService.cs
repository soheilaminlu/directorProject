using NewsProject.Dto;
using NewsProject.Model;

namespace NewsProject.Service
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> GetAllNewsService();
        Task<NewsModel> AddNewsService(AddNewsDto news);

        Task<NewsModel> UpdateNewsService(int id , EditNewsDto editNewsDto);

        Task DeleteNewsService(int id);
    }
}
