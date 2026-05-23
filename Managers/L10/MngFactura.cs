using PetraConectBack.Managers.L00;
using PetraConectBack.RecursosPetra.RsSQL;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;

namespace PetraConectBack.Managers.L10
{
    public class MngFactura
    {
        private readonly BDHelper _bdHelper;
        private readonly SettingHelper _settingHelper;
        private readonly L05.FacturaConverter _facturaConverterL05;

        public MngFactura(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _settingHelper = new SettingHelper(configuration);
            string connectionString = _settingHelper.GetConnectionString();
            _bdHelper = new BDHelper(connectionString);
            _facturaConverterL05 = new L05.FacturaConverter();
        }

        public async Task<List<FacturaItemResponse>> GetFactura(GetFacturaRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            string sql = RsFactura.GetFactura;
            int minutosCaduca = _settingHelper.GetSessionMinutes();
            List<Npgsql.NpgsqlParameter> parameters = _facturaConverterL05.Converter(request, minutosCaduca);
            DataTable table = await _bdHelper.ExecuteDataTableAsync(sql, parameters);
            List<FacturaItemResponse> result = _facturaConverterL05.ConverterGetFactura(table);
            return result;
        }

        public async Task<List<FacturaItemResponse>> GetFacturasByStatusActual(GetFacturaByStatusRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            string sql = RsFactura.GetFacturasByStatusActual;
            int minutosCaduca = _settingHelper.GetSessionMinutes();
            List<Npgsql.NpgsqlParameter> parameters = _facturaConverterL05.Converter(request, minutosCaduca);
            DataTable table = await _bdHelper.ExecuteDataTableAsync(sql, parameters);
            List<FacturaItemResponse> result = _facturaConverterL05.ConverterGetFacturasByStatusActual(table);
            return result;
        }

        public async Task<SetFacturaDbResponse?> SetFactura(SetFacturaDbRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            string sql = RsFactura.SetFactura;
            int minutosCaduca = _settingHelper.GetSessionMinutes();
            List<Npgsql.NpgsqlParameter> parameters = _facturaConverterL05.Converter(request, minutosCaduca);
            DataTable table = await _bdHelper.ExecuteDataTableAsync(sql, parameters);
            List<SetFacturaDbResponse> result = _facturaConverterL05.ConverterSetFactura(table);
            return result.FirstOrDefault();
        }


        public async Task<SetFacturaStatusResponse?> SetFacturaStatus(SetFacturaStatusRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            string sql = RsFactura.SetFacturaStatus;
            int minutosCaduca = _settingHelper.GetSessionMinutes();
            List<Npgsql.NpgsqlParameter> parameters = _facturaConverterL05.Converter(request, minutosCaduca);
            DataTable table = await _bdHelper.ExecuteDataTableAsync(sql, parameters);
            List<SetFacturaStatusResponse> result = _facturaConverterL05.ConverterSetFacturaStatus(table);
            return result.FirstOrDefault();
        }

    }
}
