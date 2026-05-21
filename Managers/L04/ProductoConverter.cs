using Npgsql;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;

namespace PetraConectBack.Managers.L04
{
    public class ProductoConverter
    {
        public List<NpgsqlParameter> Converter(SetProductoRequest request, int minutosCaduca)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(new NpgsqlParameter("@p_sessiontoken", request.SessionToken ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_minutos_caduca", minutosCaduca));
            parameters.Add(new NpgsqlParameter("@p_idalegra", request.IdAlegra ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_referencia", request.Referencia ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_nombre", request.Nombre ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_descripcion", request.Descripcion ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_iscocina", request.IsCocina ?? (object)DBNull.Value));
            return parameters;
        }

        public SetProductoResponse Converter(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));
            SetProductoResponse response = new SetProductoResponse();
            if (row.Table.Columns.Contains("isok") && row["isok"] != DBNull.Value)
                response.IsOk = Convert.ToBoolean(row["isok"]);
            if (row.Table.Columns.Contains("mensaje") && row["mensaje"] != DBNull.Value)
                response.Mensaje = Convert.ToString(row["mensaje"]);
            if (row.Table.Columns.Contains("idproducto") && row["idproducto"] != DBNull.Value)
                response.IdProducto = Convert.ToInt64(row["idproducto"]);
            return response;
        }

        public List<NpgsqlParameter> Converter(UpdateProductoRequest request, int minutosCaduca)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(new NpgsqlParameter("@p_sessiontoken", request.SessionToken ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_minutos_caduca", minutosCaduca));
            parameters.Add(new NpgsqlParameter("@p_idproducto", request.IdProducto ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_idalegra", request.IdAlegra ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_referencia", request.Referencia ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_nombre", request.Nombre ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_descripcion", request.Descripcion ?? (object)DBNull.Value));
            parameters.Add(new NpgsqlParameter("@p_iscocina", request.IsCocina));
            return parameters;
        }

        public UpdateProductoResponse ConverterUpdateProducto(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));
            UpdateProductoResponse response = new UpdateProductoResponse();
            if (row.Table.Columns.Contains("isok") && row["isok"] != DBNull.Value)
                response.IsOk = Convert.ToBoolean(row["isok"]);
            if (row.Table.Columns.Contains("mensaje") && row["mensaje"] != DBNull.Value)
                response.Mensaje = Convert.ToString(row["mensaje"]);
            if (row.Table.Columns.Contains("idproducto") && row["idproducto"] != DBNull.Value)
                response.IdProducto = Convert.ToInt64(row["idproducto"]);
            return response;
        }
    }
}
