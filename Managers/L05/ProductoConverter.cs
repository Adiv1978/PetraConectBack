using Npgsql;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using System.Data;

namespace PetraConectBack.Managers.L05
{
    public class ProductoConverter
    {
        private readonly L04.ProductoConverter _productoConverterL04;

        public ProductoConverter()
        {
            _productoConverterL04 = new L04.ProductoConverter();
        }

        public List<NpgsqlParameter> Converter(SetProductoRequest request, int minutosCaduca)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            return _productoConverterL04.Converter(request, minutosCaduca);
        }

        public List<SetProductoResponse> Converter(DataTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));
            List<SetProductoResponse> list = new List<SetProductoResponse>();
            foreach (DataRow row in table.Rows)
            {
                SetProductoResponse item = _productoConverterL04.Converter(row);
                list.Add(item);
            }
            return list;
        }

        public List<NpgsqlParameter> Converter(UpdateProductoRequest request, int minutosCaduca)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            return _productoConverterL04.Converter(request, minutosCaduca);
        }

        public List<UpdateProductoResponse> ConverterUpdateProducto(DataTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));
            List<UpdateProductoResponse> list = new List<UpdateProductoResponse>();
            foreach (DataRow row in table.Rows)
            {
                UpdateProductoResponse item = _productoConverterL04.ConverterUpdateProducto(row);
                list.Add(item);
            }
            return list;
        }


        public List<NpgsqlParameter> Converter(GetProductoRequest request, int minutosCaduca)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            return _productoConverterL04.Converter(request, minutosCaduca);
        }

        public List<ProductoItemResponse> ConverterGetProducto(DataTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));
            List<ProductoItemResponse> list = new List<ProductoItemResponse>();
            foreach (DataRow row in table.Rows)
            {
                ProductoItemResponse item = _productoConverterL04.ConverterGetProducto(row);
                list.Add(item);
            }
            return list;
        }

    }
}
