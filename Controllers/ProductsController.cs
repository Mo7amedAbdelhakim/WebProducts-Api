using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp_Products.Data;
using WebApp_Products.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp_Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var pro = _context.Products.Include(x => x.Category).OrderBy(x => x.Name).ToList();
                return Ok(pro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }


        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var pro = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
                if (pro != null)
                {
                    return Ok(pro);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpPost("Save")]
        public IActionResult Save([FromBody] ProductModelView model )
        {
            try
            {
                Product modelView = new Product() 
                { Name = model.Name, Price = model.Price, Quantity = model.Quantity,
                    CategoryID=model.CategoryID,total=model.total,discount=model.discount };
                if (modelView != null)
                {
                    var pro = _context.Products.Add(modelView);
                    _context.SaveChanges();
                    return Ok(modelView);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


      
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] ProductModelView NewModel)
        {
            try
            {
                var oldModel = _context.Products.FirstOrDefault(x => x.Id == id);

                if (oldModel != null)
                {


                    oldModel.Name = NewModel.Name;
                    oldModel.Price = NewModel.Price;
                    oldModel.Quantity = NewModel.Quantity  ;
                    oldModel.discount = NewModel.discount   ;
                    oldModel.total = NewModel.total    ;
                    oldModel.CategoryID = NewModel.CategoryID;
                    _context.Update(oldModel);
                    _context.SaveChanges();
                    return Ok(oldModel);
                }
                  
                 
                    
                
                return BadRequest();
              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var model = _context.Products.FirstOrDefault(x => x.Id == id);
                if (model != null)
                {
                    _context.Remove(model);
                    _context.SaveChanges();
                    return Ok(model);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
