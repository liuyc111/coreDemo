using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.Dtos
{
    public class TouristRouteDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime CreateTime { get; set; }

        // 计算方式：原价 * 折扣
        public decimal Price { get; set; }

        public DateTime? UpDateTime { get; set; }

        public DateTime? DepartureTime { get; set; }
        /// <summary>
        /// 特色
        /// </summary>
        public string Features { get; set; }
        /// <summary>
        /// 服务费
        /// </summary>
        public string Fees { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }

        public double? Rating { get; set; }

        public string TravelDays { get; set; }

        public string TripType { get; set; }

        public string DepartureCity { get; set; }

        public ICollection<TouristRoutePictureDto> TouristRoutePictures { get; set; }
    }
}
