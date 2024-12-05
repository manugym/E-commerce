﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using TuringClothes.Database;
using TuringClothes.Dtos;
using TuringClothes.Model;
using TuringClothes.Services;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private MyDatabase _mydatabase;
        private readonly ImageService _imageService;
        private readonly Mapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public AdminController(MyDatabase myDatabase, ImageService imageService, Mapper mapper, UnitOfWork unitOfWork)
        {
            _mydatabase = myDatabase;
            _imageService = imageService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getAllUsers")]
        public IActionResult GetUsers()
        {
            var users = _mydatabase.Users
                .Select(user => new
                {
                    user.Id,
                    user.Name,
                    user.Surname,
                    user.Email,
                    user.Role
                })
                .ToList();

            return Ok(users);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("editUserRol")]
        public async Task<IActionResult> EditUserRole(string email, string role)
        {
            var user = await _unitOfWork.AuthRepository.GetByEmail(email);
            user.Role = role;
            _mydatabase.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new { message = "Rol modificado correctamente." });
        }

        //[Authorize(Roles = "admin")]
        [HttpDelete("deleteUser")]
        public async Task<IActionResult> deleteUser(string email)
        {
            var user = await _unitOfWork.AuthRepository.GetByEmail(email);
            _mydatabase.Users.Remove(user);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new { message = "Usuario eliminado correctamente." });
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getAllProducts")]
        public IActionResult GetProducts()
        {
            var products = _mydatabase.Products
                .Select(product => new
                {
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Stock,
                    product.Image
                })
                .ToList();

            return Ok(products);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Image>> InsertAsync(CreateUpdateImageRequest createImage)
        {
            Image newImage = await _imageService.InsertAsync(createImage);

            return Created($"images/{newImage.Id}", _mapper.ToDto(newImage));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest(new { message = "Datos del producto no válidos." });
            }

            var newProduct = new Database.Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = (int)productDto.Price,
                Stock = productDto.Stock,
                Image = productDto.Image
            };

            _mydatabase.Products.Add(newProduct);

            await _mydatabase.SaveChangesAsync();

            return Ok(new { message = "Producto añadido correctamente.", product = newProduct });
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _mydatabase.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new { message = $"Producto con ID {id} no encontrado." });
            }

            _mydatabase.Products.Remove(product);

            await _mydatabase.SaveChangesAsync();

            return Ok(new { message = $"Producto con ID {id} eliminado correctamente." });
        }
    }
}
