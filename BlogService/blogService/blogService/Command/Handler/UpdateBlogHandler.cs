using blogService.Command.Model;
using blogService.Dto;
using blogService.Repository;
using MediatR;

namespace blogService.Command.Handler
{
    public class UpdateBlogHandler : IRequestHandler<UpdateBlogCommand, bool>
    {
        private readonly IBlogRepository _blogRepository;

        public UpdateBlogHandler(IBlogRepository blogRepository)
        {
             _blogRepository = blogRepository;
        }
        public async Task<bool> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var updatedBlog = new UpdateBlogs
            { 
                Title = request.updateDto.Title,
                Description = request.updateDto.Description,
            };
            return await _blogRepository.UpdateAsync(request.id , updatedBlog);
        }
    }
}
