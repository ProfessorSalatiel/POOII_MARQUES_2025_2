using Controller;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

public class DetailsModel : PageModel
{
    private readonly ChamadoController _controller;
    public ChamadoModel Chamado { get; set; }

    public DetailsModel(ChamadoController controller)
    {
        _controller = controller;
    }

    public async Task OnGetAsync(long id)
    {
        Chamado = await _controller.BuscarPorIdAsync(id);
    }
}