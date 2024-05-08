using Microsoft.AspNetCore.Mvc;
using RitaGlamStudio.Domain.Entities;
using RitaGlamStudio.Infrastructure.Data;

namespace RitaGlamStudio.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)               //Construtor
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var categories = _db.Categories.ToList();

            return View(categories);
        }

        // -- CATEGORY -- 

        //1 - Create

        //GET - Este é um clique normal que "mostra" o formulário
        public IActionResult Create()
        {
            return View();
        }

        //POST - Aqui é para gravarmos os dados do formulário
        [HttpPost]  //este método vai receber pelo método post os dados do formulário
        [ValidateAntiForgeryToken]  //para segurança
        public IActionResult Create(Category obj)  
        {
            if (ModelState.IsValid)             //validações do lado do servidor
            {
                _db.Categories.Add(obj);    //para injectar na bd
                _db.SaveChanges();          //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "Category has been created sucessfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "Category could not be created!";
            return View(obj);
        }

        // 2 - Update
        //GET - este é para, depois de selecionar qual é aquele que quero editar, recebe o id 
        public IActionResult Update(int categoryId)
        {
            //Aqui estou a aceder à bd, a comparar com o id que foi selecionado pelo clique do get
            Category? obj = _db.Categories.FirstOrDefault(x => x.Id == categoryId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");   //1º o ficheiro, depois a vista. É o error, da vista Home. Temos de indicar o caminho.
            }
            return View(obj);   //se não for nulo, devolvo TODOS os dados da categoria que eu quero abrir e editar
        }

        //POST - para gravar os dados atualizados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category obj)  
        {
            if (ModelState.IsValid && obj.Id > 0)             //validações do lado do servidor
                                                              //segurança adicional - se o Id for zero, nunca dá update. Zero é para criar.
            {
                _db.Categories.Update(obj);         //para injectar na bd c/o update
                _db.SaveChanges();                  //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "Category has been updated sucessfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Category could not be updated!";
            return View(obj);
        }

        // 3 - Delete
        //GET - este é para, depois de selecionar qual é aquele que quero eliminar, recebe o id 
        public IActionResult Delete(int categoryId)
        {
            //Aqui estou a aceder à bd, a comparar com o id que foi selecionado pelo clique do get
            Category? obj = _db.Categories.FirstOrDefault(x => x.Id == categoryId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home"); 
            }

            return View(obj);   
        }

        //POST - para gravar os dados atualizados depois de ele apagar
        [HttpPost]
        public IActionResult Delete(Category obj)  
        {
            Category? objFromDb = _db.Categories.FirstOrDefault(_ => _.Id == obj.Id);

            if (objFromDb is not null)
            {
                _db.Categories.Remove(objFromDb);
                _db.SaveChanges();              //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "Category has been deleted sucessfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "Category could not be deleted!";
            return View(obj);
        }
    }
}
