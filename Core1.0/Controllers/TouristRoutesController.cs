using AutoMapper;
using Core1._0.Dtos;
using Core1._0.Dtos.Request;
using Core1._0.IServices;
using Core1._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        #region 注入
        private ITouristRouteRepository _touristRouteRepository;

        private IMapper _mapper;
        public TouristRoutesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository ?? throw new Exception("touristRouteRepository not auto ");
            _mapper = mapper ?? throw new Exception("mapper not auto ");
        }
        #endregion

        #region 垃圾
        //[HttpGet]
        //public IActionResult GetTouristRoutes()
        //{
        //    var routes = _touristRouteRepository.GetTouristRoutes();
        //    return Ok(_mapper.Map<IEnumerable<TouristRouteDto>>(routes));
        //}
        //[HttpGet]
        //public IActionResult GetPictureListForTouristRoute(Guid guid)
        //{
        //    if (!_touristRouteRepository.TouristRouteExists(guid))
        //    {
        //        return NotFound("路由路线不存在");
        //    }
        //    var picturesFromRepo = _touristRouteRepository.GetPicturesByTouristRouteID(guid);
        //    if (picturesFromRepo == null || picturesFromRepo.Count() <= 0)
        //    {
        //        return NotFound("照片不存在");
        //    }
        //    return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo));
        //}

        //[HttpGet("{pictureID}")]
        //public IActionResult GetPicture([FromRoute] Guid touristRouteId, int pictureID)
        //{

        //    if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
        //    {
        //        return NotFound("旅游路綫不尊在");
        //    }
        //    var pictureFromRpo = _touristRouteRepository.GetPicture(pictureID);
        //    if (pictureFromRpo == null)
        //    {
        //        return NotFound("相片不存在");
        //    }
        //    return Ok(_mapper.Map<TouristRoutePictureDto>(pictureFromRpo));

        //} 
        #endregion

        #region  GetTouristRouteByID 根据ID获取旅游路线
        [HttpGet("{touristRouteId}", Name = "GetTouristRouteByID")]
        public IActionResult GetTouristRouteByID(Guid touristRouteId)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("路线不存在");
            }
            return Ok(_mapper.Map<TouristRouteDto>(_touristRouteRepository.GetTouistRoute(touristRouteId)));
        }
        #endregion

        #region CreateTouist 创建旅游路线

        [HttpPost]
        public IActionResult CreateTouist([FromBody] TouristRouteForCreateDto touristRouteForCreateDto)
        {
            var touristroute = _mapper.Map<TouristRoute>(touristRouteForCreateDto);
            _touristRouteRepository.AddTouristRoute(touristroute);
            return CreatedAtRoute("GetTouristRouteByID", new { touristRouteId = touristroute.Id }, _mapper.Map<TouristRouteDto>(touristroute));
        }
        #endregion

        #region GetTouristRouteByRating 条件检索旅游路线
        [HttpGet]
        public IActionResult GetTouristRouteByRating([FromQuery] TouristRouteResourceRatingDto touristRouteResourceRatingDto)
        {
            IEnumerable<TouristRoute> list = _touristRouteRepository.GetTouristRoutes(touristRouteResourceRatingDto.KewWord, touristRouteResourceRatingDto.RatingOperator, touristRouteResourceRatingDto.RatingValue);

            if (list == null)
            {
                return NotFound("路线不存在");
            }
            return Ok(_mapper.Map<IEnumerable<TouristRouteDto>>(list));
        }
        #endregion



        [HttpPut("{touristRouteId}")]
        public IActionResult UpdateTouristRoute([FromRoute] Guid touristRouteId, [FromBody] TouristRouteForUpdateDto touristRouteForUpdateDto)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("不存在");
            }
            TouristRoute touristRoute = _touristRouteRepository.GetTouistRoute(touristRouteId);
            touristRoute = _mapper.Map(touristRouteForUpdateDto, touristRoute);
            _touristRouteRepository.Save();
            return NoContent();
        }

    }
}
