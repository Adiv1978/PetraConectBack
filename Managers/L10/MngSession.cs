using Npgsql;
using PetraConectBack.Managers.L00;
using PetraConectBack.RecursosPetra.RsSQL;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;

namespace PetraConectBack.Managers.L10
{
    public class MngSession
    {
        private readonly BDHelper _bdHelper;
        private readonly L05.SessionConverter _sessionConverterL05;
        private readonly SettingHelper _settingHelper;

        public MngSession(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _settingHelper = new SettingHelper(configuration);
            string connectionString = _settingHelper.GetConnectionString();
            _bdHelper = new BDHelper(connectionString);
            _sessionConverterL05 = new L05.SessionConverter();
        }

        public async Task<ValidateSessionResponse?> ValidateSession(ValidateSessionRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            string sql = RsSession.ValidateSession;
            int minutosCaduca = _settingHelper.GetSessionMinutes();
            List<NpgsqlParameter> parameters = _sessionConverterL05.Converter(request, minutosCaduca);
            DataTable table = await _bdHelper.ExecuteDataTableAsync(sql, parameters);
            List<ValidateSessionResponse> result = _sessionConverterL05.Converter(table);
            if (result.Count == 0)
                return null;
            return result[0];
        }
    }
}
