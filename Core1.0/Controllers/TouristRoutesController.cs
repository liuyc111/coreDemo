using AutoMapper;
using Core1._0.Dtos;
using Core1._0.Dtos.Request;
using Core1._0.IServices;
using Core1._0.Models;
using Microsoft.AspNetCore.Authorization;
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

        #region CreateTouist 创建旅游路线
        /// <summary>
        /// 创建旅游路线 管理员权限
        /// </summary>
        /// <param name="touristRouteForCreateDto"></param>
        /// <returns></returns>

        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> CreateTouist([FromBody] TouristRouteForCreateDto touristRouteForCreateDto)
        {
            var touristroute = _mapper.Map<TouristRoute>(touristRouteForCreateDto);
            await _touristRouteRepository.AddTouristRouteasync(touristroute);
            return CreatedAtRoute("GetTouristRouteByID", new { touristRouteId = touristroute.Id }, _mapper.Map<TouristRouteDto>(touristroute));
        } 
        #endregion
        /// <summary>
        /// 根据条件进行查找
        /// </summary>
        /// <param name="touristRouteResourceRatingDto"></param>
        /// <returns></returns>
        #region GetTouristRouteByRating 条件检索旅游路线
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTouristRouteByRating([FromQuery] TouristRouteResourceRatingDto touristRouteResourceRatingDto)
        {
            IEnumerable<TouristRoute> list = await _touristRouteRepository.GetTouristRoutesasync(touristRouteResourceRatingDto.KewWord, touristRouteResourceRatingDto.RatingOperator, touristRouteResourceRatingDto.RatingValue);

            if (list == null)
            {
                return NotFound("路线不存在");
            }
            return Ok(_mapper.Map<IEnumerable<TouristRouteDto>>(list));
        }
        #endregion

        #region  GetTouristRouteByID 根据ID获取旅游路线
        /// <summary>
        /// 根据旅游路线获取详情
        /// </summary>
        /// <param name="touristRouteId"></param>
        /// <returns></returns>
        [HttpGet("{touristRouteId}", Name = "GetTouristRouteByID")]
        [Authorize]
        public async Task<IActionResult> GetTouristRouteByID(Guid touristRouteId)
        {
            if (!await _touristRouteRepository.TouristRouteExistsasync(touristRouteId))
            {
                return NotFound("路线不存在");
            }
            return Ok(_mapper.Map<TouristRouteDto>(await _touristRouteRepository.GetTouistRouteasync(touristRouteId)));
        }
        #endregion

        #region UpdateTouristRoute 更新旅游路线
        /// <summary>
        /// 更新旅游路线
        /// </summary>
        /// <param name="touristRouteId"></param>
        /// <param name="touristRouteForUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{touristRouteId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateTouristRoute([FromRoute] Guid touristRouteId, [FromBody] TouristRouteForUpdateDto touristRouteForUpdateDto)
        {
            if (!await _touristRouteRepository.TouristRouteExistsasync(touristRouteId))
            {
                return NotFound("不存在");
            }
            TouristRoute touristRoute = await _touristRouteRepository.GetTouistRouteasync(touristRouteId);
            touristRoute = _mapper.Map(touristRouteForUpdateDto, touristRoute);
           await _touristRouteRepository.Saveasync();
            return NoContent();
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
    }
}
