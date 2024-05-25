using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RitaGlamStudio.Application.Common.Interfaces;
using RitaGlamStudio.Domain.Entities;
using RitaGlamStudio.Web.ViewModels;

namespace RitaGlamStudio.Web.Controllers
{
	public class MakeupProductController : Controller
    {
		//Não vamos aceder diretamente ao repositório, vamos passar primeiro pela interface
		//Tudo através do UnitOfWork
		private readonly IUnitOfWork _unitOfWork;

		//para as imagens
		private readonly IWebHostEnvironment _webHostEnvironment;

		//Construtor
		//Estamos a aceder através do repositório
		public MakeupProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}

        public IActionResult Index()
        {
            var makeupProducts = _unitOfWork.MakeupProduct.GetAll(includeProperties: "Brand, Category");

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
                BrandItems = _unitOfWork.Brand.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),

                }),

                //Mesma coisa para a categoria - vai buscar os ids das categorias
                CategoryItems = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
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
            bool nameExists = _unitOfWork.MakeupProduct.Any(u => u.Name == obj.MakeupProduct.Name);

            if (obj.MakeupProduct.Name == obj.MakeupProduct.Description)
                ModelState.AddModelError("Name", "The Description cannot be a exact match to be Name");

            if (ModelState.IsValid && !nameExists)                      //validações do lado do servidor
            {
				//File Upload
				if (obj.MakeupProduct.Image is not null)
				{
					//Devemos gerar sempre nomes aleatórios na bd para que ela não estoire cada vez que o user faz upload de uma
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.MakeupProduct.Image.FileName);
					//Guid é o que garante o nome igual. Defini aqui o nome para a imagem
					//Preciso também da extensão da imagem. Esse comando é o que esta a seguir ao +. 
					//onde vou guardar a imagem?
					string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\productimages");
					//(vai dar ao caminho até à nossa pasta: www.root + o caminho para as pastas)

					//proteção para a bd não estoirar
					using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
					{
						obj.MakeupProduct.Image.CopyTo(fileStream);
					}
					obj.MakeupProduct.ImageUrl = @"\images\productimages\" + fileName;
				}
				else
				{
					//Se o user não colocar imagem, é colocado esta por default
					obj.MakeupProduct.ImageUrl = "https://placehold.co/600x400";
				}

				_unitOfWork.MakeupProduct.Add(obj.MakeupProduct);        //para injectar na bd
				_unitOfWork.Save();                                      //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "The Product has been created sucessfully!";
                return RedirectToAction(nameof(Index));
            }

            if (nameExists)
            {
                TempData["error"] = "The Product Name already exists.";
            }

            obj.BrandItems = _unitOfWork.Brand.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),

            });

            obj.CategoryItems = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
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
                BrandItems = _unitOfWork.Brand.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),

                }),

                //Mesma coisa para a categoria - vai buscar os ids das categorias
                CategoryItems = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                //Se o id for == ao makeupProductId, ele vai pôr aqui os dados todos
                MakeupProduct = _unitOfWork.MakeupProduct.Get(_=>_.Id == makeupProductId)
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
				_unitOfWork.MakeupProduct.Update(makeupProductVM.MakeupProduct);        //para injectar na bd
				_unitOfWork.Save();                                                     //vai ver que alterações foram preparadas e faz save na bd

				TempData["success"] = "The Product has been updated sucessfully!";
				return RedirectToAction(nameof(Index));
			}

			makeupProductVM.BrandItems = _unitOfWork.Brand.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString(),

			});

			makeupProductVM.CategoryItems = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
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
				BrandItems = _unitOfWork.Brand.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString(),

				}),

				//Mesma coisa para a categoria - vai buscar os ids das categorias
				CategoryItems = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				//Se o id for == ao makeupProductId, ele vai pôr aqui os dados todos
				MakeupProduct = _unitOfWork.MakeupProduct.Get(_ => _.Id == makeupProductId)
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
            MakeupProduct? objFromDb = _unitOfWork.MakeupProduct.Get(_ => _.Id == makeupProductVM.MakeupProduct.Id);

            if (objFromDb is not null)
            {
                _unitOfWork.MakeupProduct.Remove(objFromDb);
                _unitOfWork.Save();                                     //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "The Product has been deleted sucessfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "The Product could not be deleted!";
            return View(makeupProductVM);
        }
    }
}
