//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HoaTuoiBaSanh.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTHD
    {
        public Nullable<int> SoLuong { get; set; }
        public Nullable<double> DonGia { get; set; }
        public string MaHang { get; set; }
        public string MaHT { get; set; }
        public int IDHoaDon { get; set; }
    
        public virtual HangHoa HangHoa { get; set; }
        public virtual HoaDon HoaDon { get; set; }
        public virtual HinhThuc HinhThuc { get; set; }
    }
}
