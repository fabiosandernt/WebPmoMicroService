
namespace ONS.WEBPMO.Application.Models
{
    public class UploadFileModel
    {
        // Prefixo utilizado para identificar arquivos armazenados em banco de dados
        public const string PrefixDatabase = "database_";

        // Identificador único do arquivo
        public string Id { get; set; }

        // Nome que será utilizado para o armazenamento do arquivo
        public string TargetName { get; set; }

        // Nome original do arquivo
        public string Name { get; set; }

        // Indica se o arquivo está armazenado em banco de dados
        public bool Database { get; set; }

        // Tamanho do arquivo em bytes
        public int Size { get; set; }

        // Tamanho do arquivo em kilobytes
        public int SizeInKb => Size / 1024;

        // Tipo MIME do arquivo
        public string MimeType { get; set; }

        // Caminho físico completo do arquivo
        public string PhysicalFullPath { get; set; }

        // Identificador GUID para arquivos armazenados no banco de dados
        public Guid IdStoredIntoDatabase { get; set; }

        // Propriedades adicionais que podem ser definidas pelo usuário
        public Dictionary<string, object> ExtendedProperties { get; set; }

        // Construtor padrão
        public UploadFileModel()
        {
            ExtendedProperties = new Dictionary<string, object>();
        }

        // Construtor com parâmetros para inicializar o objeto
        public UploadFileModel(string id, string name, int size, bool database)
            : this(id, null, name, size, database)
        {
        }

        // Construtor com parâmetros para inicializar o objeto com um nome de destino
        public UploadFileModel(string id, string targetName, string name, int size, bool database)
        {
            Id = id;
            Name = name;
            Database = database;
            Size = size;

            // Define o TargetName com base na condição do armazenamento
            TargetName = database ? PrefixDatabase + id : targetName;

            ExtendedProperties = new Dictionary<string, object>();
        }

        // Verifica se o nome de destino indica armazenamento em banco de dados
        public static bool IsDatabase(string targetName)
        {
            return targetName.StartsWith(PrefixDatabase);
        }

        // Extrai o ID do banco de dados do nome de destino
        public static string ExtractIdDatabase(string targetName)
        {
            return targetName.Substring(PrefixDatabase.Length);
        }

        // Adiciona ou atualiza uma propriedade estendida
        public void AddOrSetExtendedProperty(string propertyName, object value)
        {
            if (ExtendedProperties.ContainsKey(propertyName))
            {
                ExtendedProperties[propertyName] = value;
            }
            else
            {
                ExtendedProperties.Add(propertyName, value);
            }
        }

        // Obtém o valor de uma propriedade estendida ou retorna um valor padrão
        public object GetValueOrDefaultExtendedProperty(string propertyName, object defaultValue)
        {
            if (ExtendedProperties.ContainsKey(propertyName))
            {
                return ExtendedProperties[propertyName];
            }

            return defaultValue;
        }
    }
}
