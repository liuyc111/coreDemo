using Core1._0.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.Models
{
    public class TouristRoute
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal OriginalPrice { get; set; }
        ///折扣价格
        public double? DiscountPresent { get; set; }

        public DateTime CreateTime { get; set; }

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

        public ICollection<TouristRoutePicture> Pictures { get; set; } = new List<TouristRoutePicture>();

        public double? Rating { get; set; }

        public TravelDays? TravelDays { get; set; }

        public TripType? TripType { get; set; }
    }
}
