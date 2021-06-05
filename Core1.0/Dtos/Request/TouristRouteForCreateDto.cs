using Core1._0.CoreAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.Dtos.Request
{
    [TouristRouteTitleMustBeDifferentFromDescription]
    public class TouristRouteForCreateDto
    {

        [Required(ErrorMessage = "title 不可为空")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1500)]
        //打折后的价格
        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? Updatetime { get; set; }

        public DateTime? DepatureTime { get; set; }
        //特色
        public string Features { get; set; }
        /// <summary>
        /// 服务费
        /// </summary>
        public string Fees { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }
        //折扣
        public double? Rating { get; set; }
        /// <summary>
        /// 几日游
        /// </summary>
        public string TravelDays { get; set; }

        public string TripType { get; set; }

        public string DepartureCity { get; set; }

        public ICollection<TouristRoutePictureForCreationDto> touristRoutePictureForCreationDtos { get; set; } = new List<TouristRoutePictureForCreationDto>();

    }
}
