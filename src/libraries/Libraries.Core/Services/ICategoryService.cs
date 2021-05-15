namespace DevQuiz.Libraries.Core.Services
{
    public interface ICategoryService
        : IBaseService<TUserDto, TUserDto, IList<TUserDto>, TKey, bool, bool, TKey>
        where TUserDto : UserDtoBase<TKey>
        where TKey : IEquatable<TKey>
    {
        
    }
}