using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Mapper.Mapper;

namespace IP.Mapper.Infrastructure.Usuario
{
    public class UsuarioMapper :
        Mapper<Domain.Usuario, UsuarioCreateRequest, UsuarioCreateResponse>,
        IUsuarioMapper
    {
        //public UsuarioMapper(MapperConfiguration configuration) : base(configuration)
        //{
        //    configuration = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<Domain, Request>();
        //        cfg.CreateMap<Request, Domain>();

        //        cfg.CreateMap<Domain, Response>();
        //        cfg.CreateMap<Response, Domain>();
        //    });
        //}
    }
}
