//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//public class DeleteModel : PageModel
//{
//    private readonly ChamadoController _controller;
//    [BindProperty]
//    public Chamado Chamado { get; set; } = new();

//    public DeleteModel(ChamadoController controller)
//    {
//        _controller = controller;
//    }

//    public async Task OnGetAsync(long id)
//    {
//        var c = await _controller.GetByIdAsync(id);
//        if (c != null) Chamado = c;
//    }

//    public async Task<IActionResult> OnPostAsync()
//    {
//        await _controller.DeleteAsync(Chamado.IdChamado);
//        return RedirectToPage("/Index");
//    }
//}
