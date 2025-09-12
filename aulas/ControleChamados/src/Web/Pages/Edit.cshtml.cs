//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//public class EditModel : PageModel
//{
//    private readonly ChamadoController _controller;
//    [BindProperty]
//    public Chamado Chamado { get; set; } = new();

//    public EditModel(ChamadoController controller)
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
//        if (!ModelState.IsValid) return Page();
//        await _controller.UpdateAsync(Chamado);
//        return RedirectToPage("/Index");
//    }
//}