using EduHome.Business.DTOs.CourseDTOs;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CourseController : ControllerBase
    {
        public readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;

        }


        [HttpGet("")]

        public async Task<IActionResult> Get()
        {
            try
            {
                var courses = await _courseService.FindAllAsync();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }


        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var courseDto = await _courseService.FindByIdAsync(id);
                return Ok(courseDto);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError); ;
            }


        }


        [HttpGet("searchByName/{name}")]

        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var courseDto = await _courseService.FindByConditionAsync(n => n.Name != null ? n.Name.Contains(name) : true);
                return Ok(courseDto);
            }
            catch (Exception )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError); ;
            }


        }

        [HttpPost("")]
        public async Task<IActionResult> Post(CoursePostDTO coursePost)
        {
            try
            {
                await _courseService.CreateAsync(coursePost);
                return Ok();

            }
            catch (Exception)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,CourseUpdateDTO courseUpdate)
        {
            try
            {
               await _courseService.UpdateAsync(id, courseUpdate);
                return NoContent();

            }catch(NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(BadRequestException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
               await _courseService.Delete(id);
                return Ok("Deleted");
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError); ;
            }


        }


    }
}
