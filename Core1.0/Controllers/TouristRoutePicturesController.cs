using AutoMapper;
using Core1._0.Dtos;
using Core1._0.IServices;
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

        #region GetPictureListForTouristRoute 根据旅游路线id获取旅游照片

        [HttpGet]
        public IActionResult GetPictureListForTouristRoute([FromRoute] Guid touristguid)
        {
            if (!touristRouteRepository.TouristRouteExists(touristguid))
            {
                return NotFound("旅游路线不存在");
            }
            var picturesFromRpo = touristRouteRepository.GetPicturesByTouristRouteID(touristguid);
            return Ok(mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRpo));
        }
        #endregion

        #region GetPictuer 获取某个图片

        [HttpGet("{pictureid}")]
        public IActionResult GetPictuer([FromRoute] Guid touristguid, int pictureid)
        {

            if (!touristRouteRepository.TouristRouteExists(touristguid))
            {
                return NotFound("路线不存在");
            }
            var picture = touristRouteRepository.GetPicture(pictureid);
            return Ok(mapper.Map<TouristRoutePictureDto>(picture));

        }
        #endregion
    }
}
