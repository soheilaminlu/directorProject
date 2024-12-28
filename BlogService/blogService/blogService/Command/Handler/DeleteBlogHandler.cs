using blogService.Command.Model;
using blogService.Repository;
using MediatR;

namespace blogService.Command.Handler
{
    public class DeleteBlogHandler : IRequestHandler<DeleteBlogCommand, bool>
    {
        private readonly IBlogRepository _blogRepository;

        public DeleteBlogHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<bool> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
         return await _blogRepository.DeleteAsync(request.id);            
        }
    }
}
