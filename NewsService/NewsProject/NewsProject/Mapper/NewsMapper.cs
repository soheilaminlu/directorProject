using NewsProject.Dto;
using NewsProject.Model;

namespace NewsProject.Mapper
{
    public class NewsMapper
    {
        public NewsModel MapNewsDtoToModel(NewsDto newsDto)
        {
            return new NewsModel
            {
                Title = newsDto.Title,
                Description = newsDto.Description,
                CreateDate = newsDto.CreatedDate,
            };

        }
        public NewsDto MapAddNewsToDto(AddNewsDto addNewsDto)
        {
            return new NewsDto
            {
                Title = addNewsDto.Title,
                Description = addNewsDto.Description,
            };
        }

        public NewsDto MapUpdateNewsToDto(EditNewsDto editNewsDto)
        {
            return new NewsDto
            {
                Title = editNewsDto.Title,
                Description = editNewsDto.Description,
            };
        }


    }
}
