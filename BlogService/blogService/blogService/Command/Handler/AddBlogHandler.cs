using blogService.Command.Model;
using blogService.Dto;
using blogService.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blogService.Command.Handler
{
    public class AddBlogCommandHandler : IRequestHandler<AddBlogCommand, int>
    {
        private readonly IBlogRepository _blogRepository;

        public AddBlogCommandHandler(IBlogRepository blogRepository)
        {
             _blogRepository = blogRepository;
        }
        public async Task<int> Handle(AddBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = new AddBlogsDto
            {
                Title = request.addBlogsDto.Title,
                Description = request.addBlogsDto.Description,
            };
            var blogId = await _blogRepository.AddAsync(blog);
            return blogId;

        }
    }
}
