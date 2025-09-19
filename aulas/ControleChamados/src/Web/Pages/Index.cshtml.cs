using Controller;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

public class IndexModel : PageModel
{
    private readonly ChamadoController _controller;
    public List<Chamado> Chamados { get; set; } = new();

    public IndexModel(ChamadoController controller)
    {
        _controller = controller;
    }

    public async Task OnGetAsync()
    {
        Chamados = await _controller.BuscarTodosAsync();
    }
}