using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    //api cont validationı kendi oto yapar 
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService =hotelService;
        }
        //swaggerda açıklama yapabilmek için
        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // public List<Hotel> Get()
        public IActionResult Get()
        {
            var hotels= _hotelService.GetAllHotels();
            return Ok(hotels); //response 200 code ve body kısmında otelleri dön
        }
        //-----------------------Action Overloading(Route ile belirt)
        /// <summary>
        /// Get Hotel By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]//api/hotels/gethotelbyid/2
        public IActionResult GetHotelById(int id)
        {
            var hotel= _hotelService.GetHotelById(id);
            if(hotel!=null)
            {
                return Ok(hotel); //200+data
            }
            return NotFound();//404
        }

        [HttpGet]
        //[Route("GetHotelByName/{name}")](action ismi aynı oldu değiştirdim)
        [Route("[action]/{name}")]
        public IActionResult GetHotelByName(string name)
        {
            var hotel = _hotelService.GetHotelByName(name);
            if(hotel!=null)
            {
                return Ok(hotel);
            }
            return NotFound();           
        }

        //test with with 2 params
        [HttpGet]
        //[Route("[action]/{id}/{name}")]
        //query string ile gönderebilirim-->api/hotels/gethotelbyidandname?id=3&name=titanic
        [Route("[action]")]
        public IActionResult GetHotelByIdAndName(int id, string name)
        {
            return Ok();
        }

        /// <summary>
        /// Create an Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")] //or ("CreateHotel")
        public IActionResult Post([FromBody]Hotel hotel)
        {
            //Api controller kullanmasaydık böyle validation kontrol
            //if(ModelState.IsValid)
            //{
            //    var createdHotel = _hotelService.CreateHotel(hotel);
            //    return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel);//201
            //}
            //return BadRequest(ModelState);//404

            var createdHotel = _hotelService.CreateHotel(hotel);
            //used get by id 
            return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel);//201 + data 
        }


        /// <summary>
        /// Update the Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateHotel([FromBody]Hotel hotel)
        {
            if(_hotelService.GetHotelById(hotel.Id)!=null)
            {
                return Ok(_hotelService.UpdateHotel(hotel));//200 + data 
            }
            return NotFound();
        }

        /// <summary>
        /// Delete the Hotel
        /// </summary>
        /// <param name="id"></param>

        //[HttpDelete("{id}")]
        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult DeleteHotel(int id )
        {
            if(_hotelService.GetHotelById(id)!=null)
            {
                _hotelService.DeleteHotel(id);
                return Ok();//200
            }
            return NotFound();//404
        }
    }
}
