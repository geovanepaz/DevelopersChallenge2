using AutoMapper;
namespace Application.AutoMapper
{
    public static class MapperExtension
    {
        public static T MapTo<T>(this object src, IMapper mapper) => (T)mapper.Map(src, src.GetType(), typeof(T));
    }
}
