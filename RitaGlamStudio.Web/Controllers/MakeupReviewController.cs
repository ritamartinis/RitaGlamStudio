using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RitaGlamStudio.Application.Common.Interfaces;
using RitaGlamStudio.Domain.Entities;
using RitaGlamStudio.Web.ViewModels;

namespace RitaGlamStudio.Web.Controllers
{
	public class MakeupReviewController : Controller
    {
		//Não vamos aceder diretamente ao repositório, vamos passar primeiro pela interface
		//Tudo através do UnitOfWork
		private readonly IUnitOfWork _unitOfWork;

		//Construtor
		//Estamos a aceder através do repositório
		public MakeupReviewController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public IActionResult Index()
        {
            var makeupReviews = _unitOfWork.MakeupReview.GetAll(includeProperties: "MakeupProduct");

            return View(makeupReviews);
        }

        // -- MAKEUP REVIEWS -- 

        //1 - Create

        //GET - Este é um clique normal que "mostra" o formulário
        public IActionResult Create()
        {
            //MakeupReviewVM é um novo modelo - que só é usado nas vistas - que é superior aos outros
            MakeupReviewVM makeupReviewVM = new()
            {
                //Estamos a criar um dropdown (um select) mas mostra nos produtos os id dos produtos
                MakeupProductItems = _unitOfWork.MakeupProduct.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),

                }),
            };

            return View(makeupReviewVM);
        }

        //POST - Aqui é para gravarmos os dados do formulário
        [HttpPost]  //este método vai receber pelo método post os dados do formulário
        [ValidateAntiForgeryToken]  //para segurança
        public IActionResult Create(MakeupReviewVM obj)
        {

            if (ModelState.IsValid)                                        //validações do lado do servidor
            {

				_unitOfWork.MakeupReview.Add(obj.MakeupReview);           //para injectar na bd
				_unitOfWork.Save();                                      //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "The Review has been created sucessfully!";
                return RedirectToAction(nameof(Index));
            }

            obj.MakeupProductItems = _unitOfWork.MakeupProduct.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),

            });

            TempData["error"] = "The Review could not be created!";
            return View(obj);
        }

        // 2 - Update
        //GET - este é para, depois de selecionar qual é aquele que quero editar, recebe o id 
        public IActionResult Update(int makeupReviewId)
        {
            MakeupReviewVM makeupReviewVM = new()
            {
                //Estamos a criar um dropdown (um select) mas mostra nos produtos os id das marcas
                MakeupProductItems = _unitOfWork.MakeupProduct.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),

                }),

                //Se o id for == ao makeupReviewId, ele vai pôr aqui os dados todos
                MakeupReview = _unitOfWork.MakeupReview.Get(_=>_.Id == makeupReviewId)
            };

            if (makeupReviewVM.MakeupReview is null)
            {
                return RedirectToAction("Error", "Home");   //1º o ficheiro, depois a vista. É o error, da vista Home. Temos de indicar o caminho.
            }
            return View(makeupReviewVM);   //se não for nulo, devolvo TODOS os dados da categoria que eu quero abrir e editar
        }

        //POST - para gravar os dados atualizados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(MakeupReviewVM makeupReviewVM)
        {

			if (ModelState.IsValid)                                               //validações do lado do servidor
			{
				_unitOfWork.MakeupReview.Update(makeupReviewVM.MakeupReview);        //para injectar na bd
				_unitOfWork.Save();                                                     //vai ver que alterações foram preparadas e faz save na bd

				TempData["success"] = "The Review has been updated sucessfully!";
				return RedirectToAction(nameof(Index));
			}

			makeupReviewVM.MakeupProductItems = _unitOfWork.MakeupProduct.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString(),

			});

			TempData["error"] = "The Review could not be updated!";
			return View(makeupReviewVM);
		}

		// 3 - Delete
		//GET - este é para, depois de selecionar qual é aquele que quero eliminar, recebe o id 
		public IActionResult Delete(int makeupReviewId)
		{
			MakeupReviewVM makeupReviewVM = new()
			{
				MakeupProductItems = _unitOfWork.MakeupProduct.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString(),

				}),

				//Se o id for == ao makeupReviewId, ele vai pôr aqui os dados todos
				MakeupReview = _unitOfWork.MakeupReview.Get(_ => _.Id == makeupReviewId)
			};

			if (makeupReviewVM.MakeupReview is null)
			{
				return RedirectToAction("Error", "Home");   //1º o ficheiro, depois a vista. É o error, da vista Home. Temos de indicar o caminho.
			}
			return View(makeupReviewVM);   //se não for nulo, devolvo TODOS os dados da categoria que eu quero abrir e editar
		}

		//POST - para gravar os dados atualizados depois de ele apagar
		[HttpPost]
        public IActionResult Delete(MakeupReviewVM makeupReviewVM)
        {
            MakeupReview? objFromDb = _unitOfWork.MakeupReview.Get(_ => _.Id == makeupReviewVM.MakeupReview.Id);

            if (objFromDb is not null)
            {
                _unitOfWork.MakeupReview.Remove(objFromDb);
                _unitOfWork.Save();                                     //vai ver que alterações foram preparadas e faz save na bd

                TempData["success"] = "The Review has been deleted sucessfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "The Review could not be deleted!";
            return View(makeupReviewVM);
        }
    }
}
