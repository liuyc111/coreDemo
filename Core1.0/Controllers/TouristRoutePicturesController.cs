using AutoMapper;
using Core1._0.Dtos;
using Core1._0.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.Controllers
{

    [Route("api/{controller}/{touristguid}/Pictures")]
    [ApiController]
    public class TouristRoutePicturesController : ControllerBase
    {
        #region 注入
        public ITouristRouteRepository touristRouteRepository;
        private IMapper mapper;
        public TouristRoutePicturesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            this.touristRouteRepository = touristRouteRepository;
            this.mapper = mapper;
        }
        #endregion

        #region GetPictuer 获取某个图片
        /// <summary>
        /// 根据旅游路线id获取某一个图片
        /// </summary>
        /// <param name="touristguid">路线ID</param>
        /// <param name="pictureid">照片</param>
        /// <returns></returns>

        [HttpGet("{pictureid}")]
        [Authorize]
        public async Task<IActionResult> GetPictuer([FromRoute] Guid touristguid, int pictureid)
        {

            if (!await touristRouteRepository.TouristRouteExistsasync(touristguid))
            {
                return NotFound("路线不存在");
            }
            var picture = await touristRouteRepository.GetPictureasync(pictureid);
            return Ok(mapper.Map<TouristRoutePictureDto>(picture));

        }
        #endregion

        #region GetPictureListForTouristRoute 根据旅游路线id获取旅游照片
        /// <summary>
        /// 获取所有照片
        /// </summary>
        /// <param name="touristguid">路线ID</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPictureListForTouristRoute([FromRoute] Guid touristguid)
        {
            if (!await touristRouteRepository.TouristRouteExistsasync(touristguid))
            {
                return NotFound("旅游路线不存在");
            }
            var picturesFromRpo = await touristRouteRepository.GetPicturesByTouristRouteIDasync(touristguid);
            return Ok(mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRpo));
        }
        #endregion


    }
}
