using blogService.Dto;
using MediatR;

namespace blogService.Command.Model
{
    public record AddBlogCommand(AddBlogsDto addBlogsDto) : IRequest<int>;
    public record UpdateBlogCommand(int id , UpdateBlogs updateDto) :IRequest<bool>;
    public record GetAllBlogsCommand() : IRequest<List<BlogsDto>>;
    public record DeleteBlogCommand(int id) : IRequest<bool>;
 }
