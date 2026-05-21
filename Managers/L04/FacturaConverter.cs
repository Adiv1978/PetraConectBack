using Npgsql;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;
using System.Text.Json;

namespace PetraConectBack.Managers.L04
{
    public class FacturaConverter
    {
        public List<NpgsqlParameter> Converter(GetFacturaRequest request, int minutosCaduca)
        {
            return new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@p_sessiontoken", request.SessionToken ?? (object)DBNull.Value),
                new NpgsqlParameter("@p_minutos_caduca", minutosCaduca),
                new NpgsqlParameter("@p_idfactura", request.IdFactura ?? (object)DBNull.Value),
                new NpgsqlParameter("@p_idalegra", request.IdAlegra ?? (object)DBNull.Value)
            };
        }

        public FacturaItemResponse ConverterGetFactura(DataRow row)
        {
            FacturaItemResponse item = new FacturaItemResponse();
            item.IdFactura = row["idfactura"] == DBNull.Value ? null : Convert.ToInt64(row["idfactura"]);
            item.IdAlegra = row["idalegra"] == DBNull.Value ? null : Convert.ToString(row["idalegra"]);
            item.RegFec = row["regfec"] == DBNull.Value ? null : Convert.ToDateTime(row["regfec"]);
            item.Observacion = row["observacion"] == DBNull.Value ? null : Convert.ToString(row["observacion"]);
            item.IsOpen = row["isopen"] != DBNull.Value && Convert.ToBoolean(row["isopen"]);
            item.StatusActual = row["status_actual"] == DBNull.Value ? null : Convert.ToString(row["status_actual"]);
            item.StatusFecReg = row["status_fecreg"] == DBNull.Value ? null : Convert.ToDateTime(row["status_fecreg"]);
            item.StatusComentario = row["status_comentario"] == DBNull.Value ? null : Convert.ToString(row["status_comentario"]);
            item.IdUsuarioStatus = row["idusuario_status"] == DBNull.Value ? null : Convert.ToInt64(row["idusuario_status"]);

            item.DetalleRawJson = row["detalle_json"] == DBNull.Value ? null : Convert.ToString(row["detalle_json"]);
            item.StatusRawJson = row["status_json"] == DBNull.Value ? null : Convert.ToString(row["status_json"]);

            if (!string.IsNullOrWhiteSpace(item.DetalleRawJson))
            {
                using JsonDocument doc = JsonDocument.Parse(item.DetalleRawJson);
                if (doc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement detalleElement in doc.RootElement.EnumerateArray())
                    {
                        item.Detalle.Add(ConverterFacturaDetalle(detalleElement));
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(item.StatusRawJson))
            {
                using JsonDocument doc = JsonDocument.Parse(item.StatusRawJson);
                if (doc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement statusElement in doc.RootElement.EnumerateArray())
                    {
                        item.Status.Add(ConverterFacturaStatus(statusElement));
                    }
                }
            }

            return item;
        }

        public FacturaDetalleItemResponse ConverterFacturaDetalle(JsonElement detalleElement)
        {
            FacturaDetalleItemResponse item = new FacturaDetalleItemResponse { RawJson = detalleElement.GetRawText() };

            if (detalleElement.ValueKind != JsonValueKind.Object)
                return item;

            if (detalleElement.TryGetProperty("idFacturaDet", out JsonElement idFacturaDet) && idFacturaDet.ValueKind == JsonValueKind.Number && idFacturaDet.TryGetInt64(out long idFacturaDetValue))
                item.IdFacturaDet = idFacturaDetValue;

            if (detalleElement.TryGetProperty("idProducto", out JsonElement idProducto) && idProducto.ValueKind == JsonValueKind.Number && idProducto.TryGetInt64(out long idProductoValue))
                item.IdProducto = idProductoValue;

            if (detalleElement.TryGetProperty("idAlegra", out JsonElement idAlegra) && idAlegra.ValueKind == JsonValueKind.String)
                item.IdAlegra = idAlegra.GetString();

            if (detalleElement.TryGetProperty("referencia", out JsonElement referencia) && referencia.ValueKind == JsonValueKind.String)
                item.Referencia = referencia.GetString();

            if (detalleElement.TryGetProperty("nombre", out JsonElement nombre) && nombre.ValueKind == JsonValueKind.String)
                item.Nombre = nombre.GetString();

            if (detalleElement.TryGetProperty("descripcion", out JsonElement descripcion) && descripcion.ValueKind == JsonValueKind.String)
                item.Descripcion = descripcion.GetString();

            if (detalleElement.TryGetProperty("isCocina", out JsonElement isCocina))
            {
                if (isCocina.ValueKind == JsonValueKind.True || isCocina.ValueKind == JsonValueKind.False)
                    item.IsCocina = isCocina.GetBoolean();
                else if (isCocina.ValueKind == JsonValueKind.String && bool.TryParse(isCocina.GetString(), out bool isCocinaValue))
                    item.IsCocina = isCocinaValue;
            }

            return item;
        }

        public FacturaStatusItemResponse ConverterFacturaStatus(JsonElement statusElement)
        {
            FacturaStatusItemResponse item = new FacturaStatusItemResponse { RawJson = statusElement.GetRawText() };

            if (statusElement.ValueKind != JsonValueKind.Object)
                return item;

            if (statusElement.TryGetProperty("idFacturaStatus", out JsonElement idFacturaStatus) && idFacturaStatus.ValueKind == JsonValueKind.Number && idFacturaStatus.TryGetInt64(out long idFacturaStatusValue))
                item.IdFacturaStatus = idFacturaStatusValue;

            if (statusElement.TryGetProperty("idUsuario", out JsonElement idUsuario) && idUsuario.ValueKind == JsonValueKind.Number && idUsuario.TryGetInt64(out long idUsuarioValue))
                item.IdUsuario = idUsuarioValue;

            if (statusElement.TryGetProperty("usuarioNick", out JsonElement usuarioNick) && usuarioNick.ValueKind == JsonValueKind.String)
                item.UsuarioNick = usuarioNick.GetString();

            if (statusElement.TryGetProperty("fecReg", out JsonElement fecReg))
            {
                if (fecReg.ValueKind == JsonValueKind.String && DateTime.TryParse(fecReg.GetString(), out DateTime fecRegValue))
                    item.FecReg = fecRegValue;
            }

            if (statusElement.TryGetProperty("comentario", out JsonElement comentario) && comentario.ValueKind == JsonValueKind.String)
                item.Comentario = comentario.GetString();

            if (statusElement.TryGetProperty("status", out JsonElement status) && status.ValueKind == JsonValueKind.String)
                item.Status = status.GetString();

            return item;
        }
    }
}
