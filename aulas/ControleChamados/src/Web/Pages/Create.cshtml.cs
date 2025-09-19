using Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

public class CreateModel : PageModel
{
    private readonly ChamadoController _controller;

    [BindProperty]
    public ChamadoModel chamado { get; set; } = new();

    public bool MostrarSucesso { get; private set; }

    public CreateModel(ChamadoController controller) => _controller = controller;

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var ok = await _controller.IncluirAsync(chamado.Descricao);

        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível incluir o chamado.");
            return Page();
        }

        MostrarSucesso = true;
        ModelState.Clear();
        chamado = new ChamadoModel();

        return Page();
    }
}
