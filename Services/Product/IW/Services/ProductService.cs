﻿using IW.Exceptions.ReadProductError;
using IW.Interfaces;
using IW.Models.DTOs;
using IW.Models.DTOs.CategoryDto;
using IW.Models.DTOs.Product;
using IW.Models.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace IW.Services
{
    public class ProductService : IProductService
    {
        public readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateProduct(CreateProduct input)
        {
            var product = await _unitOfWork.Products.FindByCondition(u => u.Name == input.Name);
            Category category =  await _unitOfWork.Categories.GetById(input.CategoryId);

            Product newProduct = input.Adapt<Product>();
            newProduct.Category = category;

            ProductValidator validator = new();
            validator.ValidateAndThrowException(newProduct);

            _unitOfWork.Products.Add(newProduct);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ProductDto?> GetProduct(int id)
        {
            var product = await ProductExist(id);
            if (Equals(product, null))
            {
                throw new ProductNotFoundException(id);
            }
            var category = await _unitOfWork.Categories.GetById(product.CategoryId);
            ProductDto result = product.Adapt<ProductDto>();
            result.Category = category.Adapt<CategoryDto>();
            return result;
        }

        public ProductDto? GetProductSync(int id)
        {
            var product = ProductExistSync(id);
            ProductDto result = product.Adapt<ProductDto>();
            return result;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(int page, int amount)
        {
            var products = await _unitOfWork.Products.GetAll(page, amount);
            ICollection<ProductDto> result = products.Adapt<ICollection<ProductDto>>();
            return result;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(GetProduct query, int page, int amount)
        {
            var products = await _unitOfWork.Products.FindByConditionToList(
                p => p.Name == query.Name ||
                p.CategoryId==query.CategoryId
                , page, amount);
            ICollection<ProductDto> result = products.Adapt<ICollection<ProductDto>>();
            return result;
        }

        public async Task UpdateProduct(int id, UpdateProduct model)
        {
            var product = await ProductExist(id);
            if (Equals(product, null)) throw new ProductNotFoundException(id);

            product.Name = model.Name ?? product.Name;
            product.CategoryId = model.CategoryId;
            product.Description = model.Description;
            product.Price = model.Price;
            product.SKU = model.SKU;
            product.Images=model.Images;

            ProductValidator validator = new();
            validator.ValidateAndThrowException(product);

            _unitOfWork.Products.Update(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var user = await ProductExist(id);
            if (Equals(user, null)) throw new ProductNotFoundException(id);

            _unitOfWork.Products.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        private async Task<Product?> ProductExist(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var result = await _unitOfWork.Products.GetById(id);
            return result;
        }

        private Product? ProductExistSync(int id)
        {
            if (id.ToString() == String.Empty) return null;
            var result = _unitOfWork.Products.GetByIdSync(id);
            return result;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByName(string name, int page, int amount)
        {
            var products = await _unitOfWork.Products.FindByConditionToList(
                p => EF.Functions.ILike(p.Name, $"%{name}%")
                , page, amount);
            ICollection<ProductDto> result = products.Adapt<ICollection<ProductDto>>();
            return result;
        }
    }
}
