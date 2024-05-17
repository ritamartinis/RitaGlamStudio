using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RitaGlamStudio.Domain.Entities;
using RitaGlamStudio.Infrastructure.Data;
using RitaGlamStudio.Web.ViewModels;

namespace RitaGlamStudio.Web.Controllers
{
    public class MakeupProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MakeupProductController(ApplicationDbContext db)               //Construtor
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var makeupProducts = _db.MakeupProducts.ToList();

            return View(makeupProducts);
        }

        // -- MAKEUP PRODUCTS -- 

        //1 - Create

        //GET - Este é um clique normal que "mostra" o formulário
        public IActionResult Create()
        {
            //MakeupProductVM é um novo modelo - que só é usado nas vistas - que é superior aos outros
            MakeupProductVM makeupProductVM = new()
            {
                //Estamos a criar um dropdown (um select) mas mostra nos produtos os id das marcas
                BrandItems = _db.Brands.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),

                }),

                //Mesma coisa para a categoria - vai buscar os ids das categorias
                CategoryItems = _db.Categories.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
            };

            return View(makeupProductVM);
        }

        //POST - Aqui é para gravarmos os dados do formulário
        [HttpPost]  //este método vai receber pelo método post os dados do formulário
        [ValidateAntiForgeryToken]  //para segurança
        public IActionResult Create(MakeupProduct obj)
        {
            if (obj.Name == obj.Description)
                ModelState.AddModelError("Name", "The Description cannot be a exact match to be Name");

            if (ModelState.IsValid)             //validações do lado do servidor
            {
                _db.MakeupProducts.Add(obj);        //para injectar na bd
                _db.SaveChanges();                  //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "The Product has been created sucessfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "The Product could not be created!";
            return View(obj);
        }

        // 2 - Update
        //GET - este é para, depois de selecionar qual é aquele que quero editar, recebe o id 
        public IActionResult Update(int makeupProductId)
        {
            //Aqui estou a aceder à bd, a comparar com o id que foi selecionado pelo clique do get
            Brand? obj = _db.Brands.FirstOrDefault(x => x.Id == makeupProductId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");   //1º o ficheiro, depois a vista. É o error, da vista Home. Temos de indicar o caminho.
            }
            return View(obj);   //se não for nulo, devolvo TODOS os dados da categoria que eu quero abrir e editar
        }

        //POST - para gravar os dados atualizados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Brand obj)
        {
            if (ModelState.IsValid && obj.Id > 0)             //validações do lado do servidor
                                                              //segurança adicional - se o Id for zero, nunca dá update. Zero é para criar.
            {
                _db.Brands.Update(obj);             //para injectar na bd c/o update
                _db.SaveChanges();                  //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "Brand has been updated sucessfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Brand could not be updated!";
            return View(obj);
        }

        // 3 - Delete
        //GET - este é para, depois de selecionar qual é aquele que quero eliminar, recebe o id 
        public IActionResult Delete(int brandId)
        {
            //Aqui estou a aceder à bd, a comparar com o id que foi selecionado pelo clique do get
            Brand? obj = _db.Brands.FirstOrDefault(x => x.Id == brandId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }

        //POST - para gravar os dados atualizados depois de ele apagar
        [HttpPost]
        public IActionResult Delete(Brand obj)
        {
            Brand? objFromDb = _db.Brands.FirstOrDefault(_ => _.Id == obj.Id);

            if (objFromDb is not null)
            {
                _db.Brands.Remove(objFromDb);
                _db.SaveChanges();              //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "Brand has been deleted sucessfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "Brand could not be deleted!";
            return View(obj);
        }
    }
}
