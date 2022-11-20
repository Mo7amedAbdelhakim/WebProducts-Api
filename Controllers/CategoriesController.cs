using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApp_Products.Data;
using WebApp_Products.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp_Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
           _context = context;
        }
       
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _context.Categories.OrderBy(p => p.Name).ToList();
                return Ok(result);
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
                var result = _context.Categories.FirstOrDefault(x=>x.Id==id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost("Save")]
        public IActionResult Save([FromBody] Category model)
        {
            try
            {
                var result = _context.Categories.Add(model);
                _context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

       
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category model)
        {
            try
            {
                var result = _context.Categories.FirstOrDefault(x => x.Id == id);
                if (result != null)
                {
                    result.Name = model.Name;
                    _context.Categories.Update(result);
                    _context.SaveChanges();
                return Ok(result);
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
                var result = _context.Categories.FirstOrDefault(x => x.Id == id);
                if (result != null)
                {  
                    _context.Categories.Remove(result);
                    _context.SaveChanges();
                    return Ok(result);
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
