using Npgsql;
using PetraConectBack.Managers.L00;
using PetraConectBack.RecursosPetra.RsSQL;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;

namespace PetraConectBack.Managers.L10
{
    public class MngUsuario
    {
        private readonly BDHelper _bdHelper;
        private readonly L05.UsuarioConverter _usuarioConverterL05;
        private readonly SettingHelper _settingHelper;

        public MngUsuario(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _settingHelper = new SettingHelper(configuration);
            string connectionString = _settingHelper.GetConnectionString();
            _bdHelper = new BDHelper(connectionString);
            _usuarioConverterL05 = new L05.UsuarioConverter();
        }

        public async Task<SetUsuarioResponse?> SetUsuario(SetUsuarioRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            string sql = RsUsuario.SetUsuario;
            List<NpgsqlParameter> parameters = _usuarioConverterL05.Converter(request);
            DataTable table = await _bdHelper.ExecuteDataTableAsync(sql, parameters);
            List<SetUsuarioResponse> result = _usuarioConverterL05.Converter(table);
            if (result.Count == 0)
                return null;
            return result[0];
        }

        public async Task<UpdateUsuarioResponse?> UpdateUser(UpdateUsuarioRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            string sql = RsUsuario.UpdateUser;
            int minutosCaduca = _settingHelper.GetSessionMinutes();
            List<NpgsqlParameter> parameters = _usuarioConverterL05.Converter(request, minutosCaduca);
            DataTable table = await _bdHelper.ExecuteDataTableAsync(sql, parameters);
            List<UpdateUsuarioResponse> result = _usuarioConverterL05.ConverterUpdateUsuario(table);
            if (result.Count == 0)
                return null;
            return result[0];
        }

        public async Task<LoginUsuarioResponse?> LoginUsuario(LoginUsuarioRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            string sql = RsUsuario.LoginUsuario;
            int minutosCaduca = _settingHelper.GetSessionMinutes();
            List<NpgsqlParameter> parameters = _usuarioConverterL05.Converter(request, minutosCaduca);
            DataTable table = await _bdHelper.ExecuteDataTableAsync(sql, parameters);
            List<LoginUsuarioResponse> result = _usuarioConverterL05.ConverterLoginUsuario(table);
            if (result.Count == 0)
                return null;
            return result[0];
        }
    }
}
