using blogService.Command.Model;
using blogService.Dto;
using blogService.Models;
using blogService.Repository;
using MediatR;

namespace blogService.Command.Handler
{
    public class GetAllBlogsHandler : IRequestHandler<GetAllBlogsCommand , List<BlogsDto>>
    {
        private readonly IBlogRepository _blogRepository;
        public GetAllBlogsHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<List<BlogsDto>> Handle(GetAllBlogsCommand request, CancellationToken cancellationToken)
        {
           var blogs = await _blogRepository.GetBlogsAsync(cancellationToken);
            return blogs;
        }
    }
}
