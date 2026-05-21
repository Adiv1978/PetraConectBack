using PetraConectBack.Managers.L00;
using PetraConectBack.RecursosPetra.RsSQL;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;

namespace PetraConectBack.Managers.L10
{
    public class MngProducto
    {
        private readonly BDHelper _bdHelper;
        private readonly L05.ProductoConverter _productoConverterL05;
        private readonly SettingHelper _settingHelper;

        public MngProducto(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _settingHelper = new SettingHelper(configuration);
            string connectionString = _settingHelper.GetConnectionString();
            _bdHelper = new BDHelper(connectionString);
            _productoConverterL05 = new L05.ProductoConverter();
        }

        public async Task<SetProductoResponse?> SetProducto(SetProductoRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            string sql = RsUsuario.SetProducto;
            int minutosCaduca = _settingHelper.GetSessionMinutes();
            var parameters = _productoConverterL05.Converter(request, minutosCaduca);
            DataTable table = await _bdHelper.ExecuteDataTableAsync(sql, parameters);
            List<SetProductoResponse> result = _productoConverterL05.Converter(table);
            if (result.Count == 0)
                return null;
            return result[0];
        }

        public async Task<UpdateProductoResponse?> UpdateProducto(UpdateProductoRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            string sql = RsProducto.UpdateProducto;
            int minutosCaduca = _settingHelper.GetSessionMinutes();
            var parameters = _productoConverterL05.Converter(request, minutosCaduca);
            DataTable table = await _bdHelper.ExecuteDataTableAsync(sql, parameters);
            List<UpdateProductoResponse> result = _productoConverterL05.ConverterUpdateProducto(table);
            if (result.Count == 0)
                return null;
            return result[0];
        }
    }
}
