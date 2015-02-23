using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingStore.Domain.Abstract;

namespace OnlineShoppingStore.WebUI.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult List()
        {

            return View(_productRepository.Products);
        }

    }
}