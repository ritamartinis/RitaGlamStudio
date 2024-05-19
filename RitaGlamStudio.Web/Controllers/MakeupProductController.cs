using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var makeupProducts = _db.MakeupProducts
                .Include(u=>u.Brand)
                .Include(x=>x.Category)
                .ToList();

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
        public IActionResult Create(MakeupProductVM obj)
        {
            bool nameExists = _db.MakeupProducts.Any(u => u.Name == obj.MakeupProduct.Name);

            if (obj.MakeupProduct.Name == obj.MakeupProduct.Description)
                ModelState.AddModelError("Name", "The Description cannot be a exact match to be Name");

            if (ModelState.IsValid && !nameExists)                 //validações do lado do servidor
            {
                _db.MakeupProducts.Add(obj.MakeupProduct);        //para injectar na bd
                _db.SaveChanges();                  //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "The Product has been created sucessfully!";
                return RedirectToAction(nameof(Index));
            }

            if (nameExists)
            {
                TempData["error"] = "The Product Name already exists.";
            }

            obj.BrandItems = _db.Brands.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),

            });

            obj.CategoryItems = _db.Categories.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            //TempData["error"] = "The Product could not be created!";
            return View(obj);
        }

        // 2 - Update
        //GET - este é para, depois de selecionar qual é aquele que quero editar, recebe o id 
        public IActionResult Update(int makeupProductId)
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
                //Se o id for == ao makeupProductId, ele vai pôr aqui os dados todos
                MakeupProduct = _db.MakeupProducts.FirstOrDefault(_=>_.Id == makeupProductId)!
            };

            if (makeupProductVM.MakeupProduct is null)
            {
                return RedirectToAction("Error", "Home");   //1º o ficheiro, depois a vista. É o error, da vista Home. Temos de indicar o caminho.
            }
            return View(makeupProductVM);   //se não for nulo, devolvo TODOS os dados da categoria que eu quero abrir e editar
        }

        //POST - para gravar os dados atualizados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(MakeupProductVM makeupProductVM)
        {

			if (makeupProductVM.MakeupProduct.Name == makeupProductVM.MakeupProduct.Description)
				ModelState.AddModelError("Name", "The Description cannot be a exact match to be Name");

			if (ModelState.IsValid)                                               //validações do lado do servidor
			{
				_db.MakeupProducts.Update(makeupProductVM.MakeupProduct);        //para injectar na bd
				_db.SaveChanges();                                              //vai ver que alterações foram preparadas e faz save na bd

				TempData["success"] = "The Product has been updated sucessfully!";
				return RedirectToAction(nameof(Index));
			}

			makeupProductVM.BrandItems = _db.Brands.ToList().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString(),

			});

			makeupProductVM.CategoryItems = _db.Categories.ToList().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});

			TempData["error"] = "The Product could not be updated!";
			return View(makeupProductVM);
		}

		// 3 - Delete
		//GET - este é para, depois de selecionar qual é aquele que quero eliminar, recebe o id 
		public IActionResult Delete(int makeupProductId)
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
				//Se o id for == ao makeupProductId, ele vai pôr aqui os dados todos
				MakeupProduct = _db.MakeupProducts.FirstOrDefault(_ => _.Id == makeupProductId)!
			};

			if (makeupProductVM.MakeupProduct is null)
			{
				return RedirectToAction("Error", "Home");   //1º o ficheiro, depois a vista. É o error, da vista Home. Temos de indicar o caminho.
			}
			return View(makeupProductVM);   //se não for nulo, devolvo TODOS os dados da categoria que eu quero abrir e editar
		}

		//POST - para gravar os dados atualizados depois de ele apagar
		[HttpPost]
        public IActionResult Delete(MakeupProductVM makeupProductVM)
        {
            MakeupProduct? objFromDb = _db.MakeupProducts.FirstOrDefault(_ => _.Id == makeupProductVM.MakeupProduct.Id);

            if (objFromDb is not null)
            {
                _db.MakeupProducts.Remove(objFromDb);
                _db.SaveChanges();              //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "The Product has been deleted sucessfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "The Product could not be deleted!";
            return View(makeupProductVM);
        }
    }
}
