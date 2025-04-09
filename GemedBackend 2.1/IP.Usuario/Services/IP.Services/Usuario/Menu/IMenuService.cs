using IP.Application.Comands.Responses.Menu;

namespace IP.Services.Usuario.Menu
{
    public interface IMenuService
    {
        Task<MenuResponse> GetMenuAsync();
    }
}
