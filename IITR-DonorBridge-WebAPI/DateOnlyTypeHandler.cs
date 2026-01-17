using Dapper;
using System.Data;

namespace IITR_DonorBridge_WebAPI
{
    public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.Value = value.ToDateTime(TimeOnly.MinValue);
        }

        public override DateOnly Parse(object value)
        {
            return DateOnly.FromDateTime((DateTime)value);
        }
    }
}
