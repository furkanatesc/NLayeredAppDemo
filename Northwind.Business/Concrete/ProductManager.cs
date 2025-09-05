using Northwind.DataAccess.Abstract;
using Northwind.Entities.Concrete;
using System.Collections.Generic;
using Northwind.Business.Abstract;
using System;
using System.Data.Entity.Infrastructure;
using Northwind.Business.ValidationRules.FluentValidation;
using FluentValidation;

namespace Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            ProductValidator productValidator = new ProductValidator();
            
            var result = productValidator.Validate(product);
            if (result.Errors.Count >0)
            {
                throw new ValidationException(result.Errors);
            }
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetProductByCategory(int categoryId)
        {
            return _productDal.GetAll(p => p.CategoryId == categoryId);
        }

        public List<Product> GetProductsByProductName(string productName)
        {
            return _productDal.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));
        }

        public void Update(Product product)
        {
            try
            {
                _productDal.Update(product);
            }
            catch
            {
                throw new Exception("Silme Gerçekleşmedi");
            }
        }
    }
}
