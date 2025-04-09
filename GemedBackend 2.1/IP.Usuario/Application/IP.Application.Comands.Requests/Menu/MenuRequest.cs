using IP.Application.Comands.Responses.Menu;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Menu
{
    public class MenuRequest : BaseRequest, IRequest<MenuResponse>
    {
    }
}
