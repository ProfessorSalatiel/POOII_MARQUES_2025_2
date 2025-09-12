//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//public class CreateModel : PageModel
//{
//    private readonly ChamadoController _controller;
//    [BindProperty]
//    public Chamado Chamado { get; set; } = new();

//    public CreateModel(ChamadoController controller)
//    {
//        _controller = controller;
//    }

//    public void OnGet() { }

//    public async Task<IActionResult> OnPostAsync()
//    {
//        if (!ModelState.IsValid) return Page();
//        var newId = await _controller.InsertAsync(Chamado);
//        return RedirectToPage("/Index");
//    }
//}
