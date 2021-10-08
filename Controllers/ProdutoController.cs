using System.Collections.Generic;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly DataContext _context;
        public ProdutoController(DataContext context)
        {
            _context = context;
        }
        //POST: api/produto/create
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Produto produto)
        {

            produto.Categoria = _context.Categorias.Find(produto.CategoriaId);
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return Created("", produto);
        }

        //get: api/produto/list
        [HttpGet]
        [Route("list")]
        public IActionResult List() => Ok(_context.Produtos
        .Include(produto => produto.Categoria)
        //.Include(ObjetoRelacionamento => Objeto.prop)
                //.Include(ObjetoRelacionamento => Objeto.prop.PróximoRelacionamento)

        .ToList());

        [HttpGet]
        [Route("getById/{id}")]
        public IActionResult FindById([FromRoute]int id){
            Produto produto = _context.Produtos.Find(id);
            if(produto == null){
                return NotFound();
            }

            return Ok(produto);
        //    return _context.Produtos.SingleOrDefault(produtoId => produtoId.Id == id);
        }

        // [HttpPut]
        // [Route("update/{id}")]
        // public Produto Update(int id){

        //     return null;
        // }

        [HttpDelete]
        [Route("delete/{name}")]
        public IActionResult Delete([FromRoute] string name){
            //Expressão lambda
            //buscar pelo primeiro nome
            Produto produto = _context.Produtos.FirstOrDefault(x => x.Nome == name);
            if(produto == null){
                return NotFound();
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return Ok(_context.Produtos.ToList());
        }



    }

}
