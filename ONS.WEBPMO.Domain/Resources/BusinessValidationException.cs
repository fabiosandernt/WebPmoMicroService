namespace ONS.WEBPMO.Domain.Resources
{
    public class BusinessValidationException : Exception
    {
        public IReadOnlyList<string> Errors { get; }

        // Construtor com mensagem única
        public BusinessValidationException(string error)
            : base("Ocorreram erros de validação.")
        {
            if (string.IsNullOrWhiteSpace(error))
                throw new ArgumentException("A mensagem de erro não pode ser nula ou vazia.", nameof(error));

            Errors = new List<string> { error };
        }

        public BusinessValidationException(IEnumerable<string> errors)
            : base("Ocorreram erros de validação.")
        {
            if (errors == null || !errors.Any())
                throw new ArgumentException("A lista de erros não pode ser nula ou vazia.", nameof(errors));

            Errors = errors.ToList().AsReadOnly();
        }


        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors);
        }
    }
}
